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
using DemoEx.DB;
using DemoEx.Worker;
using AppContext = DemoEx.DB.AppContext;
using System.Data.Entity;

namespace DemoEx.AdminPanels
{
    /// <summary>
    /// Логика взаимодействия для PageShifts.xaml
    /// </summary>
    public partial class PageShifts : Page
    {
        public PageShifts()
        {
            InitializeComponent();
            LoadShifts();
        }

        private void LoadShifts()
        {
            using (AppContext db = new AppContext())
            {
                
                var shifts = db.shifts.ToList();
                DataGridShifts.ItemsSource = shifts; 
            }
        }

        private void ButtonDeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            using (AppContext db = new AppContext())
            {
                if (DataGridShifts.SelectedItem is Shift selectedShift)
                {
               
                    var shiftToDelete = db.shifts.FirstOrDefault(o => o.ShiftId == selectedShift.ShiftId);

                    if (shiftToDelete != null)
                    {
                        db.shifts.Remove(shiftToDelete);
                        db.SaveChanges();

                        // Обновляем DataGrid после удаления hhkn
                        ReloadData();
                    }
                }
            }
        }

        private void ReloadData()
        {
            LoadShifts();
        }

        private void ButtonAddShift_Click(object sender, RoutedEventArgs e)
        {
            AddShiftWindow addShiftWindow = new AddShiftWindow();
            addShiftWindow.ShowDialog();
        }
    }
}
