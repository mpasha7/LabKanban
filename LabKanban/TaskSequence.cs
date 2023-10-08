using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabKanban
{
    public class TaskSequence
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsArchived { get; set; }
        public virtual TaskList TaskList { get; set; }                       //Входит в список
        public virtual ICollection<CTask> CTasks { get; set; }               //Содержит несколько задач

        public TaskSequence()
        {
            IsArchived = false;
            CTasks = new List<CTask>();
        }


        public override string ToString()
        {
            string str = $"Последовательность № {Id}:\n{Name}\n";
            foreach (var item in CTasks.ToList())
                str += item.Id.ToString() + "->";
            return str;
        }

    }
}
