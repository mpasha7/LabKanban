using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabKanban
{
    public class TaskList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsArchived { get; set; }
        public virtual ICollection<CTask> CTasks { get; set; }                      //Содержит несколько задач
        public virtual ICollection<TaskSequence> TaskSequences { get; set; }        //Содержит несколько последовательностей

        public TaskList()
        {
            IsArchived = false;
            CTasks = new List<CTask>();
            TaskSequences = new List<TaskSequence>();
        }



        public override string ToString()
        {
            string str = $"Список № {Id}:\n{Name}\n";
            foreach (var item in TaskSequences.ToList())
            {
                foreach (var x in item.CTasks.ToList())
                    str += x.Id.ToString() + "->";
                str += "\n";
            }
            foreach (var item in CTasks.ToList())
                str += item.Id.ToString() + "->\n";
            return str;
        }


    }
}
