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

namespace XMLGenerator
{
    public partial class EditHeaderCell : Window
    {
        protected override void OnClosed(EventArgs e)
        {
            MainWindow.LastActiveCoords.colCoord = int.Parse(col.Text);
            MainWindow.LastActiveCoords.rowCoord = int.Parse(row.Text);
            save();
            base.OnClosed(e);
        }


        public string[] MyAlign = new string[] { "Top", "Bottom", "Left", "Right", "Center" };
        public EditHeaderCell()
        {
            

            InitializeComponent();
            AlignBox.ItemsSource = MyAlign;
        }

        internal void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.LastActiveCoords.colCoord = int.Parse(col.Text);
                MainWindow.LastActiveCoords.rowCoord = int.Parse(row.Text);
                Logic.SaveHeader(this, MainWindow.GetObjectXML());
            }
            catch (FormatException)
            {
                Logic.ErrorDialog();
                return;
            }

            Close();
        }
        public delegate void refresh();
        public static refresh save;

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.LastActiveCoords.colCoord = int.Parse(col.Text);
            MainWindow.LastActiveCoords.rowCoord = int.Parse(row.Text);

            save();
            Close();
        }

        private void CancChButton_Click(object sender, RoutedEventArgs e)
        {
            HeaderField.Text = MainWindow.objectXML.header[new Logic.Coords(int.Parse(row.Text), int.Parse(col.Text)).GetHashCode()].headerCellHeader;
            HeightField.Text = MainWindow.objectXML.header[new Logic.Coords(int.Parse(row.Text), int.Parse(col.Text)).GetHashCode()].headerCellHeight.ToString();
            WidthField.Text = MainWindow.objectXML.header[new Logic.Coords(int.Parse(row.Text), int.Parse(col.Text)).GetHashCode()].headerCellWidth.ToString();
            NameField.Text = MainWindow.objectXML.header[new Logic.Coords(int.Parse(row.Text), int.Parse(col.Text)).GetHashCode()].headerCellName;
            AlignBox.SelectedItem= MainWindow.objectXML.header[new Logic.Coords(int.Parse(row.Text), int.Parse(col.Text)).GetHashCode()].headerCellAlign;
            FontsizeField.Text = MainWindow.objectXML.header[new Logic.Coords(int.Parse(row.Text), int.Parse(col.Text)).GetHashCode()].headerCellFontSize.ToString();
        }

        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            HeaderField.Text = "";
            HeightField.Text = "25";
            WidthField.Text = "80";
            NameField.Text = "";
            FontsizeField.Text = "14";
            AlignBox.SelectedItem = "Center";
        }

    }
}
