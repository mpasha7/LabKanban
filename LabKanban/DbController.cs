using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabKanban
{
    public static class DbController
    {
        public static KanbanContext db = new KanbanContext();

        //USERS 
        public static void CreateUser(string fullname, string pass, string role, int row = 0)
        {

            switch (role)
            {
                case "Chemist":
                    Chemist chemist = new Chemist { Fullname = fullname, Password = pass, Role = role, GridRowNumber = row };
                    db.Chemists.Add(chemist);
                    break;
                case "Admin":
                    Admin admin = new Admin { Fullname = fullname, Password = pass, Role = role };
                    db.Admins.Add(admin);
                    break;
                case "Manager":
                    Manager manager = new Manager { Fullname = fullname, Password = pass, Role = role };
                    db.Managers.Add(manager);
                    break;
                default: break;
            }
            db.SaveChanges();
        }
        public static void RedactUser(string oldName, string userRole, string newName, string newPassword, int newRow = 0)
        {
            if (newRow > 0)
            {
                Chemist chemist = db.Chemists.FirstOrDefault(x => x.Fullname == oldName);
                if (chemist != null)
                {
                    chemist.GridRowNumber = newRow;
                    db.Entry(chemist).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            User user = db.Users.FirstOrDefault(x => x.Fullname == oldName && x.Role == userRole);
            if (user != null)
            {
                if (newName != "")
                    user.Fullname = newName;
                if (newPassword != "")
                    user.Password = newPassword;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }            
        }       
        public static void DeleteUser(string userName, string userRole)
        {
            User user = db.Users.FirstOrDefault(x => x.Fullname == userName && x.Role == userRole);
            if (user != null)
            {
                switch (userRole)
                {
                    case "Chemist":
                        foreach (var item in ((Chemist)user).CTasks.ToList())
                        {
                            ((Chemist)user).CTasks.Remove(item);
                            db.Entry((Chemist)user).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        db.Chemists.Remove((Chemist)user);
                        break;
                    case "Admin":
                        db.Admins.Remove((Admin)user);
                        break;
                    case "Manager":
                        foreach (var item in ((Chemist)user).CTasks.ToList())
                        {
                            ((Chemist)user).CTasks.Remove(item);
                            db.Entry((Chemist)user).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        foreach (var item in ((Manager)user).Groups.ToList())
                        {
                            ((Manager)user).Groups.Remove(item);
                            db.Entry((Manager)user).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        db.Managers.Remove((Manager)user);
                        break;
                    default: break;
                }
                db.SaveChanges();
            }
        }

        //GROUPS 
        public static void CreateGroup(string groupName, string mangerName)
        {
            Manager manager = db.Managers.FirstOrDefault(x => x.Fullname == mangerName);
            if (manager != null)
            {
                Group group = new Group { Name = groupName, Manager = manager };
                db.Groups.Add(group);
                db.SaveChanges();
            }
            Chemist chemist = db.Chemists.FirstOrDefault(x => x.Fullname == mangerName);
            if (chemist != null)
            {
                AddChemist(groupName, mangerName);
            }
        }
        public static void RedactGroup(string groupName, string newGroupName, string newManagerName)
        {
            Group group = db.Groups.FirstOrDefault(x => x.Name == groupName);
            if (group != null)
            {
                if (newGroupName != "")
                    group.Name = newGroupName;
                if (newManagerName != "")
                    ChangeManager(groupName, newManagerName);
            }
            db.Entry(group).State = EntityState.Modified;
            db.SaveChanges();
        }
        public static void ChangeManager(string groupName, string newManagerName)
        {
            Group group = db.Groups.FirstOrDefault(x => x.Name == groupName);
            if (group != null)
            {
                string oldManagerName = "";
                if (group.Manager != null)
                    oldManagerName = group.Manager.Fullname;
                Console.Write($"\nСМЕНА РУКОВОДИТЕЛЯ ГРУППЫ {group.Name}");
                Manager newManager = db.Managers.FirstOrDefault(x => x.Fullname == newManagerName);
                if (newManager != null)
                {
                    group.Manager = newManager;
                    db.Entry(group).State = EntityState.Modified;
                    db.SaveChanges();
                }
                RemoveChemist(groupName, oldManagerName);
                AddChemist(groupName, newManagerName);
            }
        }
        public static void RemoveManager(string groupName)
        {


            Group group = db.Groups.FirstOrDefault(x => x.Name == groupName);
            string managerName = group.Manager.Fullname;
            Manager manager = db.Managers.FirstOrDefault(x => x.Fullname == managerName);
            if (manager != null && group != null)
            {
                manager.ManagersGroups.Remove(group);
                db.Entry(manager).State = EntityState.Modified;
                db.SaveChanges();
            }
            RemoveChemist(groupName, managerName);
        }
        public static void AddChemist(string groupName, string chemistName)
        {
            Group group = db.Groups.FirstOrDefault(x => x.Name == groupName);
            Chemist chemist = db.Chemists.FirstOrDefault(x => x.Fullname == chemistName);
            if (group != null && chemist != null)
            {
                group.Chemists.Add(chemist);
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void RemoveChemist(string groupName, string chemistName)
        {
            Group group = db.Groups.FirstOrDefault(x => x.Name == groupName);
            Chemist chemist = group.Chemists.FirstOrDefault(x => x.Fullname == chemistName);  //?????
            if (group != null && chemist != null)
            {
                group.Chemists.Remove(chemist);
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DeleteGroup(string groupName)
        {
            Group group = db.Groups.FirstOrDefault(x => x.Name == groupName);
            if (group != null)
            {
                db.Groups.Remove(group);
                db.SaveChanges();
            }
        }

        //TASKS
        public static void CreateTask(string theme, string desc, string shortDesc, string intCust, string extCust, string just, DateTime plan, bool isPlan, int prior = 3)
        {            
            CTask task = new CTask { Theme = theme, Description = desc, ShortDescription = shortDesc, IntCustomer = intCust, ExtCustomer = extCust, Justification = just, PlanedEndDate = plan, IsPlanned = isPlan, Priority = prior };
            db.CTasks.Add(task);
            db.SaveChanges();
        }
        public static void TaskToChemist(int taskId, string chemistName)
        {
            CTask task = db.CTasks.FirstOrDefault(x => x.Id == taskId);
            Chemist chemist = db.Chemists.FirstOrDefault(x => x.Fullname == chemistName);

            if (task != null && chemist != null)
            {
                task.Chemist = chemist;
                task.Status = (CTask.TaskStatus)2;
                task.ChemistFullname = chemistName;
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void TaskToChemist(string shortDescription, string chemistName)
        {
            CTask task = db.CTasks.FirstOrDefault(x => x.ShortDescription == shortDescription);
            Chemist chemist = db.Chemists.FirstOrDefault(x => x.Fullname == chemistName);

            if (task != null && chemist != null)
            {
                task.Chemist = chemist;
                task.Status = (CTask.TaskStatus)2;
                task.ChemistFullname = chemistName;
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void ChangeStatus(int taskId)
        {
            CTask task = db.CTasks.FirstOrDefault(x => x.Id == taskId);
            if (task != null)
            {
                if (task.Status == (CTask.TaskStatus)2 && task.BeginDate == null)
                    task.BeginDate = DateTime.Now;
                else if (task.Status == (CTask.TaskStatus)3 && task.EndDate == null)
                    task.EndDate = DateTime.Now;
                else if (task.Status == (CTask.TaskStatus)4 && task.AgreeDate == null)
                    task.AgreeDate = DateTime.Now;

                task.ChangeStatus();
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void BackChangeStatus(int taskId)
        {
            CTask task = db.CTasks.FirstOrDefault(x => x.Id == taskId);
            if (task != null)
            {
                task.BackChangeStatus();
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void ArchiveTask(int taskId)
        {
            CTask task = db.CTasks.FirstOrDefault(x => x.Id == taskId);
            if (task != null && task.Status == (CTask.TaskStatus)5)
            {
                if (task.ArchiveDate == null)
                    task.ArchiveDate = DateTime.Now;

                task.Status = (CTask.TaskStatus)6;
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DeleteTask(int taskId)
        {
            CTask task = db.CTasks.FirstOrDefault(x => x.Id == taskId);
            if (task != null)
            {
                db.CTasks.Remove(task);
                db.SaveChanges();
            }
        }

        //TASCKSEQUENCES
        public static void CreateSequence(string name, params int[] tasks)
        {
            TaskSequence taskSequence = new TaskSequence();
            taskSequence.Name = name;
            foreach (int x in tasks)
            {
                CTask task = db.CTasks.FirstOrDefault(a => a.Id == x);
                if (task != null)
                    taskSequence.CTasks.Add(task);

            }
            db.TaskSequences.Add(taskSequence);
            db.SaveChanges();
        }
        public static void AddTaskToSequence(int sequenceId, int taskId)
        {
            TaskSequence sequence = db.TaskSequences.FirstOrDefault(x => x.Id == sequenceId);
            CTask task = db.CTasks.FirstOrDefault(x => x.Id == taskId);
            if (sequence != null && task != null)
            {
                sequence.CTasks.Add(task);
                db.Entry(sequence).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void RemoveTaskToSequence(int sequenceId, int taskId)
        {
            TaskSequence sequence = db.TaskSequences.FirstOrDefault(x => x.Id == sequenceId);
            CTask task = db.CTasks.FirstOrDefault(x => x.Id == taskId);
            if (sequence != null && task != null)
            {
                sequence.CTasks.Remove(task);
                db.Entry(sequence).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void ArchiveSequence(int sequenceId)
        {
            TaskSequence sequence = db.TaskSequences.FirstOrDefault(x => x.Id == sequenceId);
            if (sequence != null)
            {
                bool flag = true;
                foreach (var item in sequence.CTasks.ToList())
                    if (item.Status < (CTask.TaskStatus)5)
                        flag = false;
                if (flag)
                {
                    sequence.IsArchived = true;
                    foreach (var item in sequence.CTasks.ToList())
                        item.Status = (CTask.TaskStatus)6;
                }
                db.Entry(sequence).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DeleteSequence(int sequenceId)
        {
            TaskSequence sequence = db.TaskSequences.FirstOrDefault(x => x.Id == sequenceId);
            if (sequence != null)
            {
                foreach (var item in sequence.CTasks.ToList())
                {
                    RemoveTaskToSequence(sequenceId, item.Id);
                }
                db.TaskSequences.Remove(sequence);
                db.SaveChanges();
            }

        }

        //TASKLISTS
        //()
        public static void CreateList(string name, int[] taskSequences, params int[] tasks)
        {
            TaskList list = new TaskList();
            list.Name = name;
            foreach (int x in taskSequences)
            {
                TaskSequence sequence = db.TaskSequences.FirstOrDefault(a => a.Id == x);
                if (sequence != null)
                    list.TaskSequences.Add(sequence);
            }
            foreach (int x in tasks)
            {
                CTask task = db.CTasks.FirstOrDefault(a => a.Id == x);
                if (task != null)
                    list.CTasks.Add(task);
            }
            db.TaskLists.Add(list);
            db.SaveChanges();
        }
        public static void CreateList(string name, params int[] tasks)
        {
            TaskList list = new TaskList();
            list.Name = name;
            foreach (int x in tasks)
            {
                CTask task = db.CTasks.FirstOrDefault(a => a.Id == x);
                if (task != null)
                    list.CTasks.Add(task);
            }
            db.TaskLists.Add(list);
            db.SaveChanges();
        }
        public static void AddTaskToList(int listId, int taskId)
        {
            TaskList list = db.TaskLists.FirstOrDefault(x => x.Id == listId);
            CTask task = db.CTasks.FirstOrDefault(x => x.Id == taskId);
            if (list != null && task != null)
            {
                list.CTasks.Add(task);
                db.Entry(list).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void RemoveTaskToList(int listId, int taskId)
        {
            TaskList list = db.TaskLists.FirstOrDefault(x => x.Id == listId);
            CTask task = db.CTasks.FirstOrDefault(x => x.Id == taskId);
            if (list != null && task != null)
            {
                list.CTasks.Remove(task);
                db.Entry(list).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void AddSequenceToList(int listId, int sequenceId)
        {
            TaskList list = db.TaskLists.FirstOrDefault(x => x.Id == listId);
            TaskSequence sequence = db.TaskSequences.FirstOrDefault(x => x.Id == sequenceId);
            if (list != null && sequence != null)
            {
                list.TaskSequences.Add(sequence);
                db.Entry(list).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void RemoveSequenceToList(int listId, int sequenceId)
        {
            TaskList list = db.TaskLists.FirstOrDefault(x => x.Id == listId);
            TaskSequence sequence = db.TaskSequences.FirstOrDefault(x => x.Id == sequenceId);
            if (list != null && sequence != null)
            {
                list.TaskSequences.Remove(sequence);
                db.Entry(list).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void ArchiveList(int listId)
        {
            TaskList list = db.TaskLists.FirstOrDefault(x => x.Id == listId);
            if (list != null)
            {
                bool flag = true;
                foreach (var item in list.CTasks)
                    if (item.Status < (CTask.TaskStatus)5)
                        flag = false;
                foreach (var item in list.TaskSequences)
                    foreach (var x in item.CTasks)
                        if (x.Status < (CTask.TaskStatus)5)
                            flag = false;
                if (flag)
                {
                    list.IsArchived = true;
                    foreach (var item in list.CTasks)
                        item.Status = (CTask.TaskStatus)6;
                    foreach (var item in list.TaskSequences)
                        foreach (var x in item.CTasks)
                            x.Status = (CTask.TaskStatus)6;
                }
                db.Entry(list).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DeleteList(int listId)
        {
            TaskList list = db.TaskLists.FirstOrDefault(x => x.Id == listId);
            if (list != null)
            {
                foreach (var item in list.TaskSequences.ToList())
                {
                    RemoveSequenceToList(listId, item.Id);
                }
                foreach (var item in list.CTasks.ToList())
                {
                    RemoveTaskToList(listId, item.Id);
                }
                db.TaskLists.Remove(list);
                db.SaveChanges();
            }
        }


    }
}
