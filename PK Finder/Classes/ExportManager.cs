using System;
using System.IO;

namespace PK_Finder.Classes
{
    /// <summary>
    /// Internal logic for exporting a KeyInfo object
    /// </summary>
    internal sealed class ExportManager
    {
        /// <summary>
        /// The KeyInfo object that can be used by the internal methods for writing information to a storage device
        /// </summary>
        private readonly KeyInfo _keyInfo;

        /// <summary>
        /// Initialize a new ExportManager object
        /// </summary>
        /// <param name="keyInfo">The KeyInfo object that needs to be exported</param>
        internal ExportManager(KeyInfo keyInfo)
        {
            _keyInfo = keyInfo ?? throw new ArgumentNullException(nameof(keyInfo));
        }

        /// <summary>
        /// Export the KeyInfo object to a storage device in plain text format
        /// </summary>
        /// <param name="path">The path where the KeyInfo object can be stored in plain text format</param>
        internal void ExportToTxt(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("Product name: " + _keyInfo.GetProductName());
                sw.WriteLine("Product key: " + _keyInfo.GetProductKey());
            }
        }

        /// <summary>
        /// Export the KeyInfo object to a storage device in CSV format
        /// </summary>
        /// <param name="path">The path where the KeyInfo object can be stored in CSV format</param>
        internal void ExportToCsv(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("Product name,Product key");
                sw.WriteLine(_keyInfo.GetProductName() + "," + _keyInfo.GetProductKey());
            }
        }

        /// <summary>
        /// Export the KeyInfo object to a storage device in HTML format
        /// </summary>
        /// <param name="path">The path where the KeyInfo object can be stored in HTML format</param>
        internal void ExportToHtml(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("<html>");
                sw.WriteLine("<head><title>PK Finder</title></head>");
                sw.WriteLine("<body>");
                sw.WriteLine("<table border='1'>");
                sw.WriteLine("<tr><th>Product name</th><th>Product key</th></tr>");
                sw.WriteLine("<tr><td>" + _keyInfo.GetProductName() + "</td><td>" + _keyInfo.GetProductKey() + "</td></tr>");
                sw.WriteLine("</table>");
                sw.WriteLine("</body>");
                sw.WriteLine("</html>");
            }
        }
    }
}
