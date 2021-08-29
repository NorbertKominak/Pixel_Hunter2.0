using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelHunter
{
    /// <summary>
    /// Static class providing methods to send local images for analysis to Microsoft Computer Vision 
    /// using its API and store results locally.
    /// </summary>
    public static class MSVisionAPI
    {
        // In case Analysis was cancelled by user
        public static bool Cancelled { get; set; } = false;
        // False if any communication with Api is running
        public static bool Finished { get; set; } = true;
        // Client to communicate with API
        public static ComputerVisionClient Client { get; set; }

        /// <summary>
        /// Yields images whose names are in imgsForApi file.
        /// </summary>
        /// <param name="imgsForApi">path to file with images names to yield</param>
        /// <returns></returns>
        private static IEnumerable<string> YieldImages(string imgsForApi)
        {
            if (File.Exists(imgsForApi))
            {
                using (StreamReader reader = new StreamReader(imgsForApi))
                {
                    string imgName;
                    while ((imgName = reader.ReadLine()) != null)
                    {
                        yield return imgName;
                    }
                }
            }
        }

        /// <summary>
        /// Runs NSFW Detection task on provided local image and returns string result in format:
        /// img_name,adult:score,racy:score
        /// </summary>
        /// <param name="img">stream with the image</param>
        /// <param name="imgName">image`s name</param>
        /// <returns>analysis result</returns>
        public static async Task<string> ModerateContent(Stream img, string imgName)
        {
            var features = new List<VisualFeatureTypes?> { VisualFeatureTypes.Adult };
            var annotations = await Client.AnalyzeImageInStreamAsync(img, visualFeatures: features);
            var result = imgName;

            if (annotations.Adult == null)
            {
                return result + "\n";
            }

            result += $",adult:{annotations.Adult.AdultScore:0.00}," +
                $"racy:{annotations.Adult.RacyScore:0.00}\n";

            return result;
        }

        /// <summary>
        /// Runs Scene Classification task on provided local image and returns string result in format:
        /// img_name,{ label_class:score, ....}
        /// </summary>
        /// <param name="img">stream with image</param>
        /// <param name="imgName">image`s name</param>
        /// <returns>analysis result</returns>
        public static async Task<string> SceneClassification(Stream img, string imgName)
        {
            var features = new List<VisualFeatureTypes?> { VisualFeatureTypes.Tags };
            var annotations = await Client.AnalyzeImageInStreamAsync(img, visualFeatures: features);
            var result = new StringBuilder(imgName);

            if (annotations.Tags == null)
            {
                return result + "\n";
            }

            foreach (var tag in annotations.Tags)
            {
                result.Append($",{tag.Name}:{tag.Confidence:0.00}");
            }

            return result + "\n";
        }

        /// <summary>
        /// Runs Age and Gender Estimation task on provided local image and returns string result in format:
        /// line = img_name,age,gender,left,top,right,bottom(bounding boxe`s edges)
        /// where each line represents single detected face
        /// </summary>
        /// <param name="img">stream with image</param>
        /// <param name="imgName">image`s name</param>
        /// <returns>analysis result</returns>
        public static async Task<string> AgeGenderEstimation(Stream img, string imgName)
        {
            var features = new List<VisualFeatureTypes?> { VisualFeatureTypes.Faces };
            var annotations = await Client.AnalyzeImageInStreamAsync(img, visualFeatures: features);
            var result = new StringBuilder();

            if (annotations.Faces == null)
            {
                return imgName + "\n";
            }

            foreach (var face in annotations.Faces)
            {
                int left = face.FaceRectangle.Left;
                int top = face.FaceRectangle.Top;
                int right = face.FaceRectangle.Left + face.FaceRectangle.Width;
                int bottom = face.FaceRectangle.Top + face.FaceRectangle.Height;

                result.Append($"{imgName},{face.Age},{face.Gender},{left},{top},{right},{bottom}\n");
            }

            return result.ToString();
        }

        /// <summary>
        /// Runs Object Detection task on provided local image and returns string result in format:
        /// line = img_name,class_name,score,left,top,right,bottom(bounding box edges)
        /// where each line represents a single detected object. If none object was detected only
        /// img_name is returned. 
        /// </summary>
        /// <param name="img">stream with image</param>
        /// <param name="imgName">image`s name</param>
        /// <returns>analysis result</returns>
        public static async Task<string> ObjectDetection(Stream img, string imgName)
        {
            var features = new List<VisualFeatureTypes?> { VisualFeatureTypes.Objects };
            var annotations = await Client.AnalyzeImageInStreamAsync(img, visualFeatures: features);
            var result = new StringBuilder();

            if (annotations.Objects == null)
            {
                return imgName + "\n";
            }

            foreach (var object_ in annotations.Objects)
            {
                int left = object_.Rectangle.X;
                int top = object_.Rectangle.Y;
                int right = object_.Rectangle.X + object_.Rectangle.W;
                int bottom = object_.Rectangle.Y + object_.Rectangle.H;

                result.Append($"{imgName},{object_.ObjectProperty}," +
                    $"{object_.Confidence:0.00},{left},{top},{right},{bottom}\n");
            }

            return result.ToString();
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
                // Connect to API
                Client = new ComputerVisionClient(
                    new ApiKeyServiceClientCredentials(mainWindow.MSVisionAPIKeys.Item1))
                { Endpoint = mainWindow.MSVisionAPIKeys.Item2 };
            }
            catch (Exception e)
            {
                mainWindow.Output.AppendText(
                    $"[ERROR] Could not connect to MS Vision API Client{Environment.NewLine}");
                mainWindow.Output.AppendText($"{e.Message}{Environment.NewLine}");
                Finished = true;
                return;
            }

            foreach (var imgAnalysisTask in mainWindow.imgAnalysisTasks
                .Where(x => x.Checked))
            {
                using (var writer =
                    new StreamWriter(mainWindow.OutputDir + "\\ms_" + imgAnalysisTask.OutputFileName))
                {
                    foreach (var imgName in YieldImages(
                        mainWindow.OutputDir + "\\" + imgAnalysisTask.ImgsForApiFileName))
                    {
                        if (Cancelled)
                        {
                            Cancelled = false;
                            Finished = true;
                            return;
                        }

                        using (var img = File.OpenRead(mainWindow.InputDir + "\\" + imgName))
                        {
                            try
                            {
                                var annotation = await imgAnalysisTask.MSApiFunc(img, imgName);
                                await writer.WriteAsync(annotation);
                            }
                            catch (Exception e)
                            {
                                mainWindow.Output.AppendText($"[ERROR] MS Vision API image annotation " +
                                    $"request for {imgAnalysisTask.Name} task failed{Environment.NewLine}");
                                mainWindow.Output.AppendText($"[ERROR] {e.Message}{Environment.NewLine}");
                                Finished = true;
                                return;
                            }
                        }
                    }
                }
            }

            Finished = true;
        }
    }
}
