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
    /// Логика взаимодействия для EditHeaderCell.xaml
    /// </summary>
    public partial class EditHeaderCell : Window
    {
        
        
        public EditHeaderCell()
        {
            
            InitializeComponent();
        }
        
        internal  void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            Logic.SaveHeader(this,MainWindow.GetObjectXML());
            }
            catch (FormatException)
            {
                Logic.ErrorDialog();
                return;
            }
            
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CancChButton_Click(object sender, RoutedEventArgs e)
        {
            HeaderField.Text = MainWindow.objectXML.header[new Logic.Coords(int.Parse(row.Text), int.Parse(col.Text)).GetHashCode()].headerCellHeader;
            AlignField.Text = MainWindow.objectXML.header[new Logic.Coords(int.Parse(row.Text), int.Parse(col.Text)).GetHashCode()].headerCellAlign;
            HeightField.Text = MainWindow.objectXML.header[new Logic.Coords(int.Parse(row.Text), int.Parse(col.Text)).GetHashCode()].headerCellHeight.ToString();
            WidthField.Text = MainWindow.objectXML.header[new Logic.Coords(int.Parse(row.Text), int.Parse(col.Text)).GetHashCode()].headerCellWidth.ToString();
            NameField.Text = MainWindow.objectXML.header[new Logic.Coords(int.Parse(row.Text), int.Parse(col.Text)).GetHashCode()].headerCellName;
            FontsizeField.Text = MainWindow.objectXML.header[new Logic.Coords(int.Parse(row.Text), int.Parse(col.Text)).GetHashCode()].headerCellFontSize.ToString();
        }

        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            HeaderField.Text = "";
            AlignField.Text = "Center";
            HeightField.Text = "25";
            WidthField.Text = "80";
            NameField.Text = "";
            FontsizeField.Text = "14";
        }
    }
}
