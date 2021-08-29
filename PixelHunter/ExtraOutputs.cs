using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelHunter
{
    /// <summary>
    /// Static class providing methods to create additional outputs to the base outputs provided
    /// by analysis.
    /// </summary>
    public static class ExtraOutputs
    {
        /// <summary>
        /// Draws rectangle with a label on provided image. 
        /// Expected coords order is left, top, right, bottom
        /// </summary>
        /// <param name="img">image</param>
        /// <param name="label">label for the rectangle</param>
        /// <param name="coords">coordinates of the rectangle</param>
        private static void DrawRectangle(Image img, string label, double[] coords)
        {
            int left = (int)(coords[0] < 1 ? coords[0] * img.Width : coords[0]);
            int top = (int)(coords[1] < 1 ? coords[1] * img.Height : coords[1]);
            int right = (int)(coords[2] < 1 ? coords[2] * img.Width : coords[2]);
            int bottom = (int)(coords[2] < 1 ? coords[3] * img.Height : coords[3]);


            using (Graphics g = Graphics.FromImage(img))
            {
                var blackPen = new Pen(Color.ForestGreen, 5);
                g.DrawRectangle(blackPen, left, top, right - left, bottom - top);

                Font drawFont = new Font("Arial", 20);
                SolidBrush drawBrush = new SolidBrush(Color.YellowGreen);
                Point drawPoint = new Point(left, top - 30);
                g.DrawString(label, drawFont, drawBrush, drawPoint);
            }
        }

        /// <summary>
        /// Parse comma separated values in a line and returns them as List
        /// </summary>
        /// <param name="line">line</param>
        /// <returns>Parsed values as List</returns>
        private static List<string> ParseLineForVisual(string line)
        {
            int start = 0;
            int end = 0;
            var parsedLine = new List<string>();
            while (start < line.Length)
            {
                end = line.IndexOf(',', start);
                if (end == -1)
                {
                    parsedLine.Add(line.Substring(start));
                    break;
                }

                parsedLine.Add(line.Substring(start, end - start));
                start = end + 1;
            }

            return parsedLine;
        }

        /// <summary>
        /// Reads tasks results in inputsDir and draws them into images found in imgDir. Modified
        /// images are stored in inputsDir
        /// </summary>
        /// <param name="task"></param>
        /// <param name="inputsDir"></param>
        /// <param name="imgDir"></param>
        public static void VisualOutput(ImageAnalysisTask task, string inputsDir, string imgDir)
        {
            var filesToVisualize = Directory.GetFiles(inputsDir)
                .Where(x => x.EndsWith(task.OutputFileName));

            foreach (var file in filesToVisualize)
            {
                // Prefix for file name to distinguish results of different models on the same task
                string prefix = "";
                if (file.Contains(@"\google_"))
                {
                    prefix = "google_";
                }

                if (file.Contains(@"\ms_"))
                {
                    prefix = "ms_";
                }
                prefix += task.OutputFileName.Substring(0, task.OutputFileName.IndexOf('.')) + "_";

                using (var reader = new StreamReader(file))
                {
                    string line;
                    var imgName = "";
                    Image img = null;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!line.Contains(',') || line.StartsWith("img_name"))
                        {
                            continue;
                        }

                        var parsedLine = ParseLineForVisual(line);
                        if (parsedLine.Count() != 7)
                        {
                            throw new Exception(
                                $"Parse error on file {file}. A line did not contain enough values");
                        }

                        // Several lines may refer to a single image
                        if (imgName != parsedLine[0])
                        {
                            img?.Save(inputsDir + "\\" + prefix + imgName);
                            imgName = parsedLine[0];
                            img = Image.FromFile(imgDir + "\\" + imgName);
                        }

                        var label = $"{parsedLine[1]}, {parsedLine[2]}";
                        var coords = new double[4];
                        int i = 0;
                        foreach (var value in parsedLine.GetRange(3, 4))
                        {
                            if (!Double.TryParse(value, out coords[i]))
                            {
                                throw new Exception(
                                    $"Parse error on file {file}. Could not parse bounding box`s coordinates");
                            }
                            i++;
                        }
                        DrawRectangle(img, label, coords);
                    }

                    img?.Save(inputsDir + "\\" + prefix + imgName);
                }
            }
        }
    }
}
