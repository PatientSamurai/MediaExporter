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
        /// Constructs a new instance of the ExportedFile class.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        public ExportedFile(string filePath)
        {
            this._path = filePath;
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
            ExportedFile file = obj as ExportedFile;
            if (file == null)
            {
                throw new ArgumentException("Object not valid reference of ExportedFile object.");
            }

            return this._path.CompareTo(file._path);
        }

        public int CompareTo(ExportedFile other)
        {
            return this._path.CompareTo(other._path);
        }

        public override bool Equals(object obj)
        {
            ExportedFile file = obj as ExportedFile;
            if (file == null)
            {
                return false;
            }

            return this._path.Equals(file._path);
        }

        public override int GetHashCode()
        {
            return this._path.GetHashCode();
        }
    }
}
