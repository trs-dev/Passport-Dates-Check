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

namespace PassportDatesCheck
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();

            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth - this.Width+6;
            this.Top = System.Windows.SystemParameters.PrimaryScreenHeight - this.Height-32;
        }


        private void OnlyNumber_Input(object sender, TextCompositionEventArgs e)
        {
            TextBox ctrl = sender as TextBox;
            e.Handled = "0123456789".IndexOf(e.Text) < 0;//only digits
        }

        private void NoSpaces_Input(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true; // don`t allow to imput space
            }
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
