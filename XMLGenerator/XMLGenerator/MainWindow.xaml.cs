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

        public void ChooseNewButtonClick(object sender, EventArgs e)
        {
            if (!Logic.CompareXml(objectXML, ForCompareXML))
                return;

            ForCompareXML = new Logic.ObjectXML();
            objectXML = new Logic.ObjectXML();

            ListBox1.Items.Clear();
            EditCell.Visibility = Visibility.Collapsed;
            EditHeader.Visibility = Visibility.Collapsed;
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
            Logic.ClearControls(this);
            LastActiveCoords.rowCoord = 0;
            LastActiveCoords.colCoord = 0;

        }

        private delegate Logic.ObjectXML LoadFile(string PathXML);

        public void ChooseEditButtonClick(object sender, EventArgs e)
        {
            if (!Logic.CompareXml(objectXML, ForCompareXML))
                return;

            PathXML = null;

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

                result = fileload.BeginInvoke(PathXML, null, null);
                ForCompareXML = fileload.EndInvoke(result);

                EditCell.Visibility = Visibility.Collapsed;
                EditHeader.Visibility = Visibility.Collapsed;
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
                Logic.ClearControls(this);
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
                        if (PathXML != null && PathXML != "")
                        {
                            if (!Logic.WriteXml(objectXML, PathXML))
                            {
                                LoadFile fileload = new LoadFile(Logic.ReadXml);
                                IAsyncResult result = fileload.BeginInvoke(PathXML, null, null);
                                ForCompareXML = fileload.EndInvoke(result);
                                result = fileload.BeginInvoke(PathXML, null, null);
                                objectXML = fileload.EndInvoke(result);
                            }
                            return;
                        }
                        else
                        {
                            Logic.SaveAs(objectXML);
                            if (PathXML != null && PathXML != "")
                            {
                                Title = PathXML;
                                LoadFile fileload = new LoadFile(Logic.ReadXml);
                                IAsyncResult result = fileload.BeginInvoke(PathXML, null, null);
                                ForCompareXML = fileload.EndInvoke(result);
                                result = fileload.BeginInvoke(PathXML, null, null);
                                objectXML = fileload.EndInvoke(result);
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
            if (PathXML != null && PathXML != "")
                Title = PathXML;
            if (PathXML == null || PathXML == "") return;
            LoadFile fileload = new LoadFile(Logic.ReadXml);
            IAsyncResult result = fileload.BeginInvoke(PathXML, null, null);
            ForCompareXML = fileload.EndInvoke(result);
            result = fileload.BeginInvoke(PathXML, null, null);
            objectXML = fileload.EndInvoke(result);
        }

        public void ExitClick(object sender, EventArgs e)
        {
            if (!Logic.CompareXml(objectXML, ForCompareXML)) return;
            Close();
        }

        private void DelRow_Click(object sender, RoutedEventArgs e)
        {
            Logic.DelRow(objectXML, LastActiveCoords);
            ListBox1.Items.Clear();
            Logic.DisplayXML(ListBox1, objectXML);
            Logic.ClearControls(this);
        }

        private void DelCol_Click(object sender, RoutedEventArgs e)
        {
            Logic.DelCol(objectXML, LastActiveCoords);
            ListBox1.Items.Clear();
            Logic.DisplayXML(ListBox1, objectXML);
            Logic.ClearControls(this);
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
            EditCell.IsOpen=false;
            EditHeader.IsOpen=true;
            this.EditHeader.DataContext = objectXML.header[new Logic.Coords(row, col).GetHashCode()];
        }

        public void CellEdit(int row, int col)
        {
            EditHeader.IsOpen = false;
            EditCell.IsOpen=true;
            this.EditCell.DataContext = objectXML.cells[new Logic.Coords(row, col).GetHashCode()];

        }

        private void HeaderCancelButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            EditHeader.Visibility = Visibility.Collapsed;
        }

        private void CellCancelButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            EditCell.Visibility = Visibility.Collapsed;
        }
        
        private void EditHeader_IsOpenChanged(object sender, RoutedEventArgs e)
        {
           if((sender as Flyout).IsOpen)
            {
                Adapter.Visibility = Visibility.Visible;
            }
            else
            {
                Adapter.Visibility = Visibility.Collapsed; ;
            }
        }

        private void EditCell_IsOpenChanged(object sender, RoutedEventArgs e)
        {

            if ((sender as Flyout).IsOpen)
            {
                Adapter.Visibility = Visibility.Visible;
            }
            else
            {
                Adapter.Visibility = Visibility.Collapsed; ;
            }
        }

        private void TagDictBtn_Click(object sender, RoutedEventArgs e)
        {
            TagDictionary tgDict = new TagDictionary();
            tgDict.ShowDialog();
        }
    }
}
