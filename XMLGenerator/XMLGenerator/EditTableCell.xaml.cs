using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using MahApps.Metro.Controls;

namespace XMLGenerator
{
    public partial class EditTableCell : MetroWindow
    {
        public string[] MyAlign = new string[] { "Top", "Bottom", "Left", "Right", "Center" };
        public string[] MyPres = new string[] { "N", "H", "1", "2", "3", "4", "5", "6", "7", "8", "9", };
        public EditTableCell()
        {
            InitializeComponent();
            AlignField.ItemsSource = MyAlign;
            PrecisionField.ItemsSource = MyPres;
        }

        public delegate void refresh();

        public static refresh save;

        protected override void OnClosing(CancelEventArgs e)
        {
            MainWindow.LastActiveCoords.colCoord = int.Parse(col.Text);
            MainWindow.LastActiveCoords.rowCoord = int.Parse(row.Text);
            save();
            base.OnClosing(e);
        }

        public void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.LastActiveCoords.colCoord = int.Parse(col.Text);
            MainWindow.LastActiveCoords.rowCoord = int.Parse(row.Text);

            Logic.SaveCell(this, MainWindow.GetObjectXML());
            Close();
        }

        public void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            save();
            Close();
        }

        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            ParametrField.Text = "";
            AlignField.SelectedItem = "Center";
            PrecisionField.SelectedItem = "N";
        }

        private void CancChButton_Click(object sender, RoutedEventArgs e)
        {
            ParametrField.Text = MainWindow.objectXML.cells[new Logic.Coords(int.Parse(row.Text), int.Parse(col.Text)).GetHashCode()].tabCellParametr;
            AlignField.SelectedItem = MainWindow.objectXML.cells[new Logic.Coords(int.Parse(row.Text), int.Parse(col.Text)).GetHashCode()].tabCellAlign;
            PrecisionField.SelectedItem = MainWindow.objectXML.cells[new Logic.Coords(int.Parse(row.Text), int.Parse(col.Text)).GetHashCode()].tabCellPrecision;

        }
    }
}
