namespace PK_Finder.Classes
{
    /// <summary>
    /// Internal logic for handling Windows product information
    /// </summary>
    public sealed class KeyInfo
    {
        #region Properties
        /// <summary>
        /// Gets or sets the product name
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the product key
        /// </summary>
        public string ProductKey { get; set; }
        #endregion

        /// <summary>
        /// Initialize a new KeyInfo object
        /// </summary>
        public KeyInfo()
        {
            ProductKey = "";
            ProductName = "";
        }
    }
}
