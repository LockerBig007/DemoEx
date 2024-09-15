using DemoEx.DB;
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

namespace DemoEx.Waiter
{
    /// <summary>
    /// Логика взаимодействия для AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        public AddOrderWindow()
        {
            InitializeComponent();
            LoadDishes();
            LoadStatuses();

        }

        private void ButtonCancelOrder_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoadDishes()
        {
            using (var db = new AppContext())
            {
                var dishes = db.dishes.ToList();
                ComboBoxDish.ItemsSource = dishes;
                ComboBoxDish.DisplayMemberPath = "DishName";
                ComboBoxDish.SelectedValuePath = "DishId";
            }
        }

        private void LoadStatuses()
        {
            using (var db = new AppContext())
            {
                var statuses = db.statuss.ToList();
                ComboBoxStatus.ItemsSource = statuses;
                ComboBoxStatus.DisplayMemberPath = "StatusName";
                ComboBoxStatus.SelectedValuePath = "StatusId";
            }
        }

        private void ButtonAddOrder_Click(object sender, RoutedEventArgs e)
        {
            using (AppContext db = new AppContext())
            {
                if (ComboBoxDish.SelectedItem == null || ComboBoxStatus.SelectedItem == null || string.IsNullOrWhiteSpace(TextBoxClientName.Text))
                {
                    MessageBox.Show("Please fill all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Добавление нового заказа
                var newOrder = new Order
                {
                    ClientName = TextBoxClientName.Text,
                    DishId = (int)ComboBoxDish.SelectedValue,
                    StatusId = (int)ComboBoxStatus.SelectedValue
                };

                db.orders.Add(newOrder);
                db.SaveChanges();
            }
            this.Close();
        }
    }
}
