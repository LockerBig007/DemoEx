using DemoEx.DB;
using DemoEx.Worker;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Data.Entity;
using AppContext = DemoEx.DB.AppContext;

namespace DemoEx
{
    /// <summary>
    /// Логика взаимодействия для PageEmployees.xaml
    /// </summary>
    public partial class PageEmployees : Page
    {
        
        public PageEmployees()
        {
            InitializeComponent();
            using (AppContext db = new AppContext())
            {
               
                var employeess = db.employees.Include(x => x.Role).Include(x => x.Shift).ToList(); //чтоб роль подгружалась
                DataGridEmployees.ItemsSource = employeess;
            }
        }

        private void ReloadData()
        {
            using (AppContext db = new AppContext())
            {
                // Загрузка обновленных данных из базы данных
                var employeess = db.employees.Include(x => x.Role).Include(x => x.Shift).ToList();
                // Обновляем источник данных для DataGrid
                DataGridEmployees.ItemsSource = employeess;
            }
        }

        private void ButtonAddEmployee_Click(object sender, RoutedEventArgs e)
        {




            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();

            // Если пользователь успешно добавлен (DialogResult == true)
            if (addEmployeeWindow.ShowDialog() == true)
            {
                // Перезагружаем данные в DataGrid
                ReloadData();
            }

            
        }

        private void ButtonDismissEmployee_Click(object sender, RoutedEventArgs e)
        {
            using (AppContext db = new AppContext())
            {

                
                if (DataGridEmployees.SelectedItem is Employee selectedEmployee)
                {
                    // Загружаем объект из контекста
                    var EmployeeToDelete = db.employees.FirstOrDefault(o => o.EmployeeId == selectedEmployee.EmployeeId);

                    if (EmployeeToDelete != null)
                    {
                        db.employees.Remove(EmployeeToDelete);
                        db.SaveChanges();

                        // Обновляем DataGrid после удаления
                        ReloadData();
                    }
                }
            }
        }
    }
}
