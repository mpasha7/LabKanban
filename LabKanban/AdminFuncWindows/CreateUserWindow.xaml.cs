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
    /// Логика взаимодействия для CreateUserWindow.xaml
    /// </summary>
    public partial class CreateUserWindow : Window
    {
        public CreateUserWindow()
        {
            InitializeComponent();
        }

        private void Button_Create_Click(object sender, RoutedEventArgs e)
        {
            string fullname = fullnameTextBox.Text.Trim();
            string password = passBox.Password.Trim();
            string role = roleTextBox.Text.Trim();
            int row = Convert.ToInt32(rowTextBox.Text.Trim());
            DbController.CreateUser(fullname, password, role, row);
            this.Close();
        }
    }
}
