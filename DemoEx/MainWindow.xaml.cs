using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DemoEx.Cooker;
using DemoEx.DB;
using DemoEx.Worker;
using AppContext = DemoEx.DB.AppContext;


namespace DemoEx
{
    //hhhh
    //jj
    //ff
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using (AppContext db = new AppContext())
            {

                db.employees.Load();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Запуск анимации при загрузке окна storyboard
            Storyboard storyboard = (Storyboard)this.Resources["FadeInStoryboard"];
            storyboard.Begin();
        }

        private void CloseMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
        }

        private void OpenAdminPanel()
        {
            AdminPanel adminPanel = new AdminPanel();
            adminPanel.Show();
        }

        private void Button_Log_Click(object sender, RoutedEventArgs e) // 
        {
            string login = TextBox_Login.Text;
            string password = PasswordBox_Login.Password;
            using (AppContext db = new AppContext())
            {
                var user = db.employees.Include("Role").FirstOrDefault(x => x.Login == login && x.Password == password);

                if (user != null)
                {
                    switch (user.Role.RoleName)
                    {
                        case "admin":
                            OpenAdminPanel();
                            CloseMainWindow();
                            break;

                        case "waiter":
                            WaiterPanel waiterPanel = new WaiterPanel();
                            waiterPanel.Show();
                            CloseMainWindow();
                            break;

                        case "cooker":
                            CookerPanel cookerPanel = new CookerPanel();
                            cookerPanel.Show();
                            CloseMainWindow();
                            break;
                    }
                }
                else if (login == null && password == null)
                {
                    MessageBox.Show("The field is empty");
                }
            }
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            string query = "Как получить 5ку за демо экзамен?";
            string url = $"https://www.google.com/search?q={Uri.EscapeDataString(query)}";

            // браузер с поисковым запросом
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Перемещение окна при нажатии левой кнопкой мыши
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void ButtonDelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }
    }
}
