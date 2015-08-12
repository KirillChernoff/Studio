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
using System.ComponentModel;

namespace XMLGenerator
{

    public partial class MainWindow : Window
    {

        public MainWindow()
        {

            Logic.getRes += findStyles;
            Logic.save += Refresh;
            EditHeaderCell.save += Refresh;
            EditTableCell.save += Refresh;
            InitializeComponent();
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

        public ListBox getListBox()
        {
            return ListBox1;
        }

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
            if (!Logic.CompareXml(objectXML, ForCompareXML)) return;
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


        }

        public void ChooseEditButtonClick(object sender, System.EventArgs e)
        {

            if (!Logic.CompareXml(objectXML, ForCompareXML)) return;

            OpenFileDialog myDialog = new OpenFileDialog();

            myDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            myDialog.CheckFileExists = true;
            myDialog.Multiselect = false;
            myDialog.ShowDialog();
            PathXML = myDialog.FileName;


            try
            {
                objectXML = Logic.ReadXml(PathXML);
                ForCompareXML = Logic.ReadXml(PathXML);
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
            }
            catch (ArgumentException)
            {

            }
            catch (XmlException)
            {
                Logic.FileErrorDialog();
            }
        }

        private void AddRowClick(object sender, RoutedEventArgs e)
        {
            Logic.AddRow(objectXML);
            ListBox1.Items.Clear();
            Logic.DisplayXML(ListBox1, objectXML);
        }

        private void AddColClick(object sender, RoutedEventArgs e)
        {
            Logic.AddCol(objectXML);
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

        private void AboutProgram_Click(object sender, RoutedEventArgs e)
        {
            Logic.ShowAbout();
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
    }
}
