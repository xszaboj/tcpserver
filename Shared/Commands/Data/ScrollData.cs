using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Commands.Data
{
    public class ScrollData : BaseCommandData
    {
        private readonly int _scrollValue;

        public ScrollData(ReceivedData data)
            : base(data)
        {
            _scrollValue = ReceivedData.Y;
        }

        public int ScrollValue
        {
            get { return _scrollValue; }
        }
    }
}
