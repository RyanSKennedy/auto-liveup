using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Utilities;

namespace AutoLiveUp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variable declaration
        globalKeyboardHook gkh = new globalKeyboardHook();
        public DispatcherTimer dispatcherTimer;
        public Key refreshControl = Key.LeftCtrl;
        public int refreshTime = 180000;
        public static int i = 0;
        #endregion


        public MainWindow()
        {
            InitializeComponent();
            
            //  DispatcherTimer setup
            // Timer using for background checking activity
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            i++;

            if (i*1000 >= refreshTime)
            {
                Send(refreshControl);
                i = 0;
            }

            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }

        private void Button_Stop_Click(object sender, RoutedEventArgs e)
        {
            //unhook
            gkh.unhook();
            dispatcherTimer.Stop();
            Button_Stop.IsEnabled = false;
            Button_Start.IsEnabled = true;
        }

        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            //start the hook 
            gkh.hook();
            dispatcherTimer.Start();
            Button_Stop.IsEnabled = true;
            Button_Start.IsEnabled = false;
        }

        private void ComboBox_CombinationKey_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboBox_CombinationKey.SelectedValue.ToString())
            {
                case "Ctrl (Default)":
                    refreshControl = Key.LeftCtrl;
                    break;

                case "Space":
                    refreshControl = Key.Space;
                    break;

                case "Shift":
                    refreshControl = Key.LeftShift;
                    break;

                case "Numlock":
                    refreshControl = Key.NumLock;
                    break;
                     
                case "Capslock":
                    refreshControl = Key.CapsLock;
                    break;

                default:
                    refreshControl = Key.LeftCtrl;
                    break;
            }
        }

        private void ComboBox_RefreshTimeout_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboBox_RefreshTimeout.SelectedValue.ToString())
            {
                case "30 sec":
                    refreshTime = 30000;
                    break;

                case "1 min":
                    refreshTime = 60000;
                    break;

                case "3 min (Default)":
                    refreshTime = 180000;
                    break;

                case "5 min":
                    refreshTime = 300000;
                    break;

                case "10 min":
                    refreshTime = 600000;
                    break;

                case "15 min":
                    refreshTime = 900000;
                    break;

                case "20 min":
                    refreshTime = 1200000;
                    break;

                case "30 min":
                    refreshTime = 1800000;
                    break;

                case "1 hour":
                    refreshTime = 3600000;
                    break;

                default:
                    refreshTime = 180000;
                    break;
            }
        }

        /// <summary>
        ///   Sends the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public static void Send(Key key)
        {
            if (Keyboard.PrimaryDevice != null)
            {
                if (Keyboard.PrimaryDevice.ActiveSource != null)
                {
                    if (key == Key.NumLock || key == Key.CapsLock)
                    {
                        var e = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key)
                        {
                            RoutedEvent = Keyboard.KeyDownEvent
                        };

                        InputManager.Current.ProcessInput(e);

                        e = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key)
                        {
                            RoutedEvent = Keyboard.KeyDownEvent
                        };

                        InputManager.Current.ProcessInput(e);
                    }
                    else
                    {
                        var e = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key)
                        {
                            RoutedEvent = Keyboard.KeyDownEvent
                        };

                        InputManager.Current.ProcessInput(e);
                    }
                    
                    // Note: Based on your requirements you may also need to fire events for:
                    // RoutedEvent = Keyboard.PreviewKeyDownEvent
                    // RoutedEvent = Keyboard.KeyUpEvent
                    // RoutedEvent = Keyboard.PreviewKeyUpEvent
                }
            }
        }
    }
}
