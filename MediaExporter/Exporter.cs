using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaExporter
{
    /// <summary>
    /// Class to do the actual job of exporting.
    /// </summary>
    public class Exporter
    {
        /// <summary>
        /// The source of the export operation.
        /// </summary>
        private readonly string _sourceDirectory;

        /// <summary>
        /// The destination of the export operation.
        /// </summary>
        private readonly string _destinationDirectory;

        /// <summary>
        /// The asyncronous progress reporter.
        /// </summary>
        private IProgress<string> _progress;

        /// <summary>
        /// Token to indicate a cancel was requested.
        /// </summary>
        private CancellationToken _cancelToken;

        /// <summary>
        /// Creates a new instance of the Exporter class.
        /// </summary>
        /// <param name="source">Where to copy files from.</param>
        /// <param name="destination">Where to copy files to.</param>
        public Exporter(string source, string destination)
        {
            _sourceDirectory = source;
            _destinationDirectory = destination;
        }

        /// <summary>
        /// Performs the export operation.
        /// </summary>
        /// <param name="progress">The progress method to report back status to.</param>
        /// <param name="cancelToken">Cancellation token for the task.</param>
        public async Task Export(IProgress<string> progress, CancellationToken cancelToken)
        {
            this._progress = progress;
            this._cancelToken = cancelToken;

            await Task.Run(new Action(this.ExportSync));
        }

        /// <summary>
        /// Performs the export operation syncronously.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if an error occurs during export.</exception>
        private void ExportSync()
        {
            if (!Directory.Exists(_sourceDirectory))
            {
                throw new InvalidOperationException("Source directory does not exist.");
            }

            if (!Directory.Exists(_destinationDirectory))
            {
                throw new InvalidOperationException("Destination directory does not exist.");
            }

            _progress.Report("Populating source.");
            var sourceFolder = new ExportedFolder(_sourceDirectory, this._sourceDirectory);
            this.PopulateExportedFolder(sourceFolder, this._sourceDirectory);

            _progress.Report("Populating destination.");
            var destinationFolder = new ExportedFolder(_destinationDirectory, this._destinationDirectory);
            this.PopulateExportedFolder(destinationFolder, this._destinationDirectory);

            this.ExportCopy(sourceFolder, destinationFolder);
        }

        /// <summary>
        /// Performs the actual copying of the export process.
        /// </summary>
        /// <param name="source">The source folder.</param>
        /// <param name="destination">The folder to copy to.</param>
        private void ExportCopy(ExportedFolder source, ExportedFolder destination)
        {
            // Delete files in destination that are not in source
            foreach (ExportedFile destinationFile in destination.Files)
            {
                if (!source.Files.Contains(destinationFile))
                {
                    this._progress.Report("Deleting file: " + destinationFile.Path);
                    File.Delete(destinationFile.Path);
                }

                this._cancelToken.ThrowIfCancellationRequested();
            }

            // Delete folders from the destination that should not be there
            foreach (ExportedFolder destinationFolder in destination.SubFolders)
            {
                if (!source.SubFolders.Contains(destinationFolder))
                {
                    this._progress.Report("Deleting folder: " + destinationFolder.Path);
                    Directory.Delete(destinationFolder.Path, true);
                }

                this._cancelToken.ThrowIfCancellationRequested();
            }

            // Copy over files that need to be copied over
            foreach (ExportedFile sourceFile in source.Files)
            {
                if (!destination.Files.Contains(sourceFile))
                {
                    this._progress.Report("Copying file: " + sourceFile.Path);
                    File.Copy(sourceFile.Path, Path.Combine(destination.Path, Path.GetFileName(sourceFile.Path)));
                }

                this._cancelToken.ThrowIfCancellationRequested();
            }

            // Copy sub folders that need to be copied
            foreach (ExportedFolder sourceFolder in source.SubFolders)
            {
                ExportedFolder destinationFolder;
                if (destination.SubFolders.Contains(sourceFolder))
                {
                    destinationFolder = destination.SubFolders.First(destinationSubFolder => destinationSubFolder.Equals(sourceFolder));
                }
                else
                {
                    string folderToCreate = Path.Combine(destination.Path, sourceFolder.Path.Split(Path.DirectorySeparatorChar).Last());
                    Directory.CreateDirectory(folderToCreate);
                    destinationFolder = new ExportedFolder(folderToCreate, this._destinationDirectory);
                }

                // Recurse!!
                this.ExportCopy(sourceFolder, destinationFolder);

                this._cancelToken.ThrowIfCancellationRequested();
            }
        }

        /// <summary>
        /// Takes an empty exportable folder and populates it with its files and sub folders.
        /// </summary>
        /// <param name="folder">The folder to populate.</param>
        private void PopulateExportedFolder(ExportedFolder folder, string basePath)
        {
            foreach (string subFolder in Directory.GetDirectories(folder.Path))
            {
                Debug.WriteLine("Found subfolder: " + subFolder);
                var exportedFolder = new ExportedFolder(subFolder, basePath);
                this.PopulateExportedFolder(exportedFolder, basePath);
                folder.SubFolders.Add(exportedFolder);
                this._cancelToken.ThrowIfCancellationRequested();
            }

            foreach (string file in Directory.GetFiles(folder.Path))
            {
                Debug.WriteLine("Found file: " + file);
                var exportedFile = new ExportedFile(file, basePath);
                folder.Files.Add(exportedFile);
                this._cancelToken.ThrowIfCancellationRequested();
            }
        }
    }
}
