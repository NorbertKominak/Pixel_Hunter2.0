using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Vision.V1;

namespace PixelHunter
{
    /// <summary>
    /// Class representing single Image Analysis Task supported by this app (e.g. NSFW Detection)
    /// </summary>
    public class ImageAnalysisTask
    {
        // Api functions providing this task
        public Func<Image, string, Task<string>> GoogleApiFunc { get; }
        public Func<Stream, string, Task<string>> MSApiFunc { get; }
        public string Name { get; }
        // Path to python script for inference
        public string ScriptPath { get; }
        // File with image`s names recommended for API analysis
        public string ImgsForApiFileName { get; }
        public string OutputFileName { get; }
        public bool Checked
        {
            get => tasksCheckBox.Checked;
        }
        // Whether task`s results can have visual output (drawn bounding boxes)
        public bool Visualizible
        {
            get => (Name == "Age and Gender Estimation") || (Name == "Object Detection");
        }

        // check box related to the task
        private CheckBox tasksCheckBox;

        public ImageAnalysisTask(string name, string scriptPath, string apiOutputFileName,
            string imgsForApiFileName, CheckBox tasksCheckBox, 
            Func<Image, string, Task<string>> googleApi, Func<Stream, string, Task<string>> msApi)
        {
            Name = name;
            ScriptPath = scriptPath;
            OutputFileName = apiOutputFileName;
            ImgsForApiFileName = imgsForApiFileName;
            GoogleApiFunc = googleApi;
            MSApiFunc = msApi;
            this.tasksCheckBox = tasksCheckBox;
        }
    }
}
