using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabKanban
{
    public class Chemist :User
    {
        public int GridRowNumber { get; set; }
        public virtual ICollection<CTask> CTasks { get; set; }              //Выполняет несколько задач
        public virtual ICollection<Group> Groups { get; set; }              //Входит в несколько групп

        public Chemist()
        {
            CTasks = new List<CTask>();
            Groups = new List<Group>();
        }


        public override string ToString()
        {
            return "Химик " + base.ToString();
        }


    }
}
