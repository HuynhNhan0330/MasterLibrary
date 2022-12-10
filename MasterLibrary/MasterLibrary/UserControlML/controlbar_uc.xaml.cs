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

namespace MasterLibrary.UserControlML
{
    /// <summary>
    /// Interaction logic for controlbar_uc.xaml
    /// </summary>
    public partial class controlbar_uc : UserControl
    {
        public controlbar_uc()
        {
            InitializeComponent();
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            Button btnClose = sender as Button;
            System.Windows.Window window = System.Windows.Window.GetWindow(btnClose);
            window.Close();
        }

        private void Button_Maximize(object sender, RoutedEventArgs e)
        {
            Button btn_Maximize = sender as Button;
            System.Windows.Window window = System.Windows.Window.GetWindow(btn_Maximize);
            if (window != null)
            {
                if (window.WindowState != WindowState.Maximized)
                {
                    window.WindowState = WindowState.Maximized;
                    btnMaximize.ToolTip = "Normal";
                    controlbarUC.MinHeight = '1';
                }
                else
                {
                    window.WindowState = WindowState.Normal;
                    btnMaximize.ToolTip = "Maximize";
                    controlbarUC.MinHeight = '0';
                }
            }
        }

        private void Button_Minimize(object sender, RoutedEventArgs e)
        {
            Button btnMinimize = sender as Button;
            System.Windows.Window window = System.Windows.Window.GetWindow(btnMinimize);
            window.WindowState = WindowState.Minimized;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Grid grid = sender as Grid;
            System.Windows.Window window = System.Windows.Window.GetWindow(grid);
            window.DragMove();
        }
    }
}
