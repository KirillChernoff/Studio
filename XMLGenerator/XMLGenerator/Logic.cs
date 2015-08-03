using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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

        public Table ReadXml(string FileName)
        {
            Table table = new Table();
            table.header = ReadXMLHeader();
            table.cells = ReadXMLCells();
            table.colNum = table.header.Count;
            table.rowNum = (table.cells.Count / table.header.Count) +1;

            return table;
        }

        public List<HeaderCell> ReadXMLHeader()
        {
            List<HeaderCell> HeaderCells = new List<HeaderCell>();

            return HeaderCells;
        }
        public List<TabCell> ReadXMLCells()
        {
            List<TabCell> TabCells = new List<TabCell>();

            return TabCells;
        }

    }

}