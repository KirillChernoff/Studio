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
        public string PathXML;
        public ListBox getListBox()
        {
            return ListBox1;
        }
        
        internal static Logic.ObjectXML GetObjectXML()
        {
            return objectXML;
        }
        
        public void findStyles(object sender)
        {
            (sender as Button).Style=(Style)FindResource("DefaultButtonStyle");
        }

        public void Refresh()
        {
            Logic.DisplayXML(ListBox1, objectXML);
        }

        public void ChooseNewButtonClick(object sender, System.EventArgs e)
        {
            ListBox1.Items.Clear();
        }

        public void ChooseEditButtonClick(object sender, System.EventArgs e)
        {
            ListBox1.Items.Clear();

            OpenFileDialog myDialog = new OpenFileDialog();

            myDialog.Filter = "XML(*.XML;*.xml)|*.Xml;*.xml" + "|Все файлы (*.*)|*.* ";
            myDialog.CheckFileExists = true;
            myDialog.Multiselect = false;
            myDialog.ShowDialog();
            PathXML = myDialog.FileName;

            objectXML=Logic.ReadXml(PathXML);
            Logic.DisplayXML(ListBox1, objectXML);
            

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
            Logic.WriteXml(objectXML,PathXML);
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            Logic.SaveAs(objectXML);
        }

        private void AboutProgram_Click(object sender, RoutedEventArgs e)
        {
            Logic.ShowAbout();
        }
        
        public void ExitClick(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
