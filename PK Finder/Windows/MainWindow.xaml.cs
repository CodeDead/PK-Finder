using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using PK_Finder.Classes;

namespace PK_Finder.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        private KeyInfo _keyInfo;

        public MainWindow()
        {
            InitializeComponent();

            LoadTheme();
            RefreshProductKey();
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
    }
}
