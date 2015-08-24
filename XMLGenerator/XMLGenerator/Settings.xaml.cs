using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace XMLGenerator
{
    public partial class Settings : MetroWindow
    {
        public Settings()
        {
            InitializeComponent();

            maxCol.Value = MainWindow.MaxCol;
            maxRow.Value = MainWindow.MaxRow;
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {

            MainWindow.MaxCol = (uint)maxCol.Value;
            MainWindow.MaxRow = (uint)maxRow.Value;

            Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
