using System;
using System.Windows;
using Syncfusion.SfSkinManager;
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
                SfSkinManager.ApplyStylesOnApplication = true;
                Enum.TryParse(Properties.Settings.Default.VisualStyle, out VisualStyles styles);

                SfSkinManager.SetVisualStyle(o, styles);
                SfSkinManager.SetTheme(o, new Theme(Properties.Settings.Default.VisualStyle));
            }
            catch (Exception ex)
            {
                SkinStorage.SetVisualStyle(o, "Metro");
                MessageBox.Show(ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
