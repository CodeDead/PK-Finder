﻿using System;
using System.Windows;
using System.Windows.Media;
using Syncfusion.Windows.Shared;

namespace PK_Finder.Classes
{
    /// <summary>
    /// Static class to change the style of an object
    /// </summary>
    internal static class StyleManager
    {
        /// <summary>
        /// Change the visual style of an object
        /// </summary>
        /// <param name="o">The object that needs to have a style overhaul</param>
        internal static void ChangeStyle(DependencyObject o)
        {
            try
            {
                SkinStorage.SetVisualStyle(o, Properties.Settings.Default.VisualStyle);
                SkinStorage.SetMetroBrush(o, new SolidColorBrush(Properties.Settings.Default.MetroColor));
                if (!(o is ChromelessWindow window)) return;
                window.BorderThickness = new Thickness(Properties.Settings.Default.BorderThickness);
                window.CornerRadius = new CornerRadius(0, 0, 0, 0);
                window.Opacity = Properties.Settings.Default.WindowOpacity / 100;
                window.ResizeBorderThickness = new Thickness(Properties.Settings.Default.WindowResizeBorder);
            }
            catch (Exception ex)
            {
                SkinStorage.SetVisualStyle(o, "Metro");
                MessageBox.Show(ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
