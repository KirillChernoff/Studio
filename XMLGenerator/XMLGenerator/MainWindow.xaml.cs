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
        public void ExitClick(object sender, System.EventArgs e)
        {
            this.Close();
        }

        public void ChooseNewButtonClick(object sender, System.EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        public void ChooseEditButtonClick(object sender, System.EventArgs e)
        {
            Logic.Table TestTable = Logic.ReadXml("_MAIN.xml");
        }

        

        public MainWindow()
        {

            InitializeComponent();
        }

        
        private void UseTemplate1Button_Click(object sender, RoutedEventArgs e)
        {
            ListBox1.Items.Clear();

            Logic.Table table = new Logic.Table();
            table = Logic.ReadXml("_MAIN.xml");
            Logic.DisplayXML(ListBox1, table);

            tabControl1.SelectedIndex = 2;

        }

        private void UseTemplate2Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void UseTemplate3Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UseTemplate4Button_Click(object sender, RoutedEventArgs e)
        {

        }


        private void UseTemplate5Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UseTemplate6Button_Click(object sender, RoutedEventArgs e)
        {

        }


        private void UseTemplate7Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UseTemplate8Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BackToMainClick(object sender, RoutedEventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void AddRowClick(object sender, RoutedEventArgs e)
        {
        }

        private void AddColClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {

        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
