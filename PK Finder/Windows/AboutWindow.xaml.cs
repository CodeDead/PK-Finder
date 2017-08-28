using System;
using System.Diagnostics;
using System.Windows;
using PK_Finder.Classes;

namespace PK_Finder.Windows
{
    /// <inheritdoc cref="Syncfusion.Windows.Shared.ChromelessWindow" />
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow
    {
        /// <inheritdoc />
        /// <summary>
        /// Initialize a new AboutWindow object
        /// </summary>
        public AboutWindow()
        {
            InitializeComponent();
            LoadTheme();
        }

        /// <summary>
        /// Load the theme of this object
        /// </summary>
        private void LoadTheme()
        {
            StyleManager.ChangeStyle(this);
        }

        /// <summary>
        /// Close this window
        /// </summary>
        /// <param name="sender">The object that has invoked this method</param>
        /// <param name="e">The routed event arguments</param>
        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Open the license file for PK Finder
        /// </summary>
        /// <param name="sender">The object that has invoked this method</param>
        /// <param name="e">The routed event arguments</param>
        private void BtnLicense_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\gpl.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Open the CodeDead website
        /// </summary>
        /// <param name="sender">The object that has invoked this method</param>
        /// <param name="e">The routed event arguments</param>
        private void BtnCodeDead_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("https://codedead.com/");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
