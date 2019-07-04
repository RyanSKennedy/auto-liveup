using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;
using Utilities;
using System.Windows.Forms;

namespace AutoLiveUp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variable declaration
        private System.Windows.Forms.ContextMenu TrayMenu;
        private System.Windows.Forms.MenuItem menuItemExit;
        private System.Windows.Forms.MenuItem menuItemStart;
        private System.Windows.Forms.MenuItem menuItemStop;
        private System.Windows.Forms.MenuItem menuItemRefreshTime;
        private System.Windows.Forms.MenuItem subMenuItemRefreshTime_30sec;
        private System.Windows.Forms.MenuItem subMenuItemRefreshTime_1min;
        private System.Windows.Forms.MenuItem subMenuItemRefreshTime_3min;
        private System.Windows.Forms.MenuItem subMenuItemRefreshTime_5min;
        private System.Windows.Forms.MenuItem subMenuItemRefreshTime_10min;
        private System.Windows.Forms.MenuItem subMenuItemRefreshTime_15min;
        private System.Windows.Forms.MenuItem subMenuItemRefreshTime_20min;
        private System.Windows.Forms.MenuItem subMenuItemRefreshTime_30min;
        private System.Windows.Forms.MenuItem subMenuItemRefreshTime_1hour;
        private System.Windows.Forms.MenuItem menuItemRefreshControl;
        private System.Windows.Forms.MenuItem subMenuItemRefreshControl_Ctrl;
        private System.Windows.Forms.MenuItem subMenuItemRefreshControl_Space;
        private System.Windows.Forms.MenuItem subMenuItemRefreshControl_Shift;
        private System.Windows.Forms.MenuItem subMenuItemRefreshControl_Numlock;
        private System.Windows.Forms.MenuItem subMenuItemRefreshControl_Capslock;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.ComponentModel.IContainer components;
        globalKeyboardHook gkh = new globalKeyboardHook();
        public DispatcherTimer dispatcherTimer;
        public Key refreshControl = Key.LeftCtrl;
        public int refreshTime = 180000;
        public static int i = 0;
        #endregion


        public MainWindow()
        {
            InitializeComponent();

            this.components = new System.ComponentModel.Container();
            this.TrayMenu = new System.Windows.Forms.ContextMenu();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.menuItemStart = new System.Windows.Forms.MenuItem();
            this.menuItemStop = new System.Windows.Forms.MenuItem();

            this.menuItemRefreshTime = new System.Windows.Forms.MenuItem();
            this.subMenuItemRefreshTime_30sec = new System.Windows.Forms.MenuItem();
            this.subMenuItemRefreshTime_1min = new System.Windows.Forms.MenuItem();
            this.subMenuItemRefreshTime_3min = new System.Windows.Forms.MenuItem();
            this.subMenuItemRefreshTime_5min = new System.Windows.Forms.MenuItem();
            this.subMenuItemRefreshTime_10min = new System.Windows.Forms.MenuItem();
            this.subMenuItemRefreshTime_15min = new System.Windows.Forms.MenuItem();
            this.subMenuItemRefreshTime_20min = new System.Windows.Forms.MenuItem();
            this.subMenuItemRefreshTime_30min = new System.Windows.Forms.MenuItem();
            this.subMenuItemRefreshTime_1hour = new System.Windows.Forms.MenuItem();

            this.menuItemRefreshControl = new System.Windows.Forms.MenuItem();
            this.subMenuItemRefreshControl_Ctrl = new System.Windows.Forms.MenuItem();
            this.subMenuItemRefreshControl_Space = new System.Windows.Forms.MenuItem();
            this.subMenuItemRefreshControl_Shift = new System.Windows.Forms.MenuItem();
            this.subMenuItemRefreshControl_Numlock = new System.Windows.Forms.MenuItem();
            this.subMenuItemRefreshControl_Capslock = new System.Windows.Forms.MenuItem();
            
            #region Initialize element of TrayMenu
            // Initialize TrayMenu
            this.TrayMenu.MenuItems.AddRange(
                        new System.Windows.Forms.MenuItem[] {
                            this.menuItemExit,
                            this.menuItemStart,
                            this.menuItemStop,
                            this.menuItemRefreshTime,
                            this.menuItemRefreshControl
                        });

            // Initialize menuItemExit
            this.menuItemExit.Index = 4;
            this.menuItemExit.Text = "E&xit";
            this.menuItemExit.Click += new System.EventHandler(this.MenuItemExit_Click);

            // Initialize menuItemStop
            this.menuItemStop.Index = 3;
            this.menuItemStop.Text = "Stop";
            this.menuItemStop.Enabled = false;
            this.menuItemStop.Click += new System.EventHandler(this.MenuItemStop_Click);

            // Initialize menuItemStart
            this.menuItemStart.Index = 2;
            this.menuItemStart.Text = "Start";
            this.menuItemStart.Enabled = true;
            this.menuItemStart.Click += new System.EventHandler(this.MenuItemStart_Click);

            // Initialize menuItemRefreshTime
            this.menuItemRefreshTime.Index = 1;
            this.menuItemRefreshTime.Text = "Refresh time";
            this.menuItemRefreshTime.MenuItems.AddRange(
                        new System.Windows.Forms.MenuItem[] {
                            this.subMenuItemRefreshTime_30sec,
                            this.subMenuItemRefreshTime_1min,
                            this.subMenuItemRefreshTime_3min,
                            this.subMenuItemRefreshTime_5min,
                            this.subMenuItemRefreshTime_10min,
                            this.subMenuItemRefreshTime_15min,
                            this.subMenuItemRefreshTime_20min,
                            this.subMenuItemRefreshTime_30min,
                            this.subMenuItemRefreshTime_1hour
                        });

            // Initialize subMenuItemRefreshTime_30sec
            this.subMenuItemRefreshTime_30sec.Index = 0;
            this.subMenuItemRefreshTime_30sec.Text = "30 sec";
            this.subMenuItemRefreshTime_30sec.Click += new System.EventHandler(this.SubMenuItemRefreshTime_Click);

            // Initialize subMenuItemRefreshTime_1min
            this.subMenuItemRefreshTime_1min.Index = 1;
            this.subMenuItemRefreshTime_1min.Text = "1 min";
            this.subMenuItemRefreshTime_1min.Click += new System.EventHandler(this.SubMenuItemRefreshTime_Click);

            // Initialize subMenuItemRefreshTime_3min
            this.subMenuItemRefreshTime_3min.Index = 2;
            this.subMenuItemRefreshTime_3min.Text = "3 min (Default)";
            this.subMenuItemRefreshTime_3min.Click += new System.EventHandler(this.SubMenuItemRefreshTime_Click);

            // Initialize subMenuItemRefreshTime_5min
            this.subMenuItemRefreshTime_5min.Index = 3;
            this.subMenuItemRefreshTime_5min.Text = "5 min";
            this.subMenuItemRefreshTime_5min.Click += new System.EventHandler(this.SubMenuItemRefreshTime_Click);

            // Initialize subMenuItemRefreshTime_10min
            this.subMenuItemRefreshTime_10min.Index = 4;
            this.subMenuItemRefreshTime_10min.Text = "10 min";
            this.subMenuItemRefreshTime_10min.Click += new System.EventHandler(this.SubMenuItemRefreshTime_Click);

            // Initialize subMenuItemRefreshTime_15min
            this.subMenuItemRefreshTime_15min.Index = 5;
            this.subMenuItemRefreshTime_15min.Text = "15 min";
            this.subMenuItemRefreshTime_15min.Click += new System.EventHandler(this.SubMenuItemRefreshTime_Click);

            // Initialize subMenuItemRefreshTime_20min
            this.subMenuItemRefreshTime_20min.Index = 6;
            this.subMenuItemRefreshTime_20min.Text = "20 min";
            this.subMenuItemRefreshTime_20min.Click += new System.EventHandler(this.SubMenuItemRefreshTime_Click);

            // Initialize subMenuItemRefreshTime_30min
            this.subMenuItemRefreshTime_30min.Index = 7;
            this.subMenuItemRefreshTime_30min.Text = "30 min";
            this.subMenuItemRefreshTime_30min.Click += new System.EventHandler(this.SubMenuItemRefreshTime_Click);

            // Initialize subMenuItemRefreshTime_1hour
            this.subMenuItemRefreshTime_1hour.Index = 8;
            this.subMenuItemRefreshTime_1hour.Text = "1 hour";
            this.subMenuItemRefreshTime_1hour.Click += new System.EventHandler(this.SubMenuItemRefreshTime_Click);

            // Initialize menuItemRefreshControl
            this.menuItemRefreshControl.Index = 0;
            this.menuItemRefreshControl.Text = "Refresh control";
            this.menuItemRefreshControl.MenuItems.AddRange(
                        new System.Windows.Forms.MenuItem[] {
                            this.subMenuItemRefreshControl_Ctrl,
                            this.subMenuItemRefreshControl_Space,
                            this.subMenuItemRefreshControl_Shift,
                            this.subMenuItemRefreshControl_Numlock,
                            this.subMenuItemRefreshControl_Capslock
                        });

            // Initialize subItemMenuRefreshControl_Ctrl
            this.subMenuItemRefreshControl_Ctrl.Index = 0;
            this.subMenuItemRefreshControl_Ctrl.Text = "Ctrl (Default)";
            this.subMenuItemRefreshControl_Ctrl.Click += new System.EventHandler(this.SubMenuItemRefreshControl_Click);

            // Initialize subMenuItemRefreshControl_Space
            this.subMenuItemRefreshControl_Space.Index = 1;
            this.subMenuItemRefreshControl_Space.Text = "Space";
            this.subMenuItemRefreshControl_Space.Click += new System.EventHandler(this.SubMenuItemRefreshControl_Click);

            // Initialize subMenuItemRefreshControl_Shift
            this.subMenuItemRefreshControl_Shift.Index = 2;
            this.subMenuItemRefreshControl_Shift.Text = "Shift";
            this.subMenuItemRefreshControl_Shift.Click += new System.EventHandler(this.SubMenuItemRefreshControl_Click);

            // Initialize subMenuItemRefreshControl_Numlock
            this.subMenuItemRefreshControl_Numlock.Index = 3;
            this.subMenuItemRefreshControl_Numlock.Text = "Numlock";
            this.subMenuItemRefreshControl_Numlock.Click += new System.EventHandler(this.SubMenuItemRefreshControl_Click);

            // Initialize subMenuItemRefreshControl_Capslock
            this.subMenuItemRefreshControl_Capslock.Index = 4;
            this.subMenuItemRefreshControl_Capslock.Text = "Capslock";
            this.subMenuItemRefreshControl_Capslock.Click += new System.EventHandler(this.SubMenuItemRefreshControl_Click);
            #endregion

            notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            notifyIcon.Icon = Properties.Resources.AutoLiveUp_logo;
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    if (WindowState == WindowState.Normal)
                    {
                        this.WindowState = WindowState.Minimized;
                    }
                    else
                    {
                        this.Show();
                        this.WindowState = WindowState.Normal;
                    }
                };

            // Handle the Click event to activate the form.
            notifyIcon.Click += new System.EventHandler(this.NotifyIcon_Click);

            // The ContextMenu property sets the menu that will
            // appear when the systray icon is right clicked.
            notifyIcon.ContextMenu = this.TrayMenu;

            //  DispatcherTimer setup
            // Timer using for background checking activity
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
        }

        private void NotifyIcon_Click(object Sender, EventArgs e)
        {
            // Show the form when the user double clicks on the notify icon.
            // Activate the form.
            this.Activate();
        }

        private void MenuItemExit_Click(object Sender, EventArgs e)
        {
            // Close the form, which closes the application.
            this.Close();
        }

        private void MenuItemStop_Click(object Sender, EventArgs e)
        {
            // Stop the hook and work.
            Stop_Work();
        }

        private void MenuItemStart_Click(object Sender, EventArgs e)
        {
            // Start the hook and work.
            Start_Work();
        }

        private void SubMenuItemRefreshTime_Click(object Sender, EventArgs e)
        {
            // Set Refresh time.
            var tmpIndex = Sender.ToString().IndexOf("Text: ") + "Text: ".Length;
            var tmpLenght = Sender.ToString().Length - tmpIndex;
            var tmpVal = Sender.ToString().Substring(tmpIndex, tmpLenght);
            ComboBox_RefreshTimeout.SelectedValue = tmpVal;
        }

        private void SubMenuItemRefreshControl_Click(object Sender, EventArgs e)
        {
            // Set Refresh control.
            var tmpIndex = Sender.ToString().IndexOf("Text: ") + "Text: ".Length;
            var tmpLenght = Sender.ToString().Length - tmpIndex;
            var tmpVal = Sender.ToString().Substring(tmpIndex, tmpLenght);
            ComboBox_RefreshControl.SelectedValue = tmpVal;
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                this.Hide();
                WindowState = WindowState.Minimized;
            }
            else
            {
                this.Show();
                this.WindowState = WindowState.Normal;
            }
                
            base.OnStateChanged(e);
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
            Stop_Work();    
        }

        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            Start_Work();
        }

        private void ComboBox_RefreshControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboBox_RefreshControl.SelectedValue.ToString())
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
            RefreshTime_Change(e.AddedItems[0].ToString());
        }

        /// <summary>
        ///   Sends the specified key.
        /// </summary>
        /// <param name="key">The key</param>
        public static void Send(Key key)
        {
            //KeyEventArgs kea = new KeyEventArgs(Keyboard.PrimaryDevice, new HwndSource(0, 0, 0, 0, 0, "", IntPtr.Zero), 0, key);

            if (Keyboard.PrimaryDevice != null)
            {
                //if (Keyboard.PrimaryDevice.ActiveSource != null)
                //{
                    if (key == Key.NumLock || key == Key.CapsLock)
                    {
                        //var e = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key)
                        var e = new System.Windows.Input.KeyEventArgs(Keyboard.PrimaryDevice, new HwndSource(0, 0, 0, 0, 0, "", IntPtr.Zero), 0, key)
                        {
                            RoutedEvent = Keyboard.KeyDownEvent
                        };

                        InputManager.Current.ProcessInput(e);

                        //e = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key)
                        e = new System.Windows.Input.KeyEventArgs(Keyboard.PrimaryDevice, new HwndSource(0, 0, 0, 0, 0, "", IntPtr.Zero), 0, key)
                        {
                            RoutedEvent = Keyboard.KeyDownEvent
                        };

                        InputManager.Current.ProcessInput(e);
                    }
                    else
                    {
                        //var e = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key)
                        var e = new System.Windows.Input.KeyEventArgs(Keyboard.PrimaryDevice, new HwndSource(0, 0, 0, 0, 0, "", IntPtr.Zero), 0, key)
                        {
                            RoutedEvent = Keyboard.KeyDownEvent
                        };

                        InputManager.Current.ProcessInput(e);
                    }
                    
                    // Note: Based on your requirements you may also need to fire events for:
                    // RoutedEvent = Keyboard.PreviewKeyDownEvent
                    // RoutedEvent = Keyboard.KeyUpEvent
                    // RoutedEvent = Keyboard.PreviewKeyUpEvent
                //}
            }
        }

        private void Start_Work()
        {
            //start the hook 
            gkh.hook();
            //refreshTime = 3000; // for test
            dispatcherTimer.Start();

            Button_Stop.IsEnabled = true;
            Button_Start.IsEnabled = false;

            menuItemStart.Enabled = false;
            menuItemStop.Enabled = true;
        }

        private void Stop_Work()
        {
            //unhook
            gkh.unhook();
            dispatcherTimer.Stop();

            Button_Stop.IsEnabled = false;
            Button_Start.IsEnabled = true;

            menuItemStart.Enabled = true;
            menuItemStop.Enabled = false;
        }

        private void RefreshTime_Change(string val)
        {
            switch (val)
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
    }
}
