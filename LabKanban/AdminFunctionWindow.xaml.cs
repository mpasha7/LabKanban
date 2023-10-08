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
using LabKanban.AdminFuncWindows;

namespace LabKanban
{
    /// <summary>
    /// Логика взаимодействия для AdminFunctionWindow.xaml
    /// </summary>
    public partial class AdminFunctionWindow : Window
    {

        public Admin AuthAdmin { get; set; }
        public AdminFunctionWindow(User user)
        {
            InitializeComponent();
            AuthAdmin = DbController.db.Admins.FirstOrDefault(x => x.Fullname == user.Fullname);
            this.Title += " / " + AuthAdmin.Fullname;

            Loaded += AdminFunctionWindow_Loaded;

        }


        private void AdminFunctionWindow_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in DbController.db.Users.ToList())
                usersView.Items.Add(item);
            foreach (var item in DbController.db.Groups.ToList())
            {
                groupsView.Items.Add(item);

            }

            var tasks = from item in DbController.db.CTasks where item.Status < (CTask.TaskStatus)5 select item;
            foreach (var item in tasks)
                tasksView.Items.Add(item);

            var sequences = from item in DbController.db.TaskSequences.ToList() where item.IsArchived == false select item;
            foreach (var item in sequences)
                sequencesList.Items.Add(item.ToString());

            var lists = from item in DbController.db.TaskLists.ToList() where item.IsArchived == false select item;
            foreach (var item in lists)
                listsList.Items.Add(item.ToString());
        }




        
        //USERS
        private void Button_CreateUser_Click(object sender, RoutedEventArgs e)
        {
            CreateUserWindow createUserWindow = new CreateUserWindow();
            createUserWindow.Show();
        }

        private void Button_RedactUser_Click(object sender, RoutedEventArgs e)
        {
            RedactUserWindow redactUserWindow = new RedactUserWindow();
            redactUserWindow.Show();
        }

        private void Button_DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            DeleteUserWindow deleteUserWindow = new DeleteUserWindow();
            deleteUserWindow.Show();
        }

        //GROUPS
        
        private void Button_CreateGroup_Click(object sender, RoutedEventArgs e)
        {
            CreateGroupWindow createGroupWindow = new CreateGroupWindow();
            //createGroupWindow.MethodEnd += ContentUpdater;
            createGroupWindow.Show();
        }

        private void Button_RedactGroup_Click(object sender, RoutedEventArgs e)
        {
            RedactGroupWindow redactGroupWindow = new RedactGroupWindow();
            redactGroupWindow.Show();
        }

        

    }


}
