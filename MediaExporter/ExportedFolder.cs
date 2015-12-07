using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaExporter
{
    /// <summary>
    /// Represents a folder that can be exported and also contain files that can be exported.
    /// </summary>
    public class ExportedFolder : IComparable, IComparable<ExportedFolder>
    {        
        /// <summary>
        /// The path to this folder.
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// The partial path to the folder.
        /// </summary>
        private readonly string _partialPath;

        /// <summary>
        /// The folders that lie within this folder.
        /// </summary>
        private readonly SortedSet<ExportedFolder> _subFolders;

        /// <summary>
        /// The files that lie within this folder.
        /// </summary>
        private readonly SortedSet<ExportedFile> _files;

        /// <summary>
        /// Constructs a new instance of the ExportedFolder class.
        /// </summary>
        /// <param name="folderPath">The path to the folder.</param>
        /// <param name="basePath">The base of the export operation this folder is associated with.</param>
        public ExportedFolder(string folderPath, string basePath)
        {
            this._path = folderPath;
            this._partialPath = folderPath.Substring(basePath.Length);
            if (this._partialPath.StartsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
            {
                this._partialPath = this._partialPath.Substring(1);
            }

            this._subFolders = new SortedSet<ExportedFolder>();
            this._files = new SortedSet<ExportedFile>();
        }

        /// <summary>
        /// Gets the folder's path.
        /// </summary>
        public string Path
        {
            get
            {
                return this._path;
            }
        }

        /// <summary>
        /// Gets the collection of subfolders directly in this folder.
        /// </summary>
        public ICollection<ExportedFolder> SubFolders
        {
            get
            {
                return this._subFolders;
            }
        }

        /// <summary>
        /// Gets the collection of files directly in this folder.
        /// </summary>
        public ICollection<ExportedFile> Files
        {
            get
            {
                return this._files;
            }
        }

        public int CompareTo(object obj)
        {
            ExportedFolder folder = obj as ExportedFolder;
            if (folder == null)
            {
                throw new ArgumentException("Object not valid reference of ExportedFolder object.");
            }

            return this._partialPath.CompareTo(folder._partialPath);
        }

        public int CompareTo(ExportedFolder other)
        {
            return this._partialPath.CompareTo(other._partialPath);
        }

        public override bool Equals(object obj)
        {
            ExportedFolder folder = obj as ExportedFolder;
            if (folder == null)
            {
                return false;
            }

            return this._partialPath.Equals(folder._partialPath);
        }

        public override int GetHashCode()
        {
            return this._partialPath.GetHashCode();
        }
    }
}
