using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
        /// The cancellation token for the current running export if one is occurring.
        /// </summary>
        private CancellationTokenSource _exportCancelTokenSource;

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
        /// Updates the enabled value of all the form's input elements to match the given parameter and sets the abort button to the opposite.
        /// </summary>
        /// <pparam name="shouldInputBeEnabled">Whether or not input should be enabled.</pparam>
        private void UpdateInputFormsEnabledness(bool shouldInputBeEnabled)
        {
            this.textBoxSourcePath.Enabled = shouldInputBeEnabled;
            this.textBoxDestinationPath.Enabled = shouldInputBeEnabled;
            this.buttonOpenSourcePath.Enabled = shouldInputBeEnabled;
            this.buttonOpenDestinationPath.Enabled = shouldInputBeEnabled;
            this.buttonExport.Enabled = shouldInputBeEnabled;
            this.buttonAbort.Enabled = !shouldInputBeEnabled;
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
            this._exportCancelTokenSource = new CancellationTokenSource();

            this.UpdateInputFormsEnabledness(false);

            try
            {
                await exporter.Export(new Progress<string>(this.GetExportProgress), this._exportCancelTokenSource.Token);
            }
            catch (InvalidOperationException ex)
            {
                this.OutputLine(ex.Message);
            }
            catch (OperationCanceledException)
            {
                this.OutputLine("Operation has been cancelled.");
            }

            this._exportCancelTokenSource = null;

            this.UpdateInputFormsEnabledness(true);
            this.OutputLine("Done.");
        }

        /// <summary>
        /// Called when "abourt" is clicked.
        /// </summary>
        private void buttonAbort_Click(object sender, EventArgs e)
        {
            if (this._exportCancelTokenSource != null)
            {
                this._exportCancelTokenSource.Cancel();
            }
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
