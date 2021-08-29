
namespace PixelHunter
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbInputDir = new System.Windows.Forms.TextBox();
            this.btInput = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btOutput = new System.Windows.Forms.Button();
            this.tbOutputDir = new System.Windows.Forms.TextBox();
            this.cbObjectDet = new System.Windows.Forms.CheckBox();
            this.btRun = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbNSFW = new System.Windows.Forms.CheckBox();
            this.cbSceneClass = new System.Windows.Forms.CheckBox();
            this.cbAgeGender = new System.Windows.Forms.CheckBox();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.bCancelAnalysis = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cbGoogle = new System.Windows.Forms.CheckBox();
            this.cbMicrosoft = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbVisual = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(274, 684);
            this.panel1.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label6.Location = new System.Drawing.Point(44, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(217, 22);
            this.label6.TabIndex = 3;
            this.label6.Text = "Using Neural Networks\"";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Location = new System.Drawing.Point(7, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 22);
            this.label5.TabIndex = 2;
            this.label5.Text = "\"Image Analysis";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label4.Location = new System.Drawing.Point(0, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(198, 38);
            this.label4.TabIndex = 1;
            this.label4.Text = "Pixel Hunter";
            // 
            // tbInputDir
            // 
            this.tbInputDir.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbInputDir.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tbInputDir.Location = new System.Drawing.Point(331, 55);
            this.tbInputDir.Name = "tbInputDir";
            this.tbInputDir.Size = new System.Drawing.Size(393, 22);
            this.tbInputDir.TabIndex = 4;
            // 
            // btInput
            // 
            this.btInput.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btInput.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btInput.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btInput.Location = new System.Drawing.Point(774, 52);
            this.btInput.Name = "btInput";
            this.btInput.Size = new System.Drawing.Size(205, 25);
            this.btInput.TabIndex = 5;
            this.btInput.Text = "Browse Input Directory";
            this.btInput.UseVisualStyleBackColor = true;
            this.btInput.Click += new System.EventHandler(this.BtInput_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(331, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Set Input Directory:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(334, 273);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Set Output Directory:";
            // 
            // btOutput
            // 
            this.btOutput.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btOutput.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btOutput.Location = new System.Drawing.Point(774, 302);
            this.btOutput.Name = "btOutput";
            this.btOutput.Size = new System.Drawing.Size(205, 25);
            this.btOutput.TabIndex = 8;
            this.btOutput.Text = "Browse Output Directory";
            this.btOutput.UseVisualStyleBackColor = true;
            this.btOutput.Click += new System.EventHandler(this.BtOutput_Click);
            // 
            // tbOutputDir
            // 
            this.tbOutputDir.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbOutputDir.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tbOutputDir.Location = new System.Drawing.Point(331, 305);
            this.tbOutputDir.Name = "tbOutputDir";
            this.tbOutputDir.Size = new System.Drawing.Size(393, 22);
            this.tbOutputDir.TabIndex = 7;
            // 
            // cbObjectDet
            // 
            this.cbObjectDet.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbObjectDet.AutoSize = true;
            this.cbObjectDet.Location = new System.Drawing.Point(342, 225);
            this.cbObjectDet.Name = "cbObjectDet";
            this.cbObjectDet.Size = new System.Drawing.Size(205, 20);
            this.cbObjectDet.TabIndex = 10;
            this.cbObjectDet.Text = "Object Detection Efficient Det1";
            this.cbObjectDet.UseVisualStyleBackColor = true;
            // 
            // btRun
            // 
            this.btRun.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btRun.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btRun.Location = new System.Drawing.Point(928, 594);
            this.btRun.Name = "btRun";
            this.btRun.Size = new System.Drawing.Size(132, 23);
            this.btRun.TabIndex = 11;
            this.btRun.Text = "Run Analysis";
            this.btRun.UseVisualStyleBackColor = true;
            this.btRun.Click += new System.EventHandler(this.BtRun_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(331, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "Pick Models:";
            // 
            // cbNSFW
            // 
            this.cbNSFW.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbNSFW.AutoSize = true;
            this.cbNSFW.Location = new System.Drawing.Point(342, 134);
            this.cbNSFW.Name = "cbNSFW";
            this.cbNSFW.Size = new System.Drawing.Size(126, 20);
            this.cbNSFW.TabIndex = 13;
            this.cbNSFW.Text = "NSFW Detection";
            this.cbNSFW.UseVisualStyleBackColor = true;
            // 
            // cbSceneClass
            // 
            this.cbSceneClass.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbSceneClass.AutoSize = true;
            this.cbSceneClass.Location = new System.Drawing.Point(342, 164);
            this.cbSceneClass.Name = "cbSceneClass";
            this.cbSceneClass.Size = new System.Drawing.Size(147, 20);
            this.cbSceneClass.TabIndex = 14;
            this.cbSceneClass.Text = "Scene Classification";
            this.cbSceneClass.UseVisualStyleBackColor = true;
            // 
            // cbAgeGender
            // 
            this.cbAgeGender.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbAgeGender.AutoSize = true;
            this.cbAgeGender.Location = new System.Drawing.Point(342, 195);
            this.cbAgeGender.Name = "cbAgeGender";
            this.cbAgeGender.Size = new System.Drawing.Size(190, 20);
            this.cbAgeGender.TabIndex = 15;
            this.cbAgeGender.Text = "Age and Gender Estimation";
            this.cbAgeGender.UseVisualStyleBackColor = true;
            // 
            // tbOutput
            // 
            this.tbOutput.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbOutput.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tbOutput.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbOutput.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tbOutput.Location = new System.Drawing.Point(331, 419);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ReadOnly = true;
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbOutput.Size = new System.Drawing.Size(572, 237);
            this.tbOutput.TabIndex = 16;
            // 
            // bCancelAnalysis
            // 
            this.bCancelAnalysis.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bCancelAnalysis.Enabled = false;
            this.bCancelAnalysis.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bCancelAnalysis.Location = new System.Drawing.Point(928, 633);
            this.bCancelAnalysis.Name = "bCancelAnalysis";
            this.bCancelAnalysis.Size = new System.Drawing.Size(132, 23);
            this.bCancelAnalysis.TabIndex = 17;
            this.bCancelAnalysis.Text = "Cancel Analysis";
            this.bCancelAnalysis.UseVisualStyleBackColor = true;
            this.bCancelAnalysis.Click += new System.EventHandler(this.BCancelAnalysis_Click);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(694, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 17);
            this.label7.TabIndex = 18;
            this.label7.Text = "Pick APIs:";
            // 
            // cbGoogle
            // 
            this.cbGoogle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbGoogle.AutoSize = true;
            this.cbGoogle.Location = new System.Drawing.Point(704, 134);
            this.cbGoogle.Name = "cbGoogle";
            this.cbGoogle.Size = new System.Drawing.Size(173, 20);
            this.cbGoogle.TabIndex = 19;
            this.cbGoogle.Text = "Google Vision Cloud API";
            this.cbGoogle.UseVisualStyleBackColor = true;
            this.cbGoogle.CheckedChanged += new System.EventHandler(this.cbGoogle_CheckedChanged);
            // 
            // cbMicrosoft
            // 
            this.cbMicrosoft.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbMicrosoft.AutoSize = true;
            this.cbMicrosoft.Location = new System.Drawing.Point(704, 164);
            this.cbMicrosoft.Name = "cbMicrosoft";
            this.cbMicrosoft.Size = new System.Drawing.Size(181, 20);
            this.cbMicrosoft.TabIndex = 20;
            this.cbMicrosoft.Text = "Microsoft Computer Vision";
            this.cbMicrosoft.UseVisualStyleBackColor = true;
            this.cbMicrosoft.CheckedChanged += new System.EventHandler(this.cbMicrosoft_CheckedChanged);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(334, 350);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(134, 17);
            this.label8.TabIndex = 21;
            this.label8.Text = "Additional Outputs:";
            // 
            // cbVisual
            // 
            this.cbVisual.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbVisual.AutoSize = true;
            this.cbVisual.Location = new System.Drawing.Point(342, 381);
            this.cbVisual.Name = "cbVisual";
            this.cbVisual.Size = new System.Drawing.Size(153, 20);
            this.cbVisual.TabIndex = 22;
            this.cbVisual.Text = "Visual Output (.JPEG)";
            this.cbVisual.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1086, 682);
            this.Controls.Add(this.cbVisual);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbMicrosoft);
            this.Controls.Add(this.cbGoogle);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.bCancelAnalysis);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.cbAgeGender);
            this.Controls.Add(this.cbSceneClass);
            this.Controls.Add(this.cbNSFW);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btRun);
            this.Controls.Add(this.cbObjectDet);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btOutput);
            this.Controls.Add(this.tbOutputDir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btInput);
            this.Controls.Add(this.tbInputDir);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Name = "MainWindow";
            this.Text = "Pixel Hunter";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbInputDir;
        private System.Windows.Forms.Button btInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btOutput;
        private System.Windows.Forms.TextBox tbOutputDir;
        private System.Windows.Forms.CheckBox cbObjectDet;
        private System.Windows.Forms.Button btRun;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbNSFW;
        private System.Windows.Forms.CheckBox cbSceneClass;
        private System.Windows.Forms.CheckBox cbAgeGender;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.Button bCancelAnalysis;
        private System.Windows.Forms.CheckBox cbMicrosoft;
        private System.Windows.Forms.CheckBox cbGoogle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbVisual;
    }
}

