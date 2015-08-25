using Microsoft.Win32;
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
using System.Xml;
using MahApps.Metro.Controls;
using System.ComponentModel;
using MahApps.Metro.Controls.Dialogs;
using System.Reflection;

namespace XMLGenerator
{

    public partial class MainWindow : MetroWindow
    {

        public MainWindow()
        {

            InitializeComponent();

            Logic.getRes += findStyles;
            Logic.save += Refresh;
            Logic.HeaderClick += HeaderEdit;
            Logic.CellClick += CellEdit;

            string[] MyAlign = new string[] { "Top", "Bottom", "Left", "Right", "Center" }; 
            string[] MyPres = new string[] { "N", "H", "1", "2", "3", "4", "5", "6", "7", "8", "9", };

            CellPrecisionBox.ItemsSource = MyPres;
            CellAlignBox.ItemsSource = MyAlign;
            HeaderAlignBox.ItemsSource = MyAlign;
            
        }



        internal static Logic.Coords LastActiveCoords = new Logic.Coords();


        protected override void OnClosing(CancelEventArgs e)
        {
            if (!Logic.CompareXml(objectXML, ForCompareXML)) e.Cancel = true;
            base.OnClosing(e);
        }

        private Logic.ObjectXML ForCompareXML = new Logic.ObjectXML();

        internal static Logic.ObjectXML objectXML = new Logic.ObjectXML();

        public static string PathXML;

        public static uint MaxRow = 20;
        public static uint MaxCol = 20;

        internal static Logic.ObjectXML GetObjectXML()
        {
            return objectXML;
        }

        public void findStyles(object sender)
        {
            (sender as Button).Style = (Style)FindResource("DefaultButtonStyle");
        }

        public void Refresh()
        {
            Logic.DisplayXML(ListBox1, objectXML);
        }

        public void ChooseNewButtonClick(object sender, System.EventArgs e)
        {
            if (!Logic.CompareXml(objectXML, ForCompareXML))
                return;

            ForCompareXML = new Logic.ObjectXML();
            objectXML = new Logic.ObjectXML();

            ListBox1.Items.Clear();
            Logic.DisplayXML(ListBox1, objectXML);

            AddColBtn.Visibility = Visibility.Visible;
            AddRowBtn.Visibility = Visibility.Visible;
            DelColBtn.Visibility = Visibility.Visible;
            DelRowBtn.Visibility = Visibility.Visible;
            SaveAsBtn.Visibility = Visibility.Visible;
            SaveBtn.Visibility = Visibility.Visible;

            SaveAsMenuBtn.IsEnabled = true;
            SaveMenuBtn.IsEnabled = true;

            Title = "Untitled";
            PathXML = null;
            LastActiveCoords.rowCoord = 0;
            LastActiveCoords.colCoord = 0;

        }

        private delegate Logic.ObjectXML LoadFile(string PathXML);

        public void ChooseEditButtonClick(object sender, System.EventArgs e)
        {
            if (!Logic.CompareXml(objectXML, ForCompareXML))
                return;

            OpenFileDialog myDialog = new OpenFileDialog();

            myDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            myDialog.CheckFileExists = true;
            myDialog.Multiselect = false;
            myDialog.ShowDialog();

            PathXML = myDialog.FileName;

            if (PathXML == null || PathXML == "") return;

            try
            {
                LoadFile fileload = new LoadFile(Logic.ReadXml);
                IAsyncResult result = fileload.BeginInvoke(PathXML, null, null);
                objectXML = fileload.EndInvoke(result);

                ForCompareXML = objectXML;

                ListBox1.Items.Clear();
                Logic.DisplayXML(ListBox1, objectXML);

                AddColBtn.Visibility = Visibility.Visible;
                AddRowBtn.Visibility = Visibility.Visible;
                DelColBtn.Visibility = Visibility.Visible;
                DelRowBtn.Visibility = Visibility.Visible;
                SaveAsBtn.Visibility = Visibility.Visible;
                SaveBtn.Visibility = Visibility.Visible;

                SaveAsMenuBtn.IsEnabled = true;
                SaveMenuBtn.IsEnabled = true;

                Title = PathXML;
                LastActiveCoords.rowCoord = 0;
                LastActiveCoords.colCoord = 0;
            }
            catch (XmlException)
            {
                Logic.FileErrorDialog(this);
            }


        }

        private void AddRowClick(object sender, RoutedEventArgs e)
        {
            Logic.AddRow(objectXML, LastActiveCoords);
            ListBox1.Items.Clear();
            Logic.DisplayXML(ListBox1, objectXML);
        }

        private void AddColClick(object sender, RoutedEventArgs e)
        {
            Logic.AddCol(objectXML, LastActiveCoords);
            ListBox1.Items.Clear();
            Logic.DisplayXML(ListBox1, objectXML);
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {

            MessageBoxResult saveConfirm = MessageBox.Show("Save Changes?", "Save Changes?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (saveConfirm)
            {
                case MessageBoxResult.Yes:
                    {
                        if (MainWindow.PathXML != null)
                        {
                            if (!Logic.WriteXml(objectXML, MainWindow.PathXML)) ;
                            ForCompareXML = objectXML;
                            return;
                        }
                        else
                        {
                            Logic.SaveAs(objectXML);
                            if (PathXML != null)
                            {
                                Title = PathXML;
                                ForCompareXML = objectXML;
                            }
                            return;
                        }
                    }
                case MessageBoxResult.No:
                    return;
            }

        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            Logic.SaveAs(objectXML);
            Title = PathXML;
            ForCompareXML = objectXML;
        }

        public void ExitClick(object sender, System.EventArgs e)
        {
            if (!Logic.CompareXml(objectXML, ForCompareXML)) return;
            Close();
        }

        private void DelRow_Click(object sender, RoutedEventArgs e)
        {
            Logic.DelRow(objectXML, LastActiveCoords);
            ListBox1.Items.Clear();
            Logic.DisplayXML(ListBox1, objectXML);
        }

        private void DelCol_Click(object sender, RoutedEventArgs e)
        {
            Logic.DelCol(objectXML, LastActiveCoords);
            ListBox1.Items.Clear();
            Logic.DisplayXML(ListBox1, objectXML);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Settings Sett = new Settings();

            Sett.ShowDialog();
        }

        private void AboutProgram_Click(object sender, RoutedEventArgs e)
        {
            Logic.ShowAbout(this);
        }

        public void HeaderEdit(int row, int col)
        {
            EditHeader.Visibility = Visibility.Visible;
            EditCell.Visibility = Visibility.Collapsed;

            HeaderField.Text = objectXML.header[new Logic.Coords(row, col).GetHashCode()].headerCellHeader;
            HeightField.Value = objectXML.header[new Logic.Coords(row, col).GetHashCode()].headerCellHeight;
            WidthField.Value = objectXML.header[new Logic.Coords(row, col).GetHashCode()].headerCellWidth;
            HeaderNameField.Text = objectXML.header[new Logic.Coords(row, col).GetHashCode()].headerCellName;
            HeaderAlignBox.SelectedItem = objectXML.header[new Logic.Coords(row, col).GetHashCode()].headerCellAlign;
            FontsizeField.Value = objectXML.header[new Logic.Coords(row, col).GetHashCode()].headerCellFontSize;
        }

        public void CellEdit(int row,int col)
        {
            EditHeader.Visibility = Visibility.Collapsed;
            EditCell.Visibility = Visibility.Visible;

            CellParametrField.Text = objectXML.cells[new Logic.Coords(row, col).GetHashCode()].tabCellParametr;
            CellAlignBox.SelectedItem = objectXML.cells[new Logic.Coords(row, col).GetHashCode()].tabCellAlign;
            CellPrecisionBox.SelectedItem = objectXML.cells[new Logic.Coords(row, col).GetHashCode()].tabCellPrecision;

        }

        #region Header Edit Buttons Event Handlers

        private void HeaderSaveButton_Click(object sender, RoutedEventArgs e)
        {
            Logic.SaveHeader(this, MainWindow.GetObjectXML());
        }

        private void HeaderCancChButton_Click(object sender, RoutedEventArgs e)
        {
            HeaderField.Text = objectXML.header[LastActiveCoords.GetHashCode()].headerCellHeader;
            HeightField.Value = objectXML.header[LastActiveCoords.GetHashCode()].headerCellHeight;
            WidthField.Value = objectXML.header[LastActiveCoords.GetHashCode()].headerCellWidth;
            HeaderNameField.Text = objectXML.header[LastActiveCoords.GetHashCode()].headerCellName;
            HeaderAlignBox.SelectedItem = objectXML.header[LastActiveCoords.GetHashCode()].headerCellAlign;
            FontsizeField.Value = objectXML.header[LastActiveCoords.GetHashCode()].headerCellFontSize;
        }
        
        private void HeaderRestoreButton_Click(object sender, RoutedEventArgs e)
        {
            HeaderField.Text = "";
            HeightField.Value = 25;
            WidthField.Value = 80;
            HeaderNameField.Text = "";
            FontsizeField.Value = 14;
            HeaderAlignBox.SelectedItem = "Center";
        }

        private void HeaderCancelButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            EditHeader.Visibility = Visibility.Collapsed;
        }

        #endregion Edit Header Buttons Event Handlers

        #region Edit Cell Buttons Events Handlers 
        private void CellSaveButton_Click(object sender, RoutedEventArgs e)
        {
            Logic.SaveCell(this, MainWindow.GetObjectXML());
        }

        private void CellCancChButton_Click(object sender, RoutedEventArgs e)
        {
            CellParametrField.Text = objectXML.cells[LastActiveCoords.GetHashCode()].tabCellParametr;
            CellAlignBox.SelectedItem = objectXML.cells[LastActiveCoords.GetHashCode()].tabCellAlign;
            CellPrecisionBox.SelectedItem = objectXML.cells[LastActiveCoords.GetHashCode()].tabCellPrecision;
        }

        private void CellRestoreButton_Click(object sender, RoutedEventArgs e)
        {
            CellParametrField.Text = "";
            CellAlignBox.SelectedItem ="Center";
            CellPrecisionBox.SelectedItem = "N";
        }
        
        private void CellCancelButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            EditCell.Visibility = Visibility.Collapsed;
        }
        #endregion
    }
}
