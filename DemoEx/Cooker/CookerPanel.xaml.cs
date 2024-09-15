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

namespace DemoEx.Cooker
{
    /// <summary>
    /// Логика взаимодействия для CookerPanel.xaml
    /// </summary>
    public partial class CookerPanel : Window
    {
        public List<Status> StatusList { get; set; }
        public CookerPanel()
        {
            InitializeComponent();
            LoadOrders();
            
        }

        private void LoadStatuses()
        {
            using (var db = new AppContext())
            {
                StatusList = db.statuss.ToList(); // Загрузка всех статусов

                // Проверяем, загрузились ли статусы
                foreach (var status in StatusList)
                {
                    Console.WriteLine("Loaded status: " + status.StatusName);
                }
            }
        }

        private void LoadOrders()
        {
            using (AppContext db = new AppContext())
            {
                var order = db.orders.Include(o => o.Dish).Include(o => o.Status).ToList();
                DataGridOrders.ItemsSource = order;
            }
        }
        private void ButtonSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            using (AppContext db = new AppContext())
            {

                var orders = DataGridOrders.ItemsSource as List<Order>;
                if (orders != null)
                {
                    foreach (var order in orders)
                    {
                        var orderToUpdate = db.orders.FirstOrDefault(o => o.OrderId == order.OrderId);
                        if (orderToUpdate != null)
                        {
                            orderToUpdate.StatusId = order.StatusId; // Сохранение нового статуса заказа
                        }
                    }
                    db.SaveChanges(); // Сохраняем все изменения в базе данных
                    MessageBox.Show("Изменения сохранены.", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            LoadOrders();
        }

        private void OnComboBoxLoad(object sender, RoutedEventArgs e)
        {
            var cb = sender as ComboBox;
            using (AppContext db = new AppContext())
            {
                cb.ItemsSource = db.statuss.ToList();
            }
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
