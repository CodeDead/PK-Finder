﻿using System;
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
        /// Export data to a specific location
        /// </summary>
        /// <param name="path">The path where the data should be stored</param>
        /// <param name="content">The content that should be stored in the file</param>
        private static void Export(string path, string content)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(content);
            }
        }

        /// <summary>
        /// Export the KeyInfo object to a storage device in plain text format
        /// </summary>
        /// <param name="path">The path where the KeyInfo object can be stored in plain text format</param>
        internal void ExportToTxt(string path)
        {
            string content = "Product name: " + _keyInfo.GetProductName() + Environment.NewLine + "Product key: " + _keyInfo.GetProductKey();
            Export(path, content);
        }

        /// <summary>
        /// Export the KeyInfo object to a storage device in CSV format
        /// </summary>
        /// <param name="path">The path where the KeyInfo object can be stored in CSV format</param>
        internal void ExportToCsv(string path)
        {
            ExportDelimiter(path, ",");
        }

        internal void ExportToExcel(string path)
        {
            ExportDelimiter(path, ";");
        }

        /// <summary>
        /// Export the KeyInfo objecty to a storage device using a specific delimiter character
        /// </summary>
        /// <param name="path">The path where the KeyInfo object can be stored</param>
        /// <param name="delimiter">The delimiter that should be used to split the data</param>
        private void ExportDelimiter(string path, string delimiter)
        {
            string content = "Product name" + delimiter + "Product key" + Environment.NewLine + _keyInfo.GetProductName() + delimiter + _keyInfo.GetProductKey();
            Export(path, content);
        }

        /// <summary>
        /// Export the KeyInfo object to a storage device in HTML format
        /// </summary>
        /// <param name="path">The path where the KeyInfo object can be stored in HTML format</param>
        internal void ExportToHtml(string path)
        {
            string content = "<html><head><title>PK Finder</title></head><body><table border='1'><tr><th>Product name</th><th>Product key</th></tr><tr><td>" + _keyInfo.GetProductName() + "</td><td>" + _keyInfo.GetProductKey() + "</td></tr></table></body></html>";
            Export(path, content);
        }
    }
}