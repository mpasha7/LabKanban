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

namespace LabKanban
{
    /// <summary>
    /// Логика взаимодействия для KanbanWindow.xaml
    /// </summary>
    public partial class KanbanWindow : Window
    {
        public User AuthUser { get; set; }
        
        public KanbanWindow(User user)
        {
            InitializeComponent();
            AuthUser = user;
            this.Title += " / " + AuthUser.Fullname;

            sticker.popupList.Items.Add("Hello, World!");

            //Loaded += KanbanWindow_Loaded;
        }

        //private void KanbanWindow_Loaded(object sender, RoutedEventArgs e)
        //{
        //    chemistColumn.
        //    foreach (var chemist in DbController.db.Chemists.ToList())
        //    {
        //        int row = chemist.GridRowNumber;

        //    }
        //}

        private void Button_Functions_Click(object sender, RoutedEventArgs e)
        {
            if (this.AuthUser.Role == "Manager")
            {
                ManagerFunctionWindow managerWindow = new ManagerFunctionWindow(AuthUser);
                managerWindow.Show();
            }
            if (this.AuthUser.Role == "Admin")
            {
                AdminFunctionWindow adminWindow = new AdminFunctionWindow(AuthUser);
                adminWindow.Show();
            }
        }

        
    }


}
