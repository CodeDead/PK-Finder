using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using PK_Finder.Classes;

namespace PK_Finder.Windows
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow
    {
        public AboutWindow()
        {
            InitializeComponent();
            LoadTheme();
        }

        private void LoadTheme()
        {
            StyleManager.ChangeStyle(this);
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

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

        private void BtnCodeDead_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("http://codedead.com/");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
