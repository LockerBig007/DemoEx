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
using System.Windows.Shapes;
using DemoEx.AdminPanels;

namespace DemoEx
{
    /// <summary>
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void ButtonEmployees_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageEmployees());

        }

        private void ButtonOrders_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate (new PageOrders());
        }

        private void ButtonShifts_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageShifts());
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Перемещение окна при нажатии левой кнопкой мыши
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void ButtonExitClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
