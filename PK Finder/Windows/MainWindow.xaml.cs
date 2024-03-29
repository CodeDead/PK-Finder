﻿using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using PK_Finder.Classes;

namespace PK_Finder.Windows
{
    /// <inheritdoc cref="Syncfusion.Windows.Shared.ChromelessWindow" />
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Variables
        /// <summary>
        /// The KeyInfo object containing the relevant product information
        /// </summary>
        private KeyInfo _keyInfo;

        /// <summary>
        /// The UpdateManager that can be used to check for updates
        /// </summary>
        private readonly UpdateManager _updateManager;
        #endregion

        /// <inheritdoc />
        /// <summary>
        /// Initialize a new MainWindow object
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            _updateManager = new UpdateManager("https://codedead.com/Software/PK%20Finder/update.json");

            LoadTheme();
            WindowDraggable();

            RefreshProductKey();

            try
            {
                if (!Properties.Settings.Default.AutoUpdate) return;
                Update update = _updateManager.GetUpdate();
                CheckUpdate(update, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Check for application updates
        /// </summary>
        /// <param name="update">The Update object</param>
        /// <param name="showNoUpdates">Display a message if no update is available</param>
        private void CheckUpdate(Update update, bool showNoUpdates)
        {
            if (_updateManager.CheckForUpdate(Assembly.GetExecutingAssembly().GetName().Version, update))
            {
                if (MessageBox.Show(this, update.UpdateInfo, "PK Finder", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(update.UpdateUrl) { UseShellExecute = true });
                }
            }
            else
            {
                if (showNoUpdates)
                {
                    MessageBox.Show(this, "You are using the latest version!", "PK Finder", MessageBoxButton.OK);
                }
            }
        }

        /// <summary>
        /// Change the visual style of the controls, depending on the settings.
        /// </summary>
        internal void LoadTheme()
        {
            StyleManager.ChangeStyle(this);
        }

        /// <summary>
        /// Check whether the Window should be draggable or not
        /// </summary>
        internal void WindowDraggable()
        {
            try
            {
                if (Properties.Settings.Default.WindowDraggable)
                {
                    // Delete event handler first to prevent duplicate handlers
                    MouseDown -= OnMouseDown;
                    MouseDown += OnMouseDown;
                }
                else
                {
                    MouseDown -= OnMouseDown;
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
        /// Refresh the controls to display the current product key and corresponding information
        /// </summary>
        private void RefreshProductKey()
        {
            try
            {
                _keyInfo = KeyManager.GetWindowsProductInformation();

                if (_keyInfo == null)
                {
                    MessageBox.Show(
                        "Unable to retrieve product key! Your registry might be corrupt or your version of Windows is not activated.",
                        "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                LblInfo.Content = _keyInfo.ProductName;
                TxtProductKey.Text = _keyInfo.ProductKey;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Refresh the product information
        /// </summary>
        /// <param name="sender">The object that has invoked this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void RefreshItem_OnClick(object sender, RoutedEventArgs e)
        {
            RefreshProductKey();
        }

        /// <summary>
        /// Export the product information
        /// </summary>
        /// <param name="sender">The object that has invoked this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void ExportItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (_keyInfo == null) return;

            SaveFileDialog sfd = new SaveFileDialog
            { Filter = "Text file (*.txt)|*.txt|HTML (*.html)|*.html|CSV (*.csv)|*.csv|Excel (*.csv)|*.csv|JSON (*.json)|*.json" };
            ExportManager exportManager = new ExportManager(_keyInfo);

            if (sfd.ShowDialog() != true) return;
            try
            {
                switch (sfd.FilterIndex)
                {
                    default:
                        exportManager.ExportToTxt(sfd.FileName);
                        break;
                    case 2:
                        exportManager.ExportToHtml(sfd.FileName);
                        break;
                    case 3:
                        exportManager.ExportToCsv(sfd.FileName);
                        break;
                    case 4:
                        exportManager.ExportToExcel(sfd.FileName);
                        break;
                    case 5:
                        exportManager.ExportToJson(sfd.FileName);
                        break;
                }

                MessageBox.Show("Key exported!", "PK Finder", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Exit the application
        /// </summary>
        /// <param name="sender">The object that has invoked this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void ExitItem_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Open a new SettingsWindow
        /// </summary>
        /// <param name="sender">The object that has invoked this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void SettingsItem_OnClick(object sender, RoutedEventArgs e)
        {
            new SettingsWindow(this).ShowDialog();
        }

        /// <summary>
        /// Copy the product key information to the clipboard
        /// </summary>
        /// <param name="sender">The object that has invoked this method</param>
        /// <param name="e">The routed event arguments</param>
        private void BtnCopy_OnClick(object sender, RoutedEventArgs e)
        {
            CopyData();
        }

        /// <summary>
        /// Open the help documentation for PK Finder
        /// </summary>
        /// <param name="sender">The object that has invoked this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void HelpItem_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + "help.pdf") { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Check for application updates
        /// </summary>
        /// <param name="sender">The object that has invoked this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private async void UpdateItem_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Update update = await _updateManager.GetUpdateAsync();
                CheckUpdate(update, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Open the CodeDead website
        /// </summary>
        /// <param name="sender">The object that has invoked this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void HomePageItem_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo("https://codedead.com/") { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Open the license file for PK Finder
        /// </summary>
        /// <param name="sender">The object that has invoked this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void LicenseItem_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + "license.pdf") { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Open a new AboutWindow
        /// </summary>
        /// <param name="sender">The object that has invoked this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void AboutItem_OnClick(object sender, RoutedEventArgs e)
        {
            new AboutWindow().ShowDialog();
        }

        /// <summary>
        /// Open the donation page
        /// </summary>
        /// <param name="sender">The object that has invoked this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void DonateItem_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo("https://codedead.com/donate/") { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// If applicable, copy the product key to the clipboard
        /// </summary>
        /// <param name="sender">The object that has invoked this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void TxtProductKey_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (Properties.Settings.Default.DoubleClickCopy)
                {
                    CopyData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Copy the product key to the clipboard and display a message if applicable.
        /// </summary>
        private void CopyData()
        {
            try
            {
                Clipboard.SetText(TxtProductKey.Text);

                if (Properties.Settings.Default.CopyMessage)
                {
                    MessageBox.Show("Product key has been copied to the clipboard!", "PK Finder", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
