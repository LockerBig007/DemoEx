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
using System.Data.Entity;
using DemoEx.Worker;
using DemoEx.DB;
using AppContext = DemoEx.DB.AppContext;

namespace DemoEx
{
    /// <summary>
    /// Логика взаимодействия для AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        

        public AddEmployeeWindow()
        {
            InitializeComponent();
            LoadRoles();
            LoadShifts();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();
            this.Close();
        }

        private void LoadShifts()
        {
            using (AppContext db = new AppContext())
            {
                var shifts = db.shifts.ToList();
                ComboBoxShift.ItemsSource = shifts;
                ComboBoxShift.DisplayMemberPath = "ShiftName";
                ComboBoxShift.SelectedValuePath = "ShiftId";
            }
        }

        private void LoadRoles()
        {
            using (AppContext db = new AppContext())
            {
                var roless = db.roles.ToList();
                ComboBoxRole.ItemsSource = roless;
                ComboBoxRole.DisplayMemberPath = "RoleName";
                ComboBoxRole.SelectedValuePath = "RoleId";
            }
        }
        private void ButtonAdd_Click_1(object sender, RoutedEventArgs e)
        {

            

            using (AppContext db = new AppContext())
            {
                if (string.IsNullOrEmpty(TextBoxName.Text) && string.IsNullOrEmpty(TextBoxLogin.Text) && string.IsNullOrEmpty(TextBoxPassword.Text) && ComboBoxRole.SelectedItem == null)
                {
                    MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                    //employees.Add(new Employee() { Name = "Nick", Login = "qwert", Password = "1234", Role = roles.ToList()[0] });
                }


                db.employees.Add(new Employee() { Name = TextBoxName.Text, Login = TextBoxLogin.Text, Password = TextBoxPassword.Text, RoleId = (int)ComboBoxRole.SelectedValue, IsFired = false, ShiftId = (int)ComboBoxShift.SelectedValue});
                //NewEmployee = new Employee
                //{
                //    Name = TextBoxName.Text,
                //    Login = TextBoxLogin.Text,
                //    Password = TextBoxPassword.Text,
                //    RoleId = (int)ComboBoxRole.SelectedValue,
                //    IsFired = false
                //};

                db.SaveChanges();

                db.employees.Load();
                DialogResult = true;
                Close();
            }
            
            //var employeess = db.employees.Include(x => x.Role).ToList(); //чтоб роль подгружалась
           // DataGridEmployees.ItemsSource = employeess;
            //
        }
    }
}
