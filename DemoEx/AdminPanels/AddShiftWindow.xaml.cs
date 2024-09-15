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
using AppContext = DemoEx.DB.AppContext;
using System.Data.Entity;

namespace DemoEx.AdminPanels
{
    /// <summary>
    /// Логика взаимодействия для AddShiftWindow.xaml
    /// </summary>
    public partial class AddShiftWindow : Window
    {
        public AddShiftWindow()
        {
            InitializeComponent();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            

            using (AppContext db = new AppContext())
            {

                if (string.IsNullOrEmpty(ShiftNameTextBox.Text))
                {
                    //MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    MessageBox.Show("Fill all fields", "Error", MessageBoxButton.OK, MessageBoxImage.None);
                    return;
                }


                db.shifts.Add(new Shift() { ShiftName = ShiftNameTextBox.Text });
                db.SaveChanges();

                db.shifts.Load();
                DialogResult = true;
                return;
            }
            
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }
    }
}
