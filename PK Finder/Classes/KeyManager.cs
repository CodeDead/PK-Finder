using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32;

namespace PK_Finder.Classes
{
    /// <summary>
    /// Find the product key for various versions of Windows automatically
    /// </summary>
    internal static class KeyManager
    {
        /// <summary>
        /// Find the product key and name associated with the current Windows installation
        /// </summary>
        /// <returns>A KeyInfo object with the current product key and product name</returns>
        internal static KeyInfo GetWindowsProductInformation()
        {
            RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
            const string keyPath = @"Software\Microsoft\Windows NT\CurrentVersion";
            RegistryKey openSubKey = key.OpenSubKey(keyPath);
            if (openSubKey == null) return null;

            byte[] digitalProductId = (byte[])openSubKey.GetValue("DigitalProductId");
            bool isWin8OrUp = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor >= 2 || Environment.OSVersion.Version.Major > 6;

            string productKey = isWin8OrUp ? DecodeProductKeyWin8AndUp(digitalProductId) : DecodeProductKey(digitalProductId);
            string productName = (string)openSubKey.GetValue("ProductName");

            KeyInfo ki = new KeyInfo();

            ki.SetProductKey(productKey);
            ki.SetProductName(productName);

            return ki;
        }

        /// <summary>
        /// Decode the product key for Windows 8 and above
        /// </summary>
        /// <param name="digitalProductId">The digital product id found in the registry</param>
        /// <returns>A readable version of the digital product id</returns>
        private static string DecodeProductKeyWin8AndUp(IList<byte> digitalProductId)
        {
            string key = string.Empty;
            const int keyOffset = 52;
            byte isWin8 = (byte)((digitalProductId[66] / 6) & 1);
            digitalProductId[66] = (byte)((digitalProductId[66] & 0xf7) | (isWin8 & 2) * 4);

            const string digits = "BCDFGHJKMPQRTVWXY2346789";
            const string insert = "N";

            int last = 0;
            for (int i = 24; i >= 0; i--)
            {
                int current = 0;
                for (int j = 14; j >= 0; j--)
                {
                    current = current * 256;
                    current = digitalProductId[j + keyOffset] + current;
                    digitalProductId[j + keyOffset] = (byte)(current / 24);
                    current = current % 24;
                    last = current;
                }
                key = digits[current] + key;
            }
            string keyPart = key.Substring(1, last);
            key = key.Substring(1).Replace(keyPart, keyPart + insert);
            if (last == 0) key = insert + key;
            for (int i = 5; i < key.Length; i += 6)
            {
                key = key.Insert(i, "-");
            }
            return key;
        }

        /// <summary>
        /// Decode the product key for Windows versions below 8
        /// </summary>
        /// <param name="digitalProductId">The digital product id found in the registry</param>
        /// <returns>A readable version of the digital product id</returns>
        private static string DecodeProductKey(IReadOnlyList<byte> digitalProductId)
        {
            const int keyStartIndex = 52;
            const int keyEndIndex = keyStartIndex + 15;
            char[] digits = { 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'M', 'P', 'Q', 'R', 'T', 'V', 'W', 'X', 'Y', '2', '3', '4', '6', '7', '8', '9' };
            const int decodeLength = 29;
            const int decodeStringLength = 15;
            char[] decodedChars = new char[decodeLength];
            ArrayList hexPid = new ArrayList();
            for (int i = keyStartIndex; i <= keyEndIndex; i++)
            {
                hexPid.Add(digitalProductId[i]);
            }
            for (int i = decodeLength - 1; i >= 0; i--)
            {
                if ((i + 1) % 6 == 0)
                {
                    decodedChars[i] = '-';
                }
                else
                {
                    int digitMapIndex = 0;
                    for (int j = decodeStringLength - 1; j >= 0; j--)
                    {
                        int byteValue = (digitMapIndex << 8) | (byte)hexPid[j];
                        hexPid[j] = (byte)(byteValue / 24);
                        digitMapIndex = byteValue % 24;
                        decodedChars[i] = digits[digitMapIndex];
                    }
                }
            }
            return new string(decodedChars);
        }
    }
}
