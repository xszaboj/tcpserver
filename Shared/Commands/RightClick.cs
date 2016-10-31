using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MouseProject;

namespace Shared.Commands
{
    public class RightClick :BaseCommand
    {
        public RightClick(IMouseManager manager) : base(manager)
        {
        }

        public override void Execute()
        {
            Manager.RightClick();
        }
    }
}
