using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoEx.DB;

namespace DemoEx.Worker
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        // Связь с таблицей ролей
        [ForeignKey("RoleId")]
        public Role Role { get; set; } // навигационное свойство
        public int RoleId { get; set; }

        public bool IsFired { get; set; }

        [ForeignKey("ShiftId")]
        public Shift Shift { get; set; }
        public int ShiftId { get; set; }




    }
}
