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

namespace LabKanban.ManagerFuncWindows
{
    /// <summary>
    /// Логика взаимодействия для TaskToChemistWindow.xaml
    /// </summary>
    public partial class TaskToChemistWindow : Window
    {
        public TaskToChemistWindow()
        {
            InitializeComponent();
        }

        private void Button_ToChemist_Click(object sender, RoutedEventArgs e)
        {
            int id;
            string shortDesc;
            string fullname = fullnameTextBox.Text.Trim();
            if (idTextBox.Text != "")
            {
                id = Convert.ToInt32(idTextBox.Text.Trim());
                DbController.TaskToChemist(id, fullname);
            }
            else
            {
                shortDesc = shortDescTextBox.Text.Trim();
                DbController.TaskToChemist(shortDesc, fullname);
            }
            this.Close();
        }
    }


}
