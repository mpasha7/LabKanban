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
    /// Логика взаимодействия для DeleteUserWindow.xaml
    /// </summary>
    public partial class DeleteUserWindow : Window
    {
        public DeleteUserWindow()
        {
            InitializeComponent();
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            string fullname = fullnameTextBox.Text.Trim();
            string role = roleTextBox.Text.Trim();
            DbController.DeleteUser(fullname, role);
            this.Close();
        }
    }


}
