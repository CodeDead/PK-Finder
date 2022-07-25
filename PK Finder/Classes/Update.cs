namespace PK_Finder.Classes
{
    internal class Update
    {
        #region Properties
        /// <summary>
        /// Gets or sets the major version
        /// </summary>
        internal int MajorVersion { get; set; }

        /// <summary>
        /// Gets or sets the minor version
        /// </summary>
        internal int MinorVersion { get; set; }

        /// <summary>
        /// Gets or sets the build version
        /// </summary>
        internal int BuildVersion { get; set; }

        /// <summary>
        /// Gets or sets the revision version
        /// </summary>
        internal int RevisionVersion { get; set; }

        /// <summary>
        /// Gets or sets the direct update download URL
        /// </summary>
        internal string UpdateUrl { get; set; }

        /// <summary>
        /// Gets or sets the information URL
        /// </summary>
        internal string InfoUrl { get; set; }

        /// <summary>
        /// Gets or sets the update information string
        /// </summary>
        internal string UpdateInfo { get; set; }
        #endregion

        /// <summary>
        /// Initialize a new Update
        /// </summary>
        internal Update()
        {
            // Default constructor
        }
    }
}
