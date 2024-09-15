using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DemoEx.DB
{
    public class Order
    {
        public int OrderId { get; set; }
        public string ClientName { get; set; }

        [ForeignKey("StatusId")]
        public Status Status { get; set; }
        public int StatusId { get; set; }


        [ForeignKey("DishId")]
        public Dish Dish { get; set; }
        public int DishId { get; set; }

     
    }
}
