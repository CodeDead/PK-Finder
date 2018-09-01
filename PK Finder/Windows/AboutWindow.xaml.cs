using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
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
            // Change the theme of the Window
            StyleManager.ChangeStyle(this);

            // Enable window dragging, if the setting is enabled
            try
            {
                if (Properties.Settings.Default.WindowDraggable)
                {
                    MouseDown += OnMouseDown;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Method that is called when the Window should be dragged
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The MouseButtonEventArgs</param>
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        /// <summary>
        /// Close this window
        /// </summary>
        /// <param name="sender">The object that has invoked this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Open the license file for PK Finder
        /// </summary>
        /// <param name="sender">The object that has invoked this method</param>
        /// <param name="e">The RoutedEventArgs</param>
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
        /// <param name="e">The RoutedEventArgs</param>
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
