using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MouseProject;

namespace Shared.Commands
{
    public class DoubleClick : BaseCommand
    {
        public DoubleClick(IMouseManager manager) : base(manager)
        {
        }

        public override void Execute()
        {
            Manager.DoubleClick();
        }
    }
}
