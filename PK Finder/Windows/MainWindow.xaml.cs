using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using PK_Finder.Classes;

namespace PK_Finder.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        #region Variables
        private KeyInfo _keyInfo;
        private readonly UpdateManager.UpdateManager _updateManager;
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            _updateManager = new UpdateManager.UpdateManager(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version, "http://codedead.com/Software/PK%20Finder/update.xml", "PK Finder");

            LoadTheme();
            RefreshProductKey();

            try
            {
                if (Properties.Settings.Default.AutoUpdate)
                {
                    CheckForUpdate(false, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Check for updates
        /// </summary>
        private void CheckForUpdate(bool showErrors, bool showNoUpdates)
        {
            try
            {
                _updateManager.CheckForUpdate(showErrors, showNoUpdates);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
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
        /// Refresh the controls to display the current product key and corresponding information
        /// </summary>
        private void RefreshProductKey()
        {
            try
            {
                _keyInfo = KeyManager.GetWindowsProductInformation();

                LblInfo.Content = _keyInfo.GetProductName();
                TxtProductKey.Text = _keyInfo.GetProductKey();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshItem_OnClick(object sender, RoutedEventArgs e)
        {
            RefreshProductKey();
        }

        private void SaveItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (_keyInfo == null) return;

            SaveFileDialog sfd = new SaveFileDialog { Filter = "Text file (*.txt)|*.txt" };
            if (sfd.ShowDialog() != true) return;
            try
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName))
                {
                    sw.Write(_keyInfo.GetReadableString());
                }

                MessageBox.Show(this, "Information saved successfully!", "PK Finder", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExitItem_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SettingsItem_OnClick(object sender, RoutedEventArgs e)
        {
            new SettingsWindow(this).ShowDialog();
        }

        private void BtnCopy_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Clipboard.SetText(TxtProductKey.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void HelpItem_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\help.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateItem_OnClick(object sender, RoutedEventArgs e)
        {
            CheckForUpdate(true, true);
        }

        private void HomePageItem_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("http://codedead.com/");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LicenseItem_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\gpl.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AboutItem_OnClick(object sender, RoutedEventArgs e)
        {
            new AboutWindow().ShowDialog();
        }
    }
}
