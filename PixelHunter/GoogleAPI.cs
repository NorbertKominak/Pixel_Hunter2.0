using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PixelHunter
{
    /// <summary>
    /// Static class providing methods to send local images for analysis to Google Vision Cloud 
    /// using its API and store results locally.
    /// </summary>
    public static class GoogleAPI
    {
        // In case Analysis was cancelled by user
        public static bool Cancelled { get; set; } = false;
        // False if any communication with Api is running
        public static bool Finished { get; set; } = true;
        // Client to communicate with API
        private static ImageAnnotatorClient Client { get; set; }

        /// <summary>
        /// Yields images whose names are in imgsForApi file.
        /// </summary>
        /// <param name="imgsForApi">path to file with images names to yield</param>
        /// <param name="imgPath">path to images</param>
        /// <returns></returns>
        private static IEnumerable<(Image, string)> YieldImages(string imgsForApi, string imgPath)
        {
            if (File.Exists(imgsForApi))
            {
                using (StreamReader reader = new StreamReader(imgsForApi))
                {
                    string imgName;
                    while ((imgName = reader.ReadLine()) != null)
                    {
                        yield return (Image.FromFile(imgPath + "\\" + imgName), imgName);
                    }
                }
            }
        }

        /// <summary>
        /// Runs Object Detection task on provided local image and returns string result in format:
        /// line = img_name,class_name,score,left,top,right,bottom(bounding box edges)
        /// where each line represents a single detected object. If none object was detected only
        /// img_name is returned.
        /// </summary>
        /// <param name="img">Image object</param>
        /// <param name="imgName">image`s name</param>
        /// <returns>analysis result</returns>
        public static async Task<string> DetectObjects(Image img, string imgName)
        {
            var annotations = await Client.DetectLocalizedObjectsAsync(img);
            if (annotations.Count() == 0)
            {
                return imgName + "\n";
            }

            var result = new StringBuilder();
            foreach (var annotation in annotations)
            {
                result.Append($"{imgName},");
                double left = annotation.BoundingPoly.NormalizedVertices[0].X;
                double top = annotation.BoundingPoly.NormalizedVertices[0].Y;
                double right = annotation.BoundingPoly.NormalizedVertices[2].X;
                double bottom = annotation.BoundingPoly.NormalizedVertices[2].Y;
                result.Append($"{annotation.Name},{annotation.Score:0.00}," +
                    $"{left:0.00},{top:0.00},{right:0.00.},{bottom:0.00}\n");
            }
            return result.ToString();

        }

        /// <summary>
        /// Runs NSFW Detection task on provided local image and returns string result in format:
        /// img_name,Adult:score,Spoof:score,Violence:score,Medical:score
        /// </summary>
        /// <param name="img">Image object</param>
        /// <param name="imgName">image`s name</param>
        /// <returns>analysis result</returns>
        public static async Task<string> ModerateContent(Image img, string imgName)
        {
            var annotation = await Client.DetectSafeSearchAsync(img);
            // Each category is classified as Very Unlikely, Unlikely, Possible, Likely or Very Likely.
            var result = $"{imgName}," +
                $"Adult:{annotation.Adult}," +
                $"Spoof:{annotation.Spoof}," +
                $"Violence:{annotation.Violence}," +
                $"Medical:{annotation.Medical}\n";

            return result;
        }

        /// <summary>
        /// Runs Scene Classification task on provided local image and returns string result in format:
        /// img_name,{ label_class:score, ....}
        /// </summary>
        /// <param name="img">Image object</param>
        /// <param name="imgName">image`s name</param>
        /// <returns>analysis result</returns>
        public static async Task<string> SceneClassification(Image img, string imgName)
        {
            var labels = await Client.DetectLabelsAsync(img);
            var result = new StringBuilder($"{imgName},");
            foreach (var label in labels)
            {
                result.Append($"{label.Description}:{label.Score:0.00},");
            }

            result.Append("\n");
            return result.ToString();
        }

        /// <summary>
        /// Google Vision API does not support this feature :(
        /// </summary>
        /// <param name="img"></param>
        /// <param name="imgName"></param>
        /// <returns></returns>
        public static async Task<string> AgeGenderEstimation(Image img, string imgName)
        {
            return "";
        }

        /// <summary>
        /// Establish connection with API, sends images recommended for further analysis to API and
        /// stores results in dir picked by User. 
        /// </summary>
        /// <param name="mainWindow">main window of the app</param>
        /// <returns></returns>
        public static async Task RunApi(MainWindow mainWindow)
        {
            Finished = false;
            try
            {
                // Connects to API
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", mainWindow.GoogleAPIKeyPath);
                Client = ImageAnnotatorClient.Create();
            }
            catch (Exception e)
            {
                mainWindow.Output.AppendText(
                   $"[ERROR] Could not connect to Google Vision API Client{Environment.NewLine}");
                mainWindow.Output.AppendText($"[ERROR] {e.Message}{Environment.NewLine}");
                Finished = true;
                return;
            }

            // Age and Gender Estimation is not supported by Google
            foreach (var imgAnalysisTask in mainWindow.imgAnalysisTasks
                .Where(x => x.Checked && x.Name != "Age and Gender Estimation"))
            {
                using (var writer =
                            new StreamWriter(mainWindow.OutputDir + "\\google_" + imgAnalysisTask.OutputFileName))
                {
                    foreach (var (img, imgName) in YieldImages(
                        mainWindow.OutputDir + "\\" + imgAnalysisTask.ImgsForApiFileName, mainWindow.InputDir))
                    {
                        if (Cancelled)
                        {
                            Cancelled = false;
                            Finished = true;
                            return;
                        }

                        try
                        {
                            string annotation = await imgAnalysisTask.GoogleApiFunc(img, imgName);
                            await writer.WriteAsync(annotation);
                        }
                        catch (AnnotateImageException e)
                        {
                            mainWindow.Output.AppendText($"[ERROR] Google Vision API image annotation" +
                                $" request for {imgAnalysisTask.Name} task failed{Environment.NewLine}");

                            AnnotateImageResponse response = e.Response;
                            mainWindow.Output.AppendText($"[ERROR] {response.Error}{Environment.NewLine}");
                            Finished = true;
                            return;
                        }
                    }
                }
            }

            Finished = true;
        }
    }
}
