using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixelHunter
{
    /// <summary>
    /// Class representing form used only for getting API credentials.
    /// </summary>
    public partial class APICredentialsForm : Form
    {
        public Label Label1 { get; }
        public Label Label2 { get; }
        public TextBox TextBox1 { get; }
        public TextBox TextBox2 { get; }
        public Button BrowseButton { get; }
        public Button SubmitButton { get; }
        public APICredentialsForm()
        {
            InitializeComponent();
            Label1 = label1;
            Label2 = label2;
            TextBox1 = textBox1;
            TextBox2 = textBox2;
            BrowseButton = btBrowse;
            SubmitButton = btSubmit;
        }

        // If Browse button is clicked
        private void btBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog(this.ParentForm) != DialogResult.OK)
            {
                return;
            }

            TextBox1.Text = dialog.FileName;
        }

        /// <summary>
        /// Ensures that empty credentials will not be submitted. Submit button is enabled iff all 
        /// visible textBoxes are not empty.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            btSubmit.Enabled = TextBox1.Text.Length != 0 && 
                (TextBox2.Visible == false || TextBox2.Text.Length != 0);
        }

        /// <summary>
        /// Ensures that empty credentials will not be submitted. Submit button is enabled iff all 
        /// visible textBoxes are not empty.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            btSubmit.Enabled = TextBox1.Text.Length != 0 &&
                (TextBox2.Visible == false || TextBox2.Text.Length != 0);
        }
    }
}
