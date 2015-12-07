using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaExporter
{
    /// <summary>
    /// Represents a file that can be exported.
    /// </summary>
    public class ExportedFile : IComparable, IComparable<ExportedFile>
    {
        /// <summary>
        /// The path to this file.
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// The partial path to the file.
        /// </summary>
        private readonly string _partialPath;

        /// <summary>
        /// Constructs a new instance of the ExportedFile class.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <param name="basePath">The base of the export operation this folder is associated with.</param>
        public ExportedFile(string filePath, string basePath)
        {
            this._path = filePath;
            this._partialPath = filePath.Substring(basePath.Length);
            if (this._partialPath.StartsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
            {
                this._partialPath = this._partialPath.Substring(1);
            }
        }

        /// <summary>
        /// Gets the file's path.
        /// </summary>
        public string Path
        {
            get
            {
                return this._path;
            }
        }

        public int CompareTo(object obj)
        {
            // TODO: Comparison must include file contents
            ExportedFile file = obj as ExportedFile;
            if (file == null)
            {
                throw new ArgumentException("Object not valid reference of ExportedFile object.");
            }

            return this._partialPath.CompareTo(file._partialPath);
        }

        public int CompareTo(ExportedFile other)
        {
            return this._partialPath.CompareTo(other._partialPath);
        }

        public override bool Equals(object obj)
        {
            ExportedFile file = obj as ExportedFile;
            if (file == null)
            {
                return false;
            }

            return this._partialPath.Equals(file._partialPath);
        }

        public override int GetHashCode()
        {
            return this._partialPath.GetHashCode();
        }
    }
}
