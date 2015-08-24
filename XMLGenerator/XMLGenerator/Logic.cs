using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using MahApps.Metro.Controls.Dialogs;

namespace XMLGenerator
{

    class Logic
    {

        internal class Coords
        {
            private int _rowCoord;
            public int rowCoord
            {
                get
                {
                    return _rowCoord;
                }
                set
                {
                    _rowCoord = value;
                }
            }
            private int _colCoord;
            public int colCoord
            {
                get
                {
                    return _colCoord;
                }
                set
                {
                    _colCoord = value;
                }
            }

            public override int GetHashCode()
            {
                return _colCoord + _rowCoord * 100000;
            }
            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                Coords t = obj as Coords;
                return (rowCoord == t.rowCoord && colCoord == t.colCoord);
            }

            public Coords()
            {
            }
            public Coords(int RowCoord, int ColCoord)
            {
                _rowCoord = RowCoord;
                _colCoord = ColCoord;
            }
        }

        public class HeaderCell
        {
            private int _headerCellHeight;
            public int headerCellHeight
            {
                get
                {
                    return _headerCellHeight;
                }
                set
                {
                    if (value > 0)
                        _headerCellHeight = value;
                    else _headerCellHeight = Math.Abs(value);
                }
            }

            private string _headerCellName;
            public string headerCellName
            {
                get
                {
                    return _headerCellName;
                }
                set
                {
                    _headerCellName = value;
                }
            }

            private int _headerCellWidth;
            public int headerCellWidth
            {
                get
                {
                    return _headerCellWidth;
                }
                set
                {
                    if (value > 0)
                        _headerCellWidth = value;
                    else _headerCellWidth = Math.Abs(value);
                }
            }

            private int _headerCellFontSize;
            public int headerCellFontSize
            {
                get
                {
                    return _headerCellFontSize;
                }
                set
                {
                    if (value > 0)
                        _headerCellFontSize = value;
                    else _headerCellFontSize = Math.Abs(value);
                }

            }

            private string _headerCellAlign;
            public string headerCellAlign
            {
                get
                {
                    return _headerCellAlign;
                }
                set
                {
                    _headerCellAlign = value;
                }

            }

            private string _headerCellHeader;
            public string headerCellHeader
            {
                get
                {
                    return _headerCellHeader;
                }
                set
                {
                    _headerCellHeader = value;
                }
            }
       
            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                HeaderCell t = obj as HeaderCell;
                return (headerCellAlign == t.headerCellAlign &&
                    headerCellFontSize == t.headerCellFontSize &&
                    headerCellHeader == t.headerCellHeader &&
                    headerCellHeight == t.headerCellHeight &&
                    headerCellName == t.headerCellName &&
                    headerCellWidth == t.headerCellWidth);
            }

            public HeaderCell()
            {
                _headerCellAlign = "Center";
                _headerCellFontSize = 14;
                _headerCellHeight = 25;
                _headerCellWidth = 80;
                _headerCellName = "";
                _headerCellHeader = "Header";
            }
            public HeaderCell(int CellHeight, int CellWidth, int CellFontSize, string CellName, string CellAlign, string CellHeader)
            {
                _headerCellHeight = CellHeight;
                _headerCellWidth = CellWidth;
                _headerCellFontSize = CellFontSize;
                _headerCellName = CellName;
                _headerCellAlign = CellAlign;
                _headerCellHeader = CellHeader;
            }

        }

        public class TabCell
        {
            private string _tabCellAlign;
            public string tabCellAlign
            {
                get
                {
                    return _tabCellAlign;
                }
                set
                {
                    _tabCellAlign = value;
                }
            }

            private string _tabCellPrecision;
            public string tabCellPrecision
            {
                get
                {
                    return _tabCellPrecision;
                }
                set
                {
                    _tabCellPrecision = value;
                }
            }
            private string _tabCellParametr;
            public string tabCellParametr
            {
                get
                {
                    return _tabCellParametr;
                }
                set
                {
                    _tabCellParametr = value;
                }
            }
            private string _name;
            public string Name
            {
                get
                {
                    return _name;
                }
                set
                {
                    _name = value;
                }
            }
            private string _number;
            public string Number
            {
                get
                {
                    return _number;
                }
                set
                {
                    _number = value;
                }
            }


            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                TabCell t = obj as TabCell;
                return (tabCellAlign == t.tabCellAlign &&
                    Name == t.Name &&
                    tabCellParametr == t.tabCellParametr &&
                    Number == t.Number &&
                    tabCellPrecision == t.tabCellPrecision);
            }

            public TabCell()
            {
                _tabCellAlign = "Center";
                _tabCellPrecision = "N";
                _tabCellParametr = "";
                _name = "  ";
                _number = "  ";
            }
            public TabCell(string CellAlign, string CellPrecision, string CellParametr)
            {
                _tabCellAlign = CellAlign;
                _tabCellPrecision = CellPrecision;
                _tabCellParametr = CellParametr;
            }

        }



        public static bool EqualCells(TabCell first, TabCell Second)
        {
            return (first.Name == Second.Name && first.Number == Second.Number && first.tabCellAlign == Second.tabCellAlign && first.tabCellParametr == Second.tabCellParametr && first.tabCellPrecision == Second.tabCellPrecision);
        }

        internal class ObjectXML
        {
            int _rowNum;
            public int rowNum
            {
                get
                {
                    return _rowNum;
                }
                set
                {
                    _rowNum = value;
                }
            }
            int _colNum;
            public int colNum
            {
                get
                {
                    return _colNum;
                }
                set
                {
                    _colNum = value;
                }
            }
            private Dictionary<int, TabCell> _cells;
            public Dictionary<int, TabCell> cells
            {
                get
                {
                    return _cells;
                }
                set
                {
                    _cells = value;
                }
            }
            private Dictionary<int, HeaderCell> _header;
            public Dictionary<int, HeaderCell> header
            {
                get
                {
                    return _header;
                }
                set
                {
                    _header = value;
                }
            }

            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                ObjectXML t = obj as ObjectXML;

                return ((Enumerable.SequenceEqual(header, t.header)) &&
                    (Enumerable.SequenceEqual(cells, t.cells)) &&
                    (rowNum == t.rowNum) &&
                    (colNum == t.colNum));
            }

            public ObjectXML()
            {
                _colNum = 1;
                _rowNum = 2;
                _cells = new Dictionary<int, TabCell>();
                _cells.Add(new Coords(1, 0).GetHashCode(), new TabCell());
                _header = new Dictionary<int, HeaderCell>();
                _header.Add(new Coords(0, 0).GetHashCode(), new HeaderCell());
            }
            public ObjectXML(int rowNum, int colNum, Dictionary<int, HeaderCell> Header, Dictionary<int, TabCell> Cell)
            {
                _colNum = colNum;
                _rowNum = rowNum;
                _header = Header;
                _cells = Cell;
            }
        }

        internal static ObjectXML ReadXml(string FileName)
        {
            ObjectXML table = new ObjectXML();

            table.header = ReadXMLHeader(FileName);
            table.cells = ReadXMLCells(FileName);

            table.colNum = table.header.Count;
            table.rowNum = (table.cells.Count / table.header.Count) + 1;

            return table;
        }

        public static Dictionary<int, HeaderCell> ReadXMLHeader(string FileName)
        {
            Dictionary<int, HeaderCell> HeaderCells = new Dictionary<int, HeaderCell>();

            XDocument xdoc = new XDocument();
            xdoc = XDocument.Load(FileName);
            int col = 0, row = 0;

            HeaderCell temp;
            Coords coordinate;

            foreach (XElement el in xdoc.Root.Element("TabColumns").Elements())
            {
                temp = new HeaderCell();

                temp.headerCellAlign = el.Attribute("Align").Value;
                temp.headerCellName = el.Attribute("Name").Value;
                temp.headerCellFontSize = Convert.ToInt32(el.Attribute("Fontsize").Value, 10);
                temp.headerCellHeight = Convert.ToInt32(el.Attribute("Height").Value, 10);
                temp.headerCellWidth = Convert.ToInt32(el.Attribute("Width").Value, 10);
                temp.headerCellHeader = el.Attribute("Header").Value;

                coordinate = new Coords(row, col);

                HeaderCells.Add(coordinate.GetHashCode(), temp);

                col++;
            }

            return HeaderCells;
        }

        public static Dictionary<int, TabCell> ReadXMLCells(string FileName)
        {
            Dictionary<int, TabCell> TabCells = new Dictionary<int, TabCell>();
            XDocument xdoc = XDocument.Load(FileName);
            int row = 1;
            int col = 0;

            string RowName = "";
            string RowNumber = "";

            foreach (XElement el in xdoc.Root.Elements("TabRow"))
            {
                try
                {
                    RowName = el.Attribute("Name").Value;
                    RowNumber = el.Attribute("Number").Value;
                }
                catch
                {
                    RowName = "  ";
                    RowNumber = "  ";
                }

                foreach (XElement elem in el.Elements())
                {
                    TabCell temp = new TabCell();
                    Coords coordinate = new Coords(row, col);

                    temp.tabCellAlign = elem.Attribute("Align").Value;
                    temp.tabCellParametr = elem.Attribute("Parametr").Value;
                    temp.tabCellPrecision = elem.Attribute("Precision").Value;
                    temp.Name = RowName;
                    temp.Number = RowNumber;

                    TabCells.Add(coordinate.GetHashCode(), temp);

                    col++;
                    temp = null;


                }
                col = 0;
                row++;
            }
            return TabCells;
        }

        public delegate void resGet(object sender);

        public static event resGet getRes;

        internal static void DisplayXML(ListBox ListBox1, ObjectXML table)
        {
            ListBox1.Items.Clear();

            StackPanel panel1 = new StackPanel();
            panel1.Orientation = Orientation.Horizontal;
            ListBox1.VerticalAlignment = VerticalAlignment.Center;
            ListBox1.HorizontalAlignment = HorizontalAlignment.Center;

            for (int col = 0; col < table.colNum; col++)
            {
                Button button = new Button();
                Coords coords = new Coords(0, col);

                button.Name = "field" + '_' + (col + 1).ToString() + '_' + 0.ToString();
                button.Content = table.header[coords.GetHashCode()].headerCellHeader;
                button.Height = table.header[coords.GetHashCode()].headerCellHeight;
                button.Width = table.header[coords.GetHashCode()].headerCellWidth; ;
                button.Click += EditHeaderClick;
                getRes(button);

                button.Background = Brushes.LightGray;
                button.BorderBrush = Brushes.Black;

                if (coords.Equals(MainWindow.LastActiveCoords)) button.Background = Brushes.MediumPurple;

                panel1.Children.Add(button);
            }

            ListBox1.Items.Add(panel1);

            for (int row = 1; row < table.rowNum; row++)
            {

                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                for (int col = 0; col < table.colNum; col++)
                {
                    Button button = new Button();
                    Coords coords = new Coords(row, col);

                    button.Name = "field" + '_' + (col + 1).ToString() + '_' + row.ToString();
                    button.Content = table.cells[coords.GetHashCode()].tabCellParametr;
                    button.Height = table.header[new Coords(0, col).GetHashCode()].headerCellHeight;
                    button.Width = table.header[new Coords(0, col).GetHashCode()].headerCellWidth;
                    button.Click += EditCellClick;

                    getRes(button);

                    button.Background = Brushes.LightGray;
                    button.BorderBrush = Brushes.Black;

                    if (coords.Equals(MainWindow.LastActiveCoords)) button.Background = Brushes.MediumPurple;

                    panel.Children.Add(button);
                }

                ListBox1.Items.Add(panel);
            }
        }

        public static Coords GetCoords(string buttonName)
        {
            Coords coord = new Coords();

            string[] temp = buttonName.Split('_');

            coord.rowCoord = int.Parse(temp[2]);
            coord.colCoord = int.Parse(temp[1]) - 1;

            return coord;
        }

        public static void OpenEditTableCellWindow(EditTableCell w, TabCell cell, Coords coord)
        {
            w.ParametrField.Text = cell.tabCellParametr;
            w.AlignField.SelectedItem = cell.tabCellAlign;
            w.PrecisionField.SelectedItem = cell.tabCellPrecision;

            w.col.Text = coord.colCoord.ToString();
            w.row.Text = coord.rowCoord.ToString();

            w.ShowDialog();
        }

        public static void OpenEditHeaderCellWindow(EditHeaderCell w, HeaderCell cell, Coords coord)
        {
            w.HeaderField.Text = cell.headerCellHeader;
            w.HeightField.Value = cell.headerCellHeight;
            w.WidthField.Value = cell.headerCellWidth;
            w.NameField.Text = cell.headerCellName;
            w.FontsizeField.Value = cell.headerCellFontSize;
            w.AlignBox.SelectedItem = cell.headerCellAlign;

            w.col.Text = coord.colCoord.ToString();
            w.row.Text = coord.rowCoord.ToString();

            w.ShowDialog();
        }

        public static void EditCellClick(object sender, RoutedEventArgs e)
        {
            Coords t = new Coords();
            EditTableCell w = new EditTableCell();
            ObjectXML xml = new ObjectXML();
            TabCell cell = new TabCell();

            t = GetCoords((sender as Button).Name.ToString());
            xml = MainWindow.GetObjectXML();
            cell = xml.cells[t.GetHashCode()];
            MainWindow.LastActiveCoords = t;

            OpenEditTableCellWindow(w, cell, t);
        }

        public static void EditHeaderClick(object sender, RoutedEventArgs e)
        {
            Coords t = new Coords();
            t = GetCoords((sender as Button).Name.ToString());
            EditHeaderCell w = new EditHeaderCell();
            ObjectXML xml = new ObjectXML();
            xml = MainWindow.GetObjectXML();
            HeaderCell cell = xml.header[t.GetHashCode()];
            MainWindow.LastActiveCoords = t;

            OpenEditHeaderCellWindow(w, cell, t);
        }

        public static void ErrorDialog()
        {
            MessageBox.Show("Введено неверное значение", "Ошибка при вводе значения",
            MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void AddRow(ObjectXML objectXML, Coords LastActive)
        {
            if (objectXML.rowNum < MainWindow.MaxRow)
            {
                for (int i = 0; i < objectXML.colNum; i++)
                {
                    Coords t = new Coords(objectXML.rowNum, i);
                    objectXML.cells.Add(t.GetHashCode(), new TabCell());
                }

                objectXML.rowNum++;

                for(int row=objectXML.rowNum-1;row> LastActive.rowCoord; row--)
                {
                    for(int col = 0; col < objectXML.colNum; col++)
                    {
                        if (row> 1)
                        {
                        objectXML.cells[new Coords(row, col).GetHashCode()] = objectXML.cells[new Coords(row-1, col).GetHashCode()];
                        }
                    }
                }
                for (int col = 0; col < objectXML.colNum; col++)
                {
                    objectXML.cells[new Coords(LastActive.rowCoord+1, col).GetHashCode()] =new TabCell();
                }
            
            MainWindow.objectXML = objectXML;
            }
        }

        public static void AddCol(ObjectXML objectXML, Coords LastActive)
        {
            if (objectXML.colNum < MainWindow.MaxCol)
            {
                Coords temp = new Coords(0, objectXML.colNum);

                objectXML.header.Add(temp.GetHashCode(), new HeaderCell());

                for (int i = 1; i < objectXML.rowNum; i++)
                {
                    temp = new Coords(i, objectXML.colNum);
                    objectXML.cells.Add(temp.GetHashCode(), new TabCell());
                }

                objectXML.colNum++;

                for(int col = objectXML.colNum - 1; col > LastActive.colCoord; col--)
                {
                    objectXML.header[new Coords(0, col).GetHashCode()] = objectXML.header[new Coords(0, col-1).GetHashCode()];
                }
                objectXML.header[new Coords(0, LastActive.colCoord + 1).GetHashCode()] = new HeaderCell();

                for(int row = 1; row< objectXML.rowNum; row++)
                {
                    for (int col = objectXML.colNum - 1; col > LastActive.colCoord; col--)
                    {
                        objectXML.cells[new Coords(row, col).GetHashCode()] = objectXML.cells[new Coords(row, col-1).GetHashCode()];
                    }
                }
                for (int row = 0  ; row <objectXML.rowNum ; row++)//если поменять на -- то будет драть МНОГО памяти
                {
                    objectXML.cells[new Coords(row, LastActive.colCoord+1).GetHashCode()] = new TabCell();
                }

                MainWindow.objectXML = objectXML;
            }
        }

        public static bool CompareXml(ObjectXML a, ObjectXML b)
        {
            if (!Equals(a, b))
            {
                MessageBoxResult result = MessageBox.Show("Save Changes?", "Save Changes?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        {
                            if (MainWindow.PathXML != null)
                            {
                                WriteXml(a, MainWindow.PathXML);
                                return true;
                            }
                            else
                            {
                                SaveAs(a);
                                return true;
                            }
                        }

                    case MessageBoxResult.No:
                        return true;

                    case MessageBoxResult.Cancel:
                        return false;
                }
            }
            return true;
        }

        public delegate void saving();

        public static event saving save;

        public static void SaveHeader(EditHeaderCell w, ObjectXML objectXML)
        {
            HeaderCell t = new HeaderCell(
                (int)w.HeightField.Value,
                (int)w.WidthField.Value,
                (int)w.FontsizeField.Value,
                w.NameField.Text,
                w.AlignBox.SelectedItem.ToString() ,
                w.HeaderField.Text);

            objectXML.header[new Coords(int.Parse(w.row.Text), int.Parse(w.col.Text)).GetHashCode()] = t;

            MainWindow.objectXML = objectXML;

            save();
        }

        public static void SaveCell(EditTableCell w, ObjectXML objectXML)
        {
            TabCell t = new TabCell(
                w.AlignField.SelectedItem.ToString(),
                w.PrecisionField.SelectedItem.ToString(),
                w.ParametrField.Text);

            objectXML.cells[new Coords(int.Parse(w.row.Text), int.Parse(w.col.Text)).GetHashCode()] = t;

            MainWindow.objectXML = objectXML;

            save();
        }

        public static void SaveAs(ObjectXML objectXML)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.ShowDialog();

            MainWindow.PathXML = saveFileDialog1.FileName;
            if (!WriteXml(objectXML, saveFileDialog1.FileName)) return;
        }

        public static bool WriteXml(ObjectXML objectXml, string filename)
        {
            XDocument doc = new XDocument();
            XElement Root = new XElement("DataSetTable");

            doc.Add(Root);

            XElement Header = new XElement("TabColumns");

            for (int col = 0; col < objectXml.header.Count; col++)
            {
                Coords coords = new Coords(0, col);
                Header.Add(new XElement("TabColumn",
                    new XAttribute("Name", objectXml.header[coords.GetHashCode()].headerCellName),
                    new XAttribute("Fontsize", objectXml.header[coords.GetHashCode()].headerCellFontSize),
                    new XAttribute("Height", objectXml.header[coords.GetHashCode()].headerCellHeight),
                    new XAttribute("Width", objectXml.header[coords.GetHashCode()].headerCellWidth),
                    new XAttribute("Align", objectXml.header[coords.GetHashCode()].headerCellAlign),
                    new XAttribute("Header", objectXml.header[coords.GetHashCode()].headerCellHeader)));
            }

            doc.Root.Add(Header);

            for (int row = 1; row < objectXml.rowNum; row++)
            {
                XElement tableRow;

                if (objectXml.cells[new Coords(row, 0).GetHashCode()].Name != "  ")
                {
                    tableRow = new XElement("TabRow",
                        new XAttribute("Name", objectXml.cells[new Coords(row, 0).GetHashCode()].Name),
                        new XAttribute("Number", objectXml.cells[new Coords(row, 0).GetHashCode()].Number));
                }

                else
                {
                    tableRow = new XElement("TabRow");
                }

                for (int col = 0; col < objectXml.colNum; col++)
                {
                    Coords coords = new Coords(row, col);
                    tableRow.Add(new XElement("TabCell",
                        new XAttribute("Align", objectXml.cells[coords.GetHashCode()].tabCellAlign),
                        new XAttribute("Precision", objectXml.cells[coords.GetHashCode()].tabCellPrecision),
                        new XAttribute("Parametr", objectXml.cells[coords.GetHashCode()].tabCellParametr)));
                }

                doc.Root.Add(tableRow);
            }

            filename = MainWindow.PathXML;
            if (filename == null || filename == "") return false;
            doc.Save(filename);

            return true;
        }

        public static void DelRow(ObjectXML objectXML, Coords LastActive)
        {

            if (LastActive.rowCoord != 0 && objectXML.rowNum > 2)
            {
                if (LastActive.rowCoord < (objectXML.rowNum - 1))
                {
                    for (int row = LastActive.rowCoord; row < objectXML.rowNum - 2; row++)
                    {
                        for (int col = 0; col < objectXML.colNum ; col++)
                        {
                            Coords temp = new Coords(row, col);
                            objectXML.cells[temp.GetHashCode()] = objectXML.cells[new Coords(row + 1, col).GetHashCode()];
                        }
                    }
                }
                for (int i = 0; i < objectXML.colNum; i++)
                {
                    objectXML.cells.Remove(new Coords(objectXML.rowNum - 1, i).GetHashCode());
                }

                objectXML.rowNum--;

                if (MainWindow.LastActiveCoords.rowCoord > objectXML.rowNum - 1)
                    MainWindow.LastActiveCoords.rowCoord = objectXML.rowNum - 1;

                MainWindow.objectXML = objectXML;
            }

        }

        public static void DelCol(ObjectXML objectXML, Coords LastActive)
        {
            if (objectXML.colNum > 1)
            {
                for (int col = LastActive.colCoord; col < objectXML.colNum - 2; col++)
                {
                    objectXML.header[new Coords(0, col).GetHashCode()] = objectXML.header[new Coords(0, col + 1).GetHashCode()];
                }

                Coords temp = new Coords(0, objectXML.colNum - 1);

                objectXML.header.Remove(temp.GetHashCode());

                for (int row = 1; row < objectXML.rowNum; row++)
                {
                    for (int col = LastActive.colCoord; col < objectXML.colNum - 2; col++)
                    {
                        objectXML.cells[new Coords(row, col).GetHashCode()] = objectXML.cells[new Coords(row, col + 1).GetHashCode()];
                    }

                    objectXML.cells.Remove(new Coords(row, objectXML.colNum - 1).GetHashCode());
                }
                objectXML.colNum--;

                if (MainWindow.LastActiveCoords.colCoord > objectXML.colNum - 1)
                    MainWindow.LastActiveCoords.colCoord = objectXML.colNum - 1;

                MainWindow.objectXML = objectXML;
            }
        }

        public static void FileErrorDialog(MainWindow w)
        {
            w.ShowMessageAsync("Неверный или поврежденный файл", "Ошибка чтения файла");
        }

        public static void ShowAbout(MainWindow w)
        {
            w.ShowMessageAsync("About","XML Generator version 0.0.4(Alpha). No rights reserved");
        }
    }
}