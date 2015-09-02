using MahApps.Metro.Controls;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    class Tag
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int id { get; set; }

        [MaxLength(30), Unique]
        public string Path { get; set; }

        [MaxLength(30)]
        public string Parent { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        public Tag() { }
        public Tag(int id, string Path, string Parent, string Name)
        {
            this.id = id;
            this.Name = Name;
            this.Parent = Parent;
            this.Path = Path;
        }
    }



    public partial class TagDictionary : MetroWindow
    {


        private Dictionary<string, string> OneTagElements;
        private Dictionary<string, string> TwoTagElements;
        private Dictionary<string, string> ThreeTagElements;
        private Dictionary<string, string> FourTagElements;
        private Dictionary<string, string> FiveTagElements;
        private TreeViewItem oneItem;
        private TreeViewItem twoItem;
        private TreeViewItem threeItem;
        private TreeViewItem fourItem;
        private TreeViewItem fiveItem;

        private void getFromDB(out Dictionary<string, string> dict, string Parent)
        {
            string dbPath = "test.sqlite";
            var db = new SQLiteConnection(dbPath, true);
            var temp = db.Query<Tag>("SELECT * FROM Tag WHERE Parent='" + Parent + "'");
            dict = new Dictionary<string, string>();
            foreach (Tag el in temp)
            {

                dict.Add(el.Path, el.Name);
            }

            db.Dispose();
        }


        public TagDictionary()
        {
            InitializeComponent();
            getFromDB(out OneTagElements, "0");
            foreach (KeyValuePair<string, string> El in OneTagElements)
            {
                oneItem = new TreeViewItem();
                oneItem.Expanded += Item_Expanded;
                oneItem.Header = El.Value;
                oneItem.Tag = "1_" + El.Key;
                oneItem.Items.Add("*");
                this.TagDictView.Items.Add(oneItem);
            }

        }

        private void Item_Expanded(object sender, RoutedEventArgs e)
        {
            string[] temp = ((TreeViewItem)e.Source).Tag.ToString().Split('_');
            switch (temp[0])
            {

                case "0":
                    getFromDB(out OneTagElements, temp[1]);
                    TagDictView.Items.Clear();
                    foreach (KeyValuePair<string, string> El in OneTagElements)
                    {
                        oneItem = new TreeViewItem();

                        oneItem.Header = El.Key;
                        oneItem.Tag = "1_" + El.Key;
                        oneItem.Items.Add("*");
                        this.TagDictView.Items.Add(oneItem);
                    }
                    break;
                case "1":
                    getFromDB(out TwoTagElements, temp[1]);
                    ((TreeViewItem)e.Source).Items.Clear();
                    foreach (KeyValuePair<string, string> El in TwoTagElements)
                    {
                        twoItem = new TreeViewItem();
                        twoItem.Header = El.Key;
                        twoItem.Tag = "2_" + El.Key;
                        twoItem.Items.Add("*");
                        ((TreeViewItem)e.Source).Items.Add(twoItem);
                    }
                    break;
                case "2":
                    getFromDB(out ThreeTagElements, temp[1]);
                    ((TreeViewItem)e.Source).Items.Clear();
                    foreach (KeyValuePair<string, string> El in ThreeTagElements)
                    {
                        threeItem = new TreeViewItem();
                        threeItem.Header = El.Key;
                        threeItem.Tag = "3_" + El.Key;
                        threeItem.Items.Add("*");
                        ((TreeViewItem)e.Source).Items.Add(threeItem);
                    }
                    break;
                case "3":
                    getFromDB(out FourTagElements, temp[1]);
                    ((TreeViewItem)e.Source).Items.Clear();
                    foreach (KeyValuePair<string, string> El in FourTagElements)
                    {
                        fourItem = new TreeViewItem();
                        fourItem.Header = El.Key;
                        fourItem.Tag = "4_" + El.Key;
                        fourItem.Items.Add("*");
                        ((TreeViewItem)e.Source).Items.Add(fourItem);
                    }
                    break;
                case "4":
                    getFromDB(out FiveTagElements, temp[1]);
                    ((TreeViewItem)e.Source).Items.Clear();
                    foreach (KeyValuePair<string, string> El in FiveTagElements)
                    {
                        fiveItem = new TreeViewItem();
                        fiveItem.Selected += FiveItem_Selected;
                        fiveItem.Unselected += FiveItem_Unselected;
                        fiveItem.Header = El.Key;
                        fiveItem.Tag = "5_" + El.Key;
                        ((TreeViewItem)e.Source).Items.Add(fiveItem);
                    }
                    break;
            }

        }

        private void FiveItem_Unselected(object sender, RoutedEventArgs e)
        {
            OkTagDictBtn.IsEnabled = false;
        }

        private void FiveItem_Selected(object sender, RoutedEventArgs e)
        {
            OkTagDictBtn.IsEnabled = true;
        }

        private void OkTagDictBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.getTagRez(MainWindow.objectXML.cells[MainWindow.LastActiveCoords.GetHashCode()], TagDictView.SelectedItem.ToString().Split(' ', ':')[2]);
            this.Close();
        }

        private void CancelTagDictBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
