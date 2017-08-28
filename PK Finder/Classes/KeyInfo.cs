using System;

namespace PK_Finder.Classes
{
    /// <summary>
    /// Internal logic for handling Windows product information
    /// </summary>
    internal sealed class KeyInfo
    {
        /// <summary>
        /// The product name
        /// </summary>
        private string _productName;
        /// <summary>
        /// The product key
        /// </summary>
        private string _productKey;

        /// <summary>
        /// Initialize a new KeyInfo object
        /// </summary>
        internal KeyInfo()
        {
            _productKey = "";
            _productName = "";
        }

        /// <summary>
        /// Set the product name
        /// </summary>
        /// <param name="name">The name of the product</param>
        internal void SetProductName(string name)
        {
            _productName = name;
        }

        /// <summary>
        /// Get the current product name
        /// </summary>
        /// <returns>The product name</returns>
        internal string GetProductName()
        {
            return _productName;
        }

        /// <summary>
        /// Set the product key
        /// </summary>
        /// <param name="key">The product key</param>
        internal void SetProductKey(string key)
        {
            _productKey = key;
        }

        /// <summary>
        /// Get the current product key
        /// </summary>
        /// <returns>The product key</returns>
        internal string GetProductKey()
        {
            return _productKey;
        }

        /// <summary>
        /// Convert the product name and product key into an extractable string
        /// </summary>
        /// <returns>A string that can be written to the disk</returns>
        internal string GetReadableString()
        {
            if (_productName == null || _productKey == null) throw new ArgumentException("Product name or product key is empty!");
            return _productName + Environment.NewLine + "Product key: " + _productKey;
        }
    }
}
