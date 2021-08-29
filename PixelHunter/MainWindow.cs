using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixelHunter
{
    /// <summary>
    /// Class representing main window of Pixel Hunter.
    /// </summary>
    public partial class MainWindow : Form
    {
        public readonly List<ImageAnalysisTask> imgAnalysisTasks;
        public bool RunApis
        {
            get => cbGoogle.Checked || cbMicrosoft.Checked;
        }
        public string InputDir
        {
            get => tbInputDir.Text;
        }
        public string OutputDir
        {
            get => tbOutputDir.Text;
        }
        public TextBox Output
        {
            get => tbOutput;
        }
        public string GoogleAPIKeyPath { get; set; } = "";
        public (string, string) MSVisionAPIKeys { get; set; } = ("", "");

        /// <summary>
        /// Initializes all available Analysis Tasks with their properties and stores them
        /// within imgAnalysisTasks field.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            imgAnalysisTasks = new List<ImageAnalysisTask>();
            imgAnalysisTasks.Add(new ImageAnalysisTask(
                "Object Detection", @"..\..\..\models\object_det.py", "det_object.csv",
                "det_object_for_api.txt", cbObjectDet, GoogleAPI.DetectObjects,
                MSVisionAPI.ObjectDetection));

            imgAnalysisTasks.Add(new ImageAnalysisTask(
                "NSFW Detection", @"..\..\..\models\nsfw.py", "nsfw_det.csv",
                "nsfw_for_api.txt", cbNSFW, GoogleAPI.ModerateContent,
                MSVisionAPI.ModerateContent));

            imgAnalysisTasks.Add(new ImageAnalysisTask(
                "Scene Classification", @"..\..\..\models\scene_class.py", "scene_class.csv",
                "scene_class_for_api.txt", cbSceneClass, GoogleAPI.SceneClassification,
                MSVisionAPI.SceneClassification));

            imgAnalysisTasks.Add(new ImageAnalysisTask(
                "Age and Gender Estimation", @"..\..\..\models\age_gender.py", "age_gender.csv",
                "age_gender_for_api.txt", cbAgeGender, GoogleAPI.AgeGenderEstimation,
                MSVisionAPI.AgeGenderEstimation));
        }

        // If Browse Input Directory button is clicked
        private void BtInput_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog(this.ParentForm) != DialogResult.OK)
            {
                return;
            }

            tbInputDir.Text = dialog.SelectedPath;
        }

        // If Browse Output Directory button is clicked
        private void BtOutput_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog(this.ParentForm) != DialogResult.OK)
            {
                return;
            }

            tbOutputDir.Text = dialog.SelectedPath;
        }

        /// <summary>
        /// If Run Analysis button is clicked. Depending in the checked checkboxes, various scenarios
        /// might occur. At first it disables all UI except Cancel button. Then runs Inference with
        /// checked models on images found in InputDir. Results are stored in OutputDir as .csv files.
        /// Images chosen by inference on the free models, will be sent to APIs, checked by user, for 
        /// further analysis. Files with the chosen images for further analysis are stored in the OutputDir
        /// along with results from APIs. In the end, if requested, visual outputs are created based on the
        /// outputs of the Object Detection and Age and Gender Estimation models.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtRun_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(InputDir))
            {
                Output.AppendText($"[ERROR] Invalid input dir{Environment.NewLine}");
                return;
            }

            if (!Directory.Exists(OutputDir))
            {
                Output.AppendText($"[ERROR] Invalid output dir{Environment.NewLine}");
                return;
            }

            EnableDisableUI(enable: false);

            Output.AppendText($"{Environment.NewLine}");
            Output.AppendText($"---------------------------------------------{Environment.NewLine}");
            Output.AppendText($"[INFO] Analysis on the chosen models started {Environment.NewLine}");
            // Run Inference
            await Inference.RunInference(this);

            // Run APIs
            if (cbGoogle.Checked)
            {
                Output.AppendText($"[INFO] Sending chosen images to Google Vision API {Environment.NewLine}");
                await GoogleAPI.RunApi(this);
                Output.AppendText($"[INFO] Google Vision API analysis ended{Environment.NewLine}");
            }

            if (cbMicrosoft.Checked && GoogleAPI.Finished)
            {
                Output.AppendText(
                    $"[INFO] Sending chosen images to MS Vision API {Environment.NewLine}");
                await MSVisionAPI.RunApi(this);
                Output.AppendText($"[INFO] MS Vision API analysis ended{Environment.NewLine}");
            }

            // Create visual output
            if (GoogleAPI.Finished && MSVisionAPI.Finished && cbVisual.Checked)
            {
                Output.AppendText($"[INFO] Started creating visual outputs {Environment.NewLine}");
                foreach (var task in imgAnalysisTasks
                    .Where(x => x.Visualizible && x.Checked))
                {
                    try
                    {
                        ExtraOutputs.VisualOutput(task, OutputDir, InputDir);
                    }
                    catch (Exception ex)
                    {
                        Output.AppendText($"[ERROR] {ex.Message}{Environment.NewLine}");
                    }
                }
                Output.AppendText($"[INFO] Finished creating visual outputs {Environment.NewLine}");
            }

            Output.AppendText($"[INFO] Analysis ended{Environment.NewLine}{Environment.NewLine}");
            EnableDisableUI(enable: true);
        }

        /// <summary>
        /// If Cancel button is clicked. Button is enabled only during running analysis. If cliked, analysis
        /// is canceled immediately, Cancel button disabled and rest of the UI enabled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BCancelAnalysis_Click(object sender, EventArgs e)
        {
            Inference.Cancelled = true;
            GoogleAPI.Cancelled = true;
            MSVisionAPI.Cancelled = true;
            cbGoogle.Checked = false;
            cbMicrosoft.Checked = false;
            foreach (var process in Inference.Processes)
            {
                process.Value.Kill();
                process.Value.WaitForExit();
            }

            Output.AppendText($"[WARNING] Analysis cancelled by user{Environment.NewLine}");
            EnableDisableUI(enable: true);
        }

        // Disables/Enables buttons, check boxes etc. before/after running the analysis
        private void EnableDisableUI(bool enable)
        {
            bCancelAnalysis.Enabled = !enable;
            btInput.Enabled = enable;
            btOutput.Enabled = enable;
            btRun.Enabled = enable;
            cbAgeGender.Enabled = enable;
            cbNSFW.Enabled = enable;
            cbSceneClass.Enabled = enable;
            cbObjectDet.Enabled = enable;
            cbGoogle.Enabled = enable;
            cbMicrosoft.Enabled = enable;
            cbVisual.Enabled = enable;
            tbInputDir.Enabled = enable;
            tbOutputDir.Enabled = enable;
        }

        /// <summary>
        /// To check Google API checkbox, user must provide required credentials in separate winform.
        /// Check is successful iff some credentials were provided (their validity is not checked). 
        /// Otherwise Google API checkbox remains unchecked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbGoogle_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbGoogle.Checked)
            {
                GoogleAPIKeyPath = "";
                return;
            }

            if (GoogleAPIKeyPath != "")
            {
                return;
            }

            cbGoogle.Checked = false;
            var apiCredentialsForm = new APICredentialsForm();
            apiCredentialsForm.Label1.Text = "Path to Key:";
            apiCredentialsForm.Label2.Visible = false;
            apiCredentialsForm.TextBox2.Visible = false;
            apiCredentialsForm.SubmitButton.Click += (sender, e) =>
            {
                GoogleAPIKeyPath = apiCredentialsForm.TextBox1.Text;
                cbGoogle.Checked = true;
                Output.AppendText($"[INFO] Google Api credentials path {GoogleAPIKeyPath} saved{Environment.NewLine}");
                apiCredentialsForm.Close();
            };

            apiCredentialsForm.ShowDialog();
        }

        /// <summary>
        /// To check MS API checkbox, user must provide required credentials in separate winform.
        /// Check is successful iff some credentials were provided (their validity is not checked). 
        /// Otherwise MS API checkbox remains unchecked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbMicrosoft_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbMicrosoft.Checked)
            {
                MSVisionAPIKeys = ("", "");
                return;
            }

            if (MSVisionAPIKeys != ("", ""))
            {
                return;
            }

            cbMicrosoft.Checked = false;
            var apiCredentialsForm = new APICredentialsForm();
            apiCredentialsForm.BrowseButton.Visible = false;
            apiCredentialsForm.SubmitButton.Click += (sender, e) =>
            {
                MSVisionAPIKeys = (apiCredentialsForm.TextBox1.Text, apiCredentialsForm.TextBox2.Text);
                cbMicrosoft.Checked = true;
                Output.AppendText($"[INFO] Microsoft Api subscription key: {MSVisionAPIKeys.Item1}, " +
                    $"and endpoint: {MSVisionAPIKeys.Item2}, saved{Environment.NewLine}");
                apiCredentialsForm.Close();
            };

            apiCredentialsForm.ShowDialog();
        }
    }
}
