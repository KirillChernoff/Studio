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

namespace XMLGenerator
{

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            Logic.getRes += this.findStyles;
            Logic.save += this.Refresh;
            InitializeComponent();
        }
        
        

        internal static Logic.ObjectXML objectXML = new Logic.ObjectXML();
        public static string PathXML;
        public ListBox getListBox()
        {
            return ListBox1;
        }
        public static  int MaxRow = 20;
        public static int MaxCol = 20;


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
            objectXML = new Logic.ObjectXML();
            ListBox1.Items.Clear();
            Logic.DisplayXML(ListBox1, objectXML);
            AddColBtn.Visibility = Visibility.Visible;
            AddRowBtn.Visibility = Visibility.Visible;
            DelColBtn.Visibility = Visibility.Visible;
            DelRowBtn.Visibility = Visibility.Visible;
            SaveAsBtn.Visibility = Visibility.Visible;
            SaveBtn.Visibility = Visibility.Visible;
                
        }

        public void ChooseEditButtonClick(object sender, System.EventArgs e)
        {

            OpenFileDialog myDialog = new OpenFileDialog();

            myDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            myDialog.CheckFileExists = true;
            myDialog.Multiselect = false;
            myDialog.ShowDialog();
            PathXML = myDialog.FileName;


            try
            {
                objectXML = Logic.ReadXml(PathXML);
                ListBox1.Items.Clear();
                Logic.DisplayXML(ListBox1, objectXML);

                AddColBtn.Visibility = Visibility.Visible;
                AddRowBtn.Visibility = Visibility.Visible;
                DelColBtn.Visibility = Visibility.Visible;
                DelRowBtn.Visibility = Visibility.Visible;
                SaveAsBtn.Visibility = Visibility.Visible;
                SaveBtn.Visibility = Visibility.Visible;

            }
            catch (ArgumentException)
            {

            }
            catch (XmlException)
            {
                Logic.FileErrorDialog();
            }

            this.Title = PathXML;
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
            try
            {
                Logic.WriteXml(objectXML, PathXML);
            }
            catch (ArgumentNullException)
            {
                try
                {
                    Logic.SaveAs(objectXML);
                    this.Title = PathXML;
                }
                catch (ArgumentException)
                {
                    return;
                }
            }
            catch (ArgumentException)
            {
                try
                {
                    Logic.SaveAs(objectXML);

                    this.Title=PathXML ;
                }
                catch (ArgumentException)
                {
                    return;
                }

            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Logic.SaveAs(objectXML);
                
                this.Title = PathXML;
            }
            catch (ArgumentException)
            {
                return;
            }
        }

        private void AboutProgram_Click(object sender, RoutedEventArgs e)
        {
            Logic.ShowAbout();
        }

        public void ExitClick(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void DelRow_Click(object sender, RoutedEventArgs e)
        {
            Logic.DelRow(objectXML);
            ListBox1.Items.Clear();
            Logic.DisplayXML(ListBox1, objectXML);
        }

        private void DelCol_Click(object sender, RoutedEventArgs e)
        {
            Logic.DelCol(objectXML);
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
