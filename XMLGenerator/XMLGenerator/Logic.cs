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

namespace XMLGenerator
{

    class  Logic : MainWindow
    {
        private static string PathXml
        {
            get { return AppDomain.CurrentDomain.BaseDirectory + "XmlDataView\\"; }
        }

        public class Coords 
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
                return _colCoord+_rowCoord*100000;
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
            public HeaderCell()
            {

            }
            public HeaderCell( int CellHeight, int CellWidth, int CellFontSize, string CellName, string CellAlign, string CellHeader)
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
                    //можно включить проверку на вхождение тега в словарь тегов
                    return _tabCellParametr;
                }
                set
                {
                    _tabCellParametr = value;
                }
            }
            public TabCell()
            {

            }
            public TabCell(string CellAlign, string CellPrecision, string CellParametr)
            {
                _tabCellAlign = CellAlign;
                _tabCellPrecision = CellPrecision;
                _tabCellParametr = CellParametr;
            }
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
            public Dictionary<int,TabCell> cells
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
            private Dictionary<int,HeaderCell> _header;
            public Dictionary<int,HeaderCell> header
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

            public ObjectXML()
            {

            }
            public ObjectXML(int rowNum, int colNum, Dictionary<int, HeaderCell> Header, Dictionary<int, TabCell> Cell)
            {
                _colNum = colNum;
                _rowNum = rowNum;
                _header = Header;
                _cells = Cell;
            }
        }

        public static ObjectXML ReadXml(string FileName)
        {
            ObjectXML table = new ObjectXML();
            table.header = ReadXMLHeader(PathXml+FileName);
            table.cells = ReadXMLCells(PathXml+FileName);
            table.colNum = table.header.Count;
            table.rowNum = (table.cells.Count / table.header.Count) +1;

            return table;
        }

        public static Dictionary<int,HeaderCell> ReadXMLHeader(string FileName)
        {
            Dictionary<int, HeaderCell> HeaderCells = new Dictionary<int, HeaderCell>();

            //Dictionary<string, HeaderCell> HeaderCells2 = new Dictionary<string, HeaderCell>();
            XDocument xdoc = XDocument.Load(FileName);
            int col = 0, row = 0;

            HeaderCell temp;
            Coords coordinate;

            foreach (XElement el in xdoc.Root.Element("TabColumns").Elements())
            {
                temp = new HeaderCell();

                //coordinate = new Coords(row, col);

                //Console.WriteLine(coordinate.ToString());

                temp.headerCellAlign = el.Attribute("Align").Value;
                temp.headerCellName = el.Attribute("Name").Value;
                temp.headerCellFontSize = Convert.ToInt32(el.Attribute("Fontsize").Value,10);
                temp.headerCellHeight = Convert.ToInt32(el.Attribute("Height").Value, 10);
                temp.headerCellWidth = Convert.ToInt32(el.Attribute("Width").Value, 10);
                temp.headerCellHeader = el.Attribute("Header").Value;
                coordinate = new Coords(row, col);
                try
                {
                    HeaderCells.Add(coordinate.GetHashCode(), temp);

                    //HeaderCells2.Add(new Coords(row, col).ToString(), temp);
                }
                catch (ArgumentException)
                {
                    //Console.WriteLine(coordinate.colCoord.ToString(), ' ', coordinate.rowCoord.ToString(), " already exists.");
                }
                //temp = null;
                col++;
            }

           // Coords coordinate1 = new Coords(0, 0);
            //Console.WriteLine(HeaderCells2.ContainsKey(coordinate1.ToString()));

            return HeaderCells;
        }
        public static Dictionary<int, TabCell> ReadXMLCells(string FileName)
        {
            Dictionary<int, TabCell> TabCells = new Dictionary<int, TabCell> ();
            XDocument xdoc = XDocument.Load(FileName);
            int row = 1;
            int col = 0;
            foreach (XElement el in xdoc.Root.Elements("TabRow")) { 
                
                foreach (XElement elem in el.Elements())
                {
                    TabCell temp = new TabCell();

                    Coords coordinate = new Coords(row, col);
                    temp.tabCellAlign = elem.Attribute("Align").Value;
                    temp.tabCellParametr = elem.Attribute("Parametr").Value;
                    temp.tabCellPrecision = elem.Attribute("Precision").Value;

                    try
                    {
                        TabCells.Add(coordinate.GetHashCode(), temp);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("An element with Key = \"txt\" already exists.");
                    }

                    col++;
                    temp = null;
                    
                    
                }
                col = 0;
                row++;
            }
            return TabCells;
        }
        public static void DisplayXML(ListBox ListBox1, ObjectXML table)
        {
            StackPanel panel1 = new StackPanel();
            panel1.Orientation = Orientation.Horizontal;
            for (int col = 0; col < table.colNum; col++)
            {
                Button button = new Button();
                Coords coords= new Coords(0, col);
                button.Name = "field" + '_' + (col + 1).ToString() + '_' + 0.ToString();
                button.Content = table.header[coords.GetHashCode()].headerCellHeader;
                button.Height = 50;
                button.Width = 150;
                button.Click += EditHeaderClick;

                panel1.Children.Add(button);    
            }

            ListBox1.Items.Add(panel1);



            for (int row=1; row < table.rowNum; row++)
            {
                
                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                for (int col = 0; col < table.colNum; col++)
                {
                    Button button = new Button();

                    Coords coords = new Coords(row, col);
                    button.Name = "field"+'_'+(col+1).ToString() + '_'+row.ToString();
                    button.Content = table.cells[coords.GetHashCode()].tabCellParametr;
                    button.Height = 50;
                    button.Width = 150;
                    button.Click += EditCellClick;

                    panel.Children.Add(button);

                }
                ListBox1.Items.Add(panel);
            }


        
           
            
        }

        public static Coords GetCoords(string buttonName)
        {
            Coords coord = new Coords();
            string[] temp = buttonName.Split('_');
            coord.rowCoord = int.Parse( temp[2]);
            coord.colCoord = int.Parse(temp[1])-1;
            return coord;
        }
        public static void OpenEditTableCellWindow( EditTableCell w, TabCell cell )
        {
            w.Show();
            w.ParametrField.Text = cell.tabCellParametr;
            w.AlignField.Text = cell.tabCellAlign;
            w.PrecisionField.Text = cell.tabCellPrecision;
            

        }
        public static void OpenEditHeaderCellWindow(EditHeaderCell w, HeaderCell cell)
        {
            w.Show();
            w.HeaderField.Text = cell.headerCellHeader;
            w.AlignField.Text = cell.headerCellAlign;
            w.HeightField.Text = cell.headerCellHeight.ToString();
            w.WidthField.Text = cell.headerCellWidth.ToString();
            w.NameField.Text = cell.headerCellName;




        }
        public static void EditCellClick(object sender, RoutedEventArgs e)
        {
            Coords t = new Coords();
            t = GetCoords((sender as Button).Name.ToString());
            EditTableCell w = new EditTableCell();
            ObjectXML xml = new ObjectXML();
            xml = GetObjectXML();
            TabCell cell = new TabCell();
            cell=xml.cells[t.GetHashCode()];
            OpenEditTableCellWindow(w, cell);
        }
        public static void EditHeaderClick(object sender, RoutedEventArgs e)
        {
            Coords t = new Coords();
            t = GetCoords((sender as Button).Name.ToString());
            EditHeaderCell w = new EditHeaderCell();
            ObjectXML xml = new ObjectXML();
            xml = GetObjectXML();
            HeaderCell cell = new HeaderCell();
            cell = xml.header[t.GetHashCode()];
            OpenEditHeaderCellWindow(w, cell);
        }
        

   
    }

}