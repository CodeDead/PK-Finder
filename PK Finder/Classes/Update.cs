using Newtonsoft.Json;

namespace PK_Finder.Classes
{
    internal class Update
    {
        #region Properties
        /// <summary>
        /// Gets or sets the major version
        /// </summary>
        [JsonProperty]
        internal int MajorVersion { get; set; }

        /// <summary>
        /// Gets or sets the minor version
        /// </summary>
        [JsonProperty]
        internal int MinorVersion { get; set; }

        /// <summary>
        /// Gets or sets the build version
        /// </summary>
        [JsonProperty]
        internal int BuildVersion { get; set; }

        /// <summary>
        /// Gets or sets the revision version
        /// </summary>
        [JsonProperty]
        internal int RevisionVersion { get; set; }

        /// <summary>
        /// Gets or sets the direct update download URL
        /// </summary>
        [JsonProperty]
        internal string UpdateUrl { get; set; }

        /// <summary>
        /// Gets or sets the information URL
        /// </summary>
        [JsonProperty]
        internal string InfoUrl { get; set; }

        /// <summary>
        /// Gets or sets the update information string
        /// </summary>
        [JsonProperty]
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
