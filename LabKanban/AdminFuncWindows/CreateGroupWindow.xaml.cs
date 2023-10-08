using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace LabKanban.AdminFuncWindows
{
    /// <summary>
    /// Логика взаимодействия для CreateGroupWindow.xaml
    /// </summary>
    public partial class CreateGroupWindow : Window
    {
        //public delegate void ContentUpdatHandler();
        //public event ContentUpdatHandler MethodEnd;

        public CreateGroupWindow()
        {
            InitializeComponent();
        }

        private void Button_Create_Click(object sender, RoutedEventArgs e)
        {
            if (groupnameTextBox.Text != "" && managerTextBox.Text != "")
            {
                string groupName = groupnameTextBox.Text.Trim();
                string managerName = managerTextBox.Text.Trim();
                DbController.CreateGroup(groupName, managerName);
                //MethodEnd?.Invoke();
                this.Close();
            }            
        }

    }


}
