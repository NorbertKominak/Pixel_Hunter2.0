using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixelHunter
{
    /// <summary>
    /// Static class that provides RunApi method to run inference with the chosen models on images
    /// those path were provided by users. Inference of each chosen model is run by python script.
    /// </summary>
    public static class Inference
    {
        // In case Analysis was cancelled by user
        public static bool Cancelled { get; set; } = false;

        // Paths to Python .exe and scripts to run free models
        private const string PythonExePath = @"..\..\..\models\venv\Scripts\python.exe";

        public static Dictionary<string, Process> Processes { get; set; }

        /// <summary>
        /// Prepares Python script specified by scriptPath, to be run as separate process.
        /// </summary>
        /// <param name="scriptPath"> path to Python script</param>
        /// <param name="mainWindow"> main window of the app</param>
        /// <returns></returns>
        private static ProcessStartInfo PrepareTaskProcess(string scriptPath, MainWindow mainWindow)
        {
            var modelProcessInfo = new ProcessStartInfo();
            modelProcessInfo.FileName = PythonExePath;

            // --allow_api argument tells the script to store names of images recommended for further
            // analysis
            modelProcessInfo.Arguments = $"{scriptPath} " +
                            $"--input_dir {mainWindow.InputDir} " +
                            $"--output_dir {mainWindow.OutputDir} " +
                            "--allow_api " + (mainWindow.RunApis ? "True" : "False");

            modelProcessInfo.UseShellExecute = false;
            modelProcessInfo.CreateNoWindow = true;
            modelProcessInfo.ErrorDialog = true;
            return modelProcessInfo;
        }

        // Runs checkedModels inference in parrarel, asynchronously awaiting its finish
        /// <summary>
        /// Runs all models checked by user in the main window in parrarel as separate processes. 
        /// Asynchronously awaits their exit. 
        /// </summary>
        /// <param name="mainWindow">main window of the app</param>
        /// <returns></returns>
        public static async Task RunInference(MainWindow mainWindow)
        {
            Processes = new Dictionary<string, Process>();
            mainWindow.Output.AppendText(
                $"[INFO] Running inference on the chosen models{Environment.NewLine}");

            foreach (var imgAnalysisTask in mainWindow.imgAnalysisTasks.Where(x => x.Checked))
            {
                mainWindow.Output.AppendText(
                    $"[INFO] {imgAnalysisTask.Name} model is running{Environment.NewLine}");
                Processes.Add(imgAnalysisTask.Name,
                    Process.Start(PrepareTaskProcess(imgAnalysisTask.ScriptPath, mainWindow)));
            }

            foreach (var process in Processes)
            {
                await process.Value.WaitForExitAsync();
                if (Cancelled)
                {
                    Cancelled = false;
                    return;
                }
                mainWindow.Output.AppendText(
                    $"[INFO] {process.Key} model finished{Environment.NewLine}");
            }

            mainWindow.Output.AppendText($"[INFO] Inference finished{Environment.NewLine}");
        }
    }
}
