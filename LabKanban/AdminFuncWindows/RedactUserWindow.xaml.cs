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
    /// Логика взаимодействия для RedactUserWindow.xaml
    /// </summary>
    public partial class RedactUserWindow : Window
    {
        public RedactUserWindow()
        {
            InitializeComponent();
        }

        private void Button_Redact_Click(object sender, RoutedEventArgs e)
        {
            string oldFullname = oldNameTextBox.Text.Trim();
            string role = roleTextBox.Text.Trim();
            string newFullname = newNameTextBox.Text.Trim();
            string password = passBox.Password.Trim();
            string password2 = passBox2.Password.Trim();
            int row = Convert.ToInt32(rowTextBox.Text.Trim());
            if (password != password2)
                MessageBox.Show("Повторите ввод пароля");
            else
            {
                DbController.RedactUser(oldFullname, role, newFullname, password, row);
                this.Close();
            }
        }
    }


}
