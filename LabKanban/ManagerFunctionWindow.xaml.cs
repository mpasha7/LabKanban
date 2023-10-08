using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LabKanban.ManagerFuncWindows;

namespace LabKanban
{
    /// <summary>
    /// Логика взаимодействия для ManagerFunctionWindow.xaml
    /// </summary>
    public partial class ManagerFunctionWindow : Window
    {
        
        public Manager AuthManager { get; set; }

        public ManagerFunctionWindow(User user)
        {
            InitializeComponent();

            AuthManager = DbController.db.Managers.FirstOrDefault(x => x.Fullname == user.Fullname);
            this.Title += " / " + AuthManager.Fullname;

            Loaded += ManagerFuncWindow_Loaded;            

        }

        private void ManagerFuncWindow_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var groupe in AuthManager.ManagersGroups)
            {
                var chemists = from chemist in DbController.db.Chemists.ToList() where chemist.Groups.Contains(groupe) select chemist;
                foreach (var chemist in chemists)
                {
                    if (!chemistsView.Items.Contains(chemist))
                        chemistsView.Items.Add(chemist);
                    foreach (var task in chemist.CTasks)
                        if (!tasksView.Items.Contains(task))
                        {
                            if (task.Status == (CTask.TaskStatus)5)
                                agreedView.Items.Add(task);
                            else if (task.Status > (CTask.TaskStatus)1 && task.Status < (CTask.TaskStatus)5)
                                tasksView.Items.Add(task);
                        }
                }
            }

            var noCharedTasks = from item in DbController.db.CTasks where item.Status == (CTask.TaskStatus)1 select item;
            foreach (var item in noCharedTasks)
                noSharedView.Items.Add(item);

            var sequences = from item in DbController.db.TaskSequences.ToList() where item.IsArchived == false select item;
            foreach (var item in sequences)
                sequencesList.Items.Add(item.ToString());

            var lists = from item in DbController.db.TaskLists.ToList() where item.IsArchived == false select item;
            foreach (var item in lists)
                listsList.Items.Add(item.ToString());
        }

        private void Button_CreateTask_Click(object sender, RoutedEventArgs e)
        {
            CreateTaskWindow createTaskWindow = new CreateTaskWindow();
            createTaskWindow.Show();
        }

        private void Button_TaskToChemist_Click(object sender, RoutedEventArgs e)
        {
            TaskToChemistWindow taskToChemistWindow = new TaskToChemistWindow();
            taskToChemistWindow.Show();
        }

        private void Button_ArchiveTask_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
