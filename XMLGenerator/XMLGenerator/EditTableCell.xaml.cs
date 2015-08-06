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

namespace XMLGenerator
{
    /// <summary>
    /// Логика взаимодействия для EditTableCell.xaml
    /// </summary>
    public partial class EditTableCell : Window
    {
        public EditTableCell()
        {
            InitializeComponent();
        }

        public void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            
            Logic.SaveCell(this, MainWindow.GetObjectXML());
            this.Close();
        }

        public  void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
