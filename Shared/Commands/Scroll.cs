using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MouseProject;
using Shared.Commands.Data;

namespace Shared.Commands
{
    public class Scroll: BaseCommand
    {
        private readonly ScrollData _data;

        public Scroll(IMouseManager manager, ScrollData data)
            : base(manager, data)
        {
            _data = data;
        }

        public override void Execute()
        {
            Manager.Scroll(_data.ScrollValue);
        }
    }
}
