using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEx.DB
{
    public class Status
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        [NotMapped]
        public List<Status> Statuses
        {
            get
            {
                using (AppContext db = new AppContext())
                {
                    db.statuss.Load();
                    return db.statuss.ToList();
                }
            }
        }
    }
}
