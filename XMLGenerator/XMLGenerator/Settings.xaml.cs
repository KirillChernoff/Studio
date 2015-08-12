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
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            maxCol.Text = MainWindow.MaxCol.ToString();
            maxRow.Text = MainWindow.MaxRow.ToString();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            if (maxCol.Text != null)
            {
                try
                {
                    MainWindow.MaxCol = uint.Parse(maxCol.Text);
                }
                catch
                {
                    MessageBox.Show("Max number of columns should be natural number", "Ошибка при вводе значения",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            if (maxRow.Text != null)
            {
                try
                {
                    MainWindow.MaxRow = uint.Parse(maxRow.Text);
                }
                catch
                {
                    MessageBox.Show("Max number of rows should be natural number", "Ошибка при вводе значения",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
