using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabKanban
{
    public class KanbanContext:DbContext
    {
        public KanbanContext() : base("KanbanConnectionString") { }


        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Chemist> Chemists { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<CTask> CTasks { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<TaskSequence> TaskSequences { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }
    }
}
