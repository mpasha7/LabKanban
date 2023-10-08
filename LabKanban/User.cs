using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabKanban
{
    public class User
    {
        
        //public static KanbanContext db = new KanbanContext();
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }


        public override string ToString()
        {
            return this.Fullname;
        }
    }
}
