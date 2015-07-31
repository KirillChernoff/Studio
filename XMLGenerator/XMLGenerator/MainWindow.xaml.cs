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

        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void tabControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Template3Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Template4Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Template1Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Template2Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Template5Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Template6Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Template8Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Template7Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
