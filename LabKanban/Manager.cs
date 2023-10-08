using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabKanban
{
    public class Manager :Chemist
    {

        public virtual ICollection<Group> ManagersGroups { get; set; }

        public Manager()
        {
            ManagersGroups = new List<Group>();
        }



        public override string ToString()
        {
            return "Руководитель " + base.ToString();
        }


        
        


    }
}
