using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MouseProject;
using Shared.Commands.Data;

namespace Shared.Commands
{
    public class DoubleClick : BaseCommand
    {
        public DoubleClick(IMouseManager manager, Empty data)
            : base(manager, data)
        {
        }

        public override void Execute()
        {
            Manager.DoubleClick();
        }
    }
}
