using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PK_Finder.Classes
{
    internal class UpdateManager
    {
        #region Variables
        /// <summary>
        /// Gets or sets the URL from which Update objects can be retrieved
        /// </summary>
        public string UpdateUrl { get; set; }
        #endregion

        /// <summary>
        /// Initialize a new UpdateManager
        /// </summary>
        /// <param name="updateUrl">The URL to retrieve Update objects from</param>
        internal UpdateManager(string updateUrl)
        {
            UpdateUrl = updateUrl;
        }

        /// <summary>
        /// Retrieve an Update from an external URL
        /// </summary>
        /// <returns>The Update object</returns>
        /// <exception cref="ArgumentNullException">When the UpdateUrl is null</exception>
        /// <exception cref="ArgumentException">When the UpdateUrl is empty</exception>
        internal Update GetUpdate()
        {
            if (UpdateUrl == null)
                throw new ArgumentNullException(nameof(UpdateUrl));
            if (UpdateUrl.Length == 0)
                throw new ArgumentException(nameof(UpdateUrl));

            using HttpClient client = new HttpClient();
            string content = client.GetStringAsync(UpdateUrl).Result;
            Update update = JsonConvert.DeserializeObject<Update>(content);
            return update;
        }

        /// <summary>
        /// Retrieve an Update asynchronously from an external URL
        /// </summary>
        /// <returns>The Update object</returns>
        /// <exception cref="ArgumentNullException">When the UpdateUrl is null</exception>
        /// <exception cref="ArgumentException">When the UpdateUrl is empty</exception>
        internal async Task<Update> GetUpdateAsync()
        {
            if (UpdateUrl == null)
                throw new ArgumentNullException(nameof(UpdateUrl));
            if (UpdateUrl.Length == 0)
                throw new ArgumentException(nameof(UpdateUrl));

            using HttpClient client = new HttpClient();
            string content = await client.GetStringAsync(UpdateUrl);
            Update update = JsonConvert.DeserializeObject<Update>(content);
            return update;
        }

        /// <summary>
        /// Check if an update is available
        /// </summary>
        /// <param name="applicationVersion">The current application version</param>
        /// <param name="update">The Update object</param>
        /// <returns>True if an update is available, otherwise false</returns>
        /// <exception cref="ArgumentNullException">When the applicationVersion or update variable is null</exception>
        internal bool CheckForUpdate(Version applicationVersion, Update update)
        {
            if (applicationVersion == null)
                throw new ArgumentNullException(nameof(applicationVersion));
            if (update == null)
                throw new ArgumentNullException(nameof(update));

            int result = new Version(update.MajorVersion, update.MinorVersion, update.BuildVersion, update.RevisionVersion)
                .CompareTo(applicationVersion);
            return result > 0;
        }
    }
}
