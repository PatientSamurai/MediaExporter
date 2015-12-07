using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaExporter
{
    /// <summary>
    /// The primary program form.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Constructs a new instance of MainForm class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
            this.openFileDialogSourcePath.FileOk += openFileDialogSourcePath_FileOk;
            this.openFileDialogDestinationPath.FileOk += openFileDialogDestinationPath_FileOk;
        }

        /// <summary>
        /// Called when the "open" button next for the source path is clicked.
        /// </summary>
        private void buttonOpenSourcePath_Click(object sender, EventArgs e)
        {
            this.openFileDialogSourcePath.ShowDialog();
        }

        /// <summary>
        /// Called when the "open" button next for the destination path is clicked.
        /// </summary>
        private void buttonOpenDestinationPath_Click(object sender, EventArgs e)
        {
            this.openFileDialogDestinationPath.ShowDialog();
        }

        /// <summary>
        /// Called when the open file dialog for the source path is closed with a selection made.
        /// </summary>
        private void openFileDialogSourcePath_FileOk(object sender, CancelEventArgs e)
        {
            this.textBoxSourcePath.Text = Path.GetDirectoryName(this.openFileDialogSourcePath.FileName);
        }

        /// <summary>
        /// Called when the open file dialog for the destination path is closed with a selection made.
        /// </summary>
        private void openFileDialogDestinationPath_FileOk(object sender, CancelEventArgs e)
        {
            this.textBoxDestinationPath.Text = Path.GetDirectoryName(this.openFileDialogDestinationPath.FileName);
        }

        /// <summary>
        /// Called when the "export" button is clicked.
        /// </summary>
        private async void buttonExport_Click(object sender, EventArgs e)
        {
            this.textBoxOutput.Text = string.Empty;

            var exporter = new Exporter(this.textBoxSourcePath.Text, this.textBoxDestinationPath.Text);

            try
            {
                await exporter.Export(new Progress<string>(this.GetExportProgress));
            }
            catch (InvalidOperationException ex)
            {
                this.OutputLine(ex.Message);
            }

            this.OutputLine("Done.");
        }

        /// <summary>
        /// Progress method for adding a message to the status window.
        /// </summary>
        /// <param name="status">Line to add.</param>
        private void GetExportProgress(string status)
        {
            this.OutputLine(status);
        }

        /// <summary>
        /// Outputs a string to the form's status box.
        /// </summary>
        /// <param name="line">The line to add.</param>
        private void OutputLine(string line)
        {
            Debug.WriteLine(line);
            if (this.textBoxOutput.Text.Length != 0)
            {
                this.textBoxOutput.AppendText("\r\n");
            }

            this.textBoxOutput.AppendText(line);
        }
    }
}
