﻿using System.Windows.Data;

namespace PK_Finder.Classes
{
    /// <inheritdoc />
    /// <summary>
    /// Internal logic for binding settings to controls
    /// </summary>
    internal sealed class SettingsBinder : Binding
    {
        /// <inheritdoc />
        /// <summary>
        /// Initialize a new SettingsBinder
        /// </summary>
        /// <param name="path">The path that can be used to bind</param>
        public SettingsBinder(string path) : base(path)
        {
            Source = Properties.Settings.Default;
            Mode = BindingMode.TwoWay;
        }
    }
}
