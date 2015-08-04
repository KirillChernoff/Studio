using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Controls;

namespace XMLGenerator
{

    class  Logic
    {
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
            private Coords _headerCellCoord;
            public Coords headerCellCoord
            {
                get
                {
                    return _headerCellCoord;
                }
                set
                {
                    _headerCellCoord = value;
                }
            }
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
            public HeaderCell(Coords CellCoord, int CellHeight, int CellWidth, int CellFontSize, string CellName, string CellAlign, string CellHeader)
            {
                _headerCellCoord = CellCoord;
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
            private Coords _tabCellCoord;
            public Coords tabCellCoord
            {
                get
                {
                    return _tabCellCoord;
                }
                set
                {
                    _tabCellCoord = value;
                }
            }
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
            public TabCell(Coords CellCoord, string CellAlign, string CellPrecision, string CellParametr)
            {
                Coords temp = new Coords();
                temp = CellCoord;
                _tabCellCoord = CellCoord;
                _tabCellAlign = CellAlign;
                _tabCellPrecision = CellPrecision;
                _tabCellParametr = CellParametr;
            }
        }

        public class Table
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
            private List<HeaderCell> _header;
            public List<HeaderCell> header
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
            private List<TabCell> _cells;
            public List<TabCell> cells
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
            public Table()
            {

            }
            public Table(int rowNum, int colNum, List<HeaderCell> Header, List<TabCell> Cell)
            {
                _colNum = colNum;
                _rowNum = rowNum;
                _header = Header;
                _cells = Cell;
            }
        }

        public static Table ReadXml(string FileName)
        {
            Table table = new Table();
            table.header = ReadXMLHeader(FileName);
            table.cells = ReadXMLCells(FileName);
            table.colNum = table.header.Count;
            table.rowNum = (table.cells.Count / table.header.Count) +1;

            return table;
        }

        public static List<HeaderCell> ReadXMLHeader(string FileName)
        {
            List<HeaderCell> HeaderCells = new List<HeaderCell>();
            XDocument xdoc = XDocument.Load(FileName);
            Coords coordinate = new Coords(0, 0);

            foreach (XElement el in xdoc.Root.Element("TabColumns").Elements())
            {
                HeaderCell temp = new HeaderCell();
                temp.headerCellAlign = el.Attribute("Align").Value;
                temp.headerCellName = el.Attribute("Name").Value;
                temp.headerCellFontSize = Convert.ToInt32(el.Attribute("Fontsize").Value,10);
                temp.headerCellHeight = Convert.ToInt32(el.Attribute("Height").Value, 10);
                temp.headerCellWidth = Convert.ToInt32(el.Attribute("Width").Value, 10);
                temp.headerCellHeader = el.Attribute("Header").Value;

                temp.headerCellCoord = coordinate;
                coordinate.colCoord++;
                HeaderCells.Add(temp);;
            }
            return HeaderCells;
        }
        public static void DisplayXML(ListBox ListBox1, Table table)
        {
            
            

            
            
            
            for(int j=0; j < table.colNum; j++)
            {
                List < string > temp = new List<string>();
                for(int i = 0; i < table.cells.Count; i++)
                {
                    Console.WriteLine(table.cells[i].tabCellParametr);
                    Console.WriteLine(table.cells[i].tabCellCoord.colCoord);
                    Console.WriteLine(table.cells[i].tabCellCoord.rowCoord);
                    if (table.cells[i].tabCellCoord.rowCoord == (j+1))
                    {
                        temp.Add(table.cells[i].tabCellParametr);

                    }   
                }
                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                for (int i = 0; i < table.colNum; i++)
                {
                    TextBlock block = new TextBlock();

                    block.Name = "field"+'_'+(i+1).ToString() + '_'+j.ToString();
                    block.Text = temp[i];
                    block.Height = 50;
                    block.Width = 150;
                    panel.Children.Add(block);

                }
                temp.Clear();
                ListBox1.Items.Add(panel);
            }


        
           
            
        }
        public static List<TabCell> ReadXMLCells(string FileName)
        {
            List<TabCell> TabCells = new List<TabCell>();
            XDocument xdoc = XDocument.Load(FileName);
            int row = 1;
            int col = 0;
            foreach (XElement el in xdoc.Root.Elements()) { 
                
                foreach (XElement elem in xdoc.Root.Element("TabRow").Elements())
                {
                    TabCell temp = new TabCell();

                    Coords coordinate = new Coords(row, col);
                    temp.tabCellAlign = elem.Attribute("Align").Value;
                    temp.tabCellParametr = elem.Attribute("Parametr").Value;
                    temp.tabCellPrecision = elem.Attribute("Precision").Value;
                   
                    temp.tabCellCoord = coordinate;
                    TabCells.Add(temp);
                    col++;
                    temp = null;
                    
                    
                }
                col = 0;
                row++;
            }
            return TabCells;
        }
        

   
    }

}