using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
                if (Properties.Settings.Default.WindowDraggable)
                {
                    // Prevent duplicate handlers
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
                MessageBox.Show(this, ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
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
        /// Reset all settings to their default values
        /// </summary>
        /// <param name="sender">The object that has invoked this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void BtnReset_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show(this, "Are you sure that you want to reset all settings?", "PK Finder",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;

                Properties.Settings.Default.Reset();
                Properties.Settings.Default.Save();

                LoadSettings();

                _mw.LoadTheme();
                _mw.WindowDraggable();

                LoadTheme();
                LoadSettings();
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
        /// <param name="e">The RoutedEventArgs</param>
        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.Save();

                _mw.LoadTheme();
                _mw.WindowDraggable();

                LoadTheme();
                LoadSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Method that is called when the theme is changed
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The SelectionChangedEventArgs</param>
        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadTheme();
        }

        /// <summary>
        /// Method that is called when the Window is closing
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The CancelEventArgs</param>
        private void SettingsWindow_OnClosing(object sender, CancelEventArgs e)
        {
            try
            {
                Properties.Settings.Default.Reload();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PK Finder", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
