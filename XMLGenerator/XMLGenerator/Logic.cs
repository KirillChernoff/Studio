using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLGenerator
{
    public class Coords
    {
        private int _rowCoord
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
        private int _colCoord
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
        private int _headerCellHeight
        {
            get
            {
                return _headerCellHeight;
            }
            set
            {
                if(value>0)
                _headerCellHeight = value;
            }
        }
        private string _headerCellName
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
        private int _headerCellWidth
        {
            get
            {
                return _headerCellWidth;
            }
            set
            {
                if(value>0)
                _headerCellWidth = value;
            }
        }
        private int _headerCellFontSize
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
        private string _headerCellAlign
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
        private string _headerCellHeader
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
        private string _tabCellAlign
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
        private string _tabCellPrecision
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
        private string _tabCellParametr
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

   /* public  class Table
    {
        public static int GetCoordRow(string Coord)
        {
            char[] splitters = { ' ' };
            int row;
            string[] temp= Coord.Split(splitters);
            row = Convert.ToInt32(temp[0], 10);
            return row;
        }

        public static int GetCoordCol(string Coord)
        {
            char[] splitters = { ' ' };
            int col;
            string[] temp = Coord.Split(splitters);
            col = Convert.ToInt32(temp[1], 10);
            return col;
        }

        public static string SetCoord(int row, int col)
        {
            string coord = row.ToString() + ' ' + col.ToString();
            return coord;
        }

        public static string GetCell(int row,int col, Dictionary<string,string> dict)
        {
            string val;
            val = dict[SetCoord(row, col)];
            return val;
        }

        public static void ReadXML(string XMLName)
        {
            XmlTextReader reader = null;
            try
            {
                reader = new XmlTextReader("MAIN.xml");

                reader.WhitespaceHandling = WhitespaceHandling.None; // пропускаем пустые узлы 

                while (reader.Read())
                    if (reader.NodeType == XmlNodeType.Element)
                        if (reader.Name == "TabColumn")
                        {
                            string order = reader.GetAttribute("Header");


                            Console.WriteLine(order);

                        }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        public Table FillTable(string templateName)
        {
            Table table=new Table();


            return table;
        }

        public Dictionary<string, string> SetCell(int row, int col, string val, Dictionary<string, string> dict)
        {
            dict.Add(SetCoord(row, col), val);
            return dict;
        }

        int _rowNum
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
        int _colNum
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
        Dictionary<string, string> _cells;
        public Table()
        {
            _colNum = 0;
            _rowNum = 0;
            _cells = new Dictionary<string, string>();
        }
        public Table(int rowNum, int colNum, Dictionary<string,string> cells)
        {
            this._colNum = colNum;
            this._rowNum = rowNum;
            this._cells = cells;
        }
    }*/
    static class  Logic
    {
    }
}