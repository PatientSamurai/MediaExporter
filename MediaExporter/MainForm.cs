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
        private void buttonExport_Click(object sender, EventArgs e)
        {
            this.textBoxOutput.Text = string.Empty;

            this.Export();

            this.OutputLine("Done.");
        }

        /// <summary>
        /// Performs the export process.
        /// </summary>
        private void Export()
        {
            string sourcePath = this.textBoxSourcePath.Text;
            string destinationPath = this.textBoxDestinationPath.Text;
            if (!Directory.Exists(sourcePath))
            {
                this.OutputLine("Source directory does not exist.");
                return;
            }

            if (!Directory.Exists(destinationPath))
            {
                this.OutputLine("Destination directory does not exist.");
                return;
            }

            var sourceFolder = new ExportedFolder(sourcePath);
            this.PopulateExportedFolder(sourceFolder);

            var destinationFolder = new ExportedFolder(destinationPath);
            this.PopulateExportedFolder(destinationFolder);
        }

        /// <summary>
        /// Takes an empty exportable folder and populates it with its files and sub folders.
        /// </summary>
        /// <param name="folder">The folder to populate.</param>
        private void PopulateExportedFolder(ExportedFolder folder)
        {
            foreach (string subFolder in Directory.GetDirectories(folder.Path))
            {
                Debug.WriteLine("Found subfolder: " + subFolder);
                var exportedFolder = new ExportedFolder(subFolder);
                this.PopulateExportedFolder(exportedFolder);
                folder.SubFolders.Add(exportedFolder);
            }

            foreach (string file in Directory.GetFiles(folder.Path))
            {
                Debug.WriteLine("Found file: " + file);
                var exportedFile = new ExportedFile(file);
                folder.Files.Add(exportedFile);
            }
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
                this.textBoxOutput.Text += "\n";
            }

            this.textBoxOutput.Text += line;
        }
    }
}
