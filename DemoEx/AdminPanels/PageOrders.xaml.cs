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
using DemoEx.Worker;
using DemoEx.DB;
using AppContext = DemoEx.DB.AppContext;
using System.Data.Entity;

namespace DemoEx
{
    /// <summary>
    /// Логика взаимодействия для PageOrders.xaml
    /// </summary>
    public partial class PageOrders : Page
    {
        public PageOrders()
        {
            InitializeComponent();
            
            using (AppContext db = new AppContext())
            {
                //db.orders.Add(new Order { ClientName = "Коля", Dish = db.dishes.ToList()[0], Status = db.statuss.ToList()[0] });
                //db.orders.Add(new Order { ClientName = "Антон", Dish = db.dishes.ToList()[1], Status = db.statuss.ToList()[2] });
                //db.orders.Add(new Order { ClientName = "Ящер", Dish = db.dishes.ToList()[2], Status = db.statuss.ToList()[1] });
                //db.SaveChanges();

                var order = db.orders
                    .Include(o => o.Status)  // Загружаем статус заказа
                    .Include(o => o.Dish)    // Загружаем информацию о блюде
                    .ToList();


                DataGridOrders.ItemsSource = order;
            }
        }

        private void ReloadData()
        {
            using (AppContext db = new AppContext())
            {
                var order = db.orders
                    .Include(o => o.Status)  // Загружаем статус заказа
                    .Include(o => o.Dish)    // Загружаем информацию о блюде
                    .ToList();


                DataGridOrders.ItemsSource = order;
            }
        }


        private void ButtonDeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            using (AppContext db = new AppContext())
            {
                if (DataGridOrders.SelectedItem is Order selectedOrder)
                {
                    // Загружаем объект из контекста
                    var orderToDelete = db.orders.FirstOrDefault(o => o.OrderId == selectedOrder.OrderId);

                    if (orderToDelete != null)
                    {
                        db.orders.Remove(orderToDelete);
                        db.SaveChanges();

                        // Обновляем DataGrid после удаления
                        ReloadData();
                    }
                }
            }
        }
    }
}
