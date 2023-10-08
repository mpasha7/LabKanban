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

namespace LabKanban.AdminFuncWindows
{
    /// <summary>
    /// Логика взаимодействия для RedactGroupWindow.xaml
    /// </summary>
    public partial class RedactGroupWindow : Window
    {
        public RedactGroupWindow()
        {
            InitializeComponent();
        }

        private void Button_Redact_Click(object sender, RoutedEventArgs e)
        {
            string oldGroupName = groupNameTextBox.Text.Trim();
            string newGroupName = newNameTextBox.Text.Trim();
            string managerName = managerTextBox.Text.Trim();
            DbController.RedactGroup(oldGroupName, newGroupName, managerName);
            this.Close();
        }
    }


}
