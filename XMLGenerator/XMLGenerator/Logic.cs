using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLGenerator
{
   
    public  class Table
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
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("MAIN.xml");
            foreach (XmlNode task in xmlDoc.DocumentElement.ChildNodes)
            {
                Console.WriteLine( xmlDoc.DocumentElement.GetAttribute("Name"));
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
    }
    static class  Logic
    {
    }
}