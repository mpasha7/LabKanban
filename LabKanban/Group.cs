using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabKanban
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Manager Manager { get; set; }                              //Управляется руководителем
        public virtual ICollection<Chemist> Chemists { get; set; }                //Включает в себя несколько химиков
        

        public Group()
        {
            Chemists = new List<Chemist>();
        }

        public override string ToString()
        {
            return "Группа " + Name /*+ ", руководитель - " + this.Manager.ToString()*/;
        }
    }
}
