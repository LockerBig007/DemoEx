using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DemoEx.Worker;

namespace DemoEx.DB
{
    public class AppContext : DbContext
    {
        public AppContext() : base("Server = LAPTOP-RRTUKQLA\\SQLEXPRESS;Database=dbCafe;Trusted_Connection=True;")
        {
            if (Database.CreateIfNotExists())
            {

                shifts.AddRange(new Shift[] { new Shift() { ShiftName = "Утро" }, new Shift() { ShiftName = "День" }, new Shift() { ShiftName = "Вечер" } });
                SaveChanges();
                //db.load()
                roles.AddRange(new Role[] { new Role() { RoleName = "admin" }, new Role() { RoleName = "cooker" }, new Role() { RoleName = "waiter" } });
                SaveChanges();
                // (new Role() { RoleName = "Админ" });
                employees.Add(new Employee() { Name = "Nick", Login = "qwert", Password = "1234", Role = roles.ToList()[0], Shift = shifts.ToList()[0] });
                employees.Add(new Employee() { Name = "Robert", Login = "Timm", Password = "1234", Role = roles.ToList()[1], Shift = shifts.ToList()[1] });
                employees.Add(new Employee() { Name = "Vova", Login = "Logic", Password = "5678", Role = roles.ToList()[2], Shift = shifts.ToList()[2] });
                SaveChanges();

                statuss.AddRange(new Status[] { new Status() { StatusName = "Ожидает" }, new Status() { StatusName = "Принят" }, new Status() { StatusName = "Завершён" } });
                SaveChanges();

                dishes.Add(new Dish() { DishName = "Карбонара", Price = "400" });
                dishes.Add(new Dish() { DishName = "Пицца", Price = "1400" });
                dishes.Add(new Dish() { DishName = "Пельмени", Price = "100" });
                SaveChanges();

                orders.Add(new Order { ClientName = "Коля", Dish = dishes.ToList()[0], Status = statuss.ToList()[0]});
                orders.Add(new Order { ClientName = "Антон", Dish = dishes.ToList()[1], Status = statuss.ToList()[2] });
                orders.Add(new Order { ClientName = "Ящер", Dish = dishes.ToList()[2], Status = statuss.ToList()[1] });
                SaveChanges();

                


            }
        }

        public DbSet<Employee>employees {  get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Dish> dishes { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Shift> shifts { get; set; }
        public DbSet<Status> statuss { get; set; }

        

        

    }
}
