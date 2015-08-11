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

        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            ParametrField.Text = "";
            AlignField.Text = "Center";
            PrecisionField.Text = "N";
        }

        private void CancChButton_Click(object sender, RoutedEventArgs e)
        {
            ParametrField.Text = MainWindow.objectXML.cells[new Logic.Coords(int.Parse(row.Text), int.Parse(col.Text)).GetHashCode()].tabCellParametr;
            AlignField.Text = MainWindow.objectXML.cells[new Logic.Coords(int.Parse(row.Text), int.Parse(col.Text)).GetHashCode()].tabCellAlign;
            PrecisionField.Text = MainWindow.objectXML.cells[new Logic.Coords(int.Parse(row.Text), int.Parse(col.Text)).GetHashCode()].tabCellPrecision;

        }
    }
}
