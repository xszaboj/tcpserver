using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Commands.Data
{
    public class ScrollData
    {
        private readonly int _scrollValue;

        public ScrollData(int scrollValue)
        {
            this._scrollValue = scrollValue;
        }

        public int ScrollValue
        {
            get { return _scrollValue; }
        }
    }
}
