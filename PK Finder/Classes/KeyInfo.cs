using System;

namespace PK_Finder.Classes
{
    /// <summary>
    /// Internal logic for handling Windows product information
    /// </summary>
    internal class KeyInfo
    {
        private string _productName;
        private string _productKey;

        internal void SetProductName(string name)
        {
            _productName = name;
        }

        internal string GetProductName()
        {
            return _productName;
        }

        internal void SetProductKey(string key)
        {
            _productKey = key;
        }

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
