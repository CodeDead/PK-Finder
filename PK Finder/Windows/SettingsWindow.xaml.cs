using System;
using System.Windows;
using PK_Finder.Classes;

namespace PK_Finder.Windows
{
    /// <inheritdoc cref="Syncfusion.Windows.Shared.ChromelessWindow" />
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow
    {
        #region Variables
        /// <summary>
        /// The MainWindow object
        /// </summary>
        private readonly MainWindow _mw;
        #endregion

        /// <inheritdoc />
        /// <summary>
        /// Initialize a new SettingsWindow object
        /// </summary>
        /// <param name="mw"></param>
        public SettingsWindow(MainWindow mw)
        {
            _mw = mw;

            InitializeComponent();
            LoadTheme();

            LoadSettings();
        }

        /// <summary>
        /// Change the visual style of the controls, depending on the settings.
        /// </summary>
        private void LoadTheme()
        {
            StyleManager.ChangeStyle(this);
        }

        /// <summary>
        /// Change the state of certain controls to represent the current settings
        /// </summary>
        private void LoadSettings()
        {
            try
            {
                ChbAutoUpdate.IsChecked = Properties.Settings.Default.AutoUpdate;
                CboStyle.SelectedValue = Properties.Settings.Default.VisualStyle;
                CpMetroBrush.Color = Properties.Settings.Default.MetroColor;
                IntBorderThickness.Value = Properties.Settings.Default.BorderThickness;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Reset all settings to their default values
        /// </summary>
        /// <param name="sender">The object that has invoked this method</param>
        /// <param name="e">The routed event arguments</param>
        private void BtnReset_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show(this, "Are you sure that you want to reset all settings?", "PK Finder", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
                Properties.Settings.Default.Reset();
                Properties.Settings.Default.Save();

                LoadSettings();

                _mw.LoadTheme();
                LoadTheme();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Save all settings
        /// </summary>
        /// <param name="sender">The object that has invoked this method</param>
        /// <param name="e">The routed event arguments</param>
        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ChbAutoUpdate.IsChecked != null) Properties.Settings.Default.AutoUpdate = ChbAutoUpdate.IsChecked.Value;
                Properties.Settings.Default.VisualStyle = CboStyle.Text;

                Properties.Settings.Default.MetroColor = CpMetroBrush.Color;
                if (IntBorderThickness.Value != null) Properties.Settings.Default.BorderThickness = (int)IntBorderThickness.Value;

                Properties.Settings.Default.Save();

                _mw.LoadTheme();
                LoadTheme();

                MessageBox.Show(this, "All settings have been saved!", "PK Finder", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
