using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Commands.Data
{
    public class MoveData : BaseCommandData
    {
        private readonly int _moveX;
        private readonly int _moveY;

        public MoveData(ReceivedData data) : base(data)
        {
            _moveX = data.X;
            _moveY = data.Y;
        }

        public int MoveX
        {
            get { return _moveX; }
        }

        public int MoveY
        {
            get { return _moveY; }
        }
    }
}
