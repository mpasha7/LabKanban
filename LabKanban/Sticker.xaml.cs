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
    /// Логика взаимодействия для Sticker.xaml
    /// </summary>
    public partial class Sticker : UserControl
    {
        public Sticker()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler NextStatus;

        private void StickerButton_MouseMove(object sender, MouseEventArgs e)
        {
            StickerPopup.IsOpen = true;
        }

        private void StickerButton_Click(object sender, RoutedEventArgs e)
        {
            NextStatus?.Invoke(sender, e);
        }
    }
}
