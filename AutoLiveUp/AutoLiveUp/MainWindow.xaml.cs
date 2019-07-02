using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoLiveUp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region
        public string refreshControl;
        public int refreshTime = 0;
        #endregion


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Stop_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_CombinationKey_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboBox_CombinationKey.SelectedValue.ToString())
            {
                case "Ctrl (Default)":
                    refreshControl = "{^}";
                    break;

                case "Backspace":
                    refreshControl = "{BACKSPACE}";
                    break;

                case "Shift":
                    refreshControl = "{+}";
                    break;

                case "Numlock":
                    refreshControl = "{NUMLOCK}";
                    break;
                     
                case "Capslock":
                    refreshControl = "{CAPSLOCK}";
                    break;

                case "Alt+Tab":
                    refreshControl = "{% TAB}";
                    break;

                default:
                    refreshControl = "{^}";
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
    }
}
