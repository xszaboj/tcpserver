using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Commands.Data
{
    public class ReceivedData
    {
        private readonly TouchEnum _dataType;
        public int X { get; set; }
        public int Y { get; set; }

        public TouchEnum DataType
        {
            get { return _dataType; }
        }

        public ReceivedData(int x, int y, TouchEnum dataType)
        {
            _dataType = dataType;
            X = x;
            Y = y;
        }
    }
}
