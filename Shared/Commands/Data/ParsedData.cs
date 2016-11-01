using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Commands.Data
{
    public class ParsedData
    {
        public ParsedData()
        {
            
        }

        public ParsedData(TouchEnum action, int x, int y)
        {
            Action = action;
            X = x;
            Y = y;
        }

        public static ParsedData ParseData(string data)
        {
            string[] coords = data.Split('|');
            int x = Int32.Parse(coords[0]);
            int y = Int32.Parse(coords[1]);
            TouchEnum action = (TouchEnum)Enum.Parse(typeof(TouchEnum), coords[2]);
            return new ParsedData(action, x, y);
        }

        public TouchEnum Action { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
