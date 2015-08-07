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

    }
}
