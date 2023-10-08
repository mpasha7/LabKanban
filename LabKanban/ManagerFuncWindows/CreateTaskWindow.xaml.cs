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
    /// Логика взаимодействия для CreateTaskWindow.xaml
    /// </summary>
    public partial class CreateTaskWindow : Window
    {
        public CreateTaskWindow()
        {
            InitializeComponent();
        }

        private void Button_Create_Click(object sender, RoutedEventArgs e)
        {
            string theme = themeTextBox.Text.Trim();
            string desc = descTextBox.Text.Trim();
            string shortDesc = shortDescTextBox.Text.Trim();
            string intCust = intCustTextBox.Text.Trim();
            string extCust = extCustTextBox.Text.Trim();
            string just = justTextBox.Text.Trim();
            DateTime planEndDate = planEndDatePicker.DisplayDate;
            bool isPlan = isPlanedCheckBox.IsEnabled;
            int prior = 3;
            if (priorTextBox.Text != "")
                prior = Convert.ToInt32(priorTextBox.Text.Trim());
            DbController.CreateTask(theme, desc, shortDesc, intCust, extCust, just, planEndDate, isPlan, prior);
            this.Close();
        }
    }


}
