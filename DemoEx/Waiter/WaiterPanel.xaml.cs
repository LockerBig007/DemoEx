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
using DemoEx.DB;
using DemoEx.Worker;
using DemoEx.Cooker;
using DemoEx.AdminPanels;
using AppContext = DemoEx.DB.AppContext;
using System.Data.Entity;
using DemoEx.Waiter;

namespace DemoEx
{
    /// <summary>
    /// Логика взаимодействия для WaiterPanel.xaml
    /// </summary>
    public partial class WaiterPanel : Window
    {
        public WaiterPanel()
        {
            InitializeComponent();
            LoadOrders();
        }

        private int GetActiveShiftId()
        {
            // Логика для получения активной смены (может быть, по времени)
            return 1; // Пример, ID смены
        }

        private void LoadOrders()
        {
            using (var db = new AppContext())
            {
                // Предположим, что есть таблица для смен
                var activeShiftId = GetActiveShiftId(); // Получение активной смены
                var orders = db.orders
                    .Include(o => o.Dish)
                    .Include(o => o.Status)
                    .ToList();

                DataGridOrders.ItemsSource = orders; // Привязка заказов к DataGrid
            }
        }

        private void ButtonOrders_Click(object sender, RoutedEventArgs e)
        {
            LoadOrders();
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadOrders();
        }

        private void ButtonAddOrder_Click(object sender, RoutedEventArgs e)
        {
            AddOrderWindow addOrderWindow = new AddOrderWindow(); // Создание окна для добавления заказа
            addOrderWindow.ShowDialog();
            LoadOrders();
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
            mainWindow.ShowDialog();
            this.Close();
        }
    }
}
