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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabKanban
{    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string pass = passBox.Password.Trim();

            if(login == "")
            {
                textBoxLogin.ToolTip = "Заполните поле";
                textBoxLogin.Background = Brushes.Red;
            }
            else
            {
                textBoxLogin.ToolTip = "";
                textBoxLogin.Background = Brushes.Transparent;
            }

            if (pass == "")
            {
                passBox.ToolTip = "Заполните это поле";
                passBox.Background = Brushes.Red;
            }
            else
            {
                passBox.ToolTip = "";
                passBox.Background = Brushes.Transparent;
            }

            User authUser = null;
            {
                authUser = DbController.db.Users.ToList().Where(x => x.Fullname == login && x.Password == pass).FirstOrDefault();
            }
            if (authUser != null)
            {
                KanbanWindow kanbanWindow = new KanbanWindow(authUser);
                kanbanWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Некорректные данные!");
            }

        }
    }
}
