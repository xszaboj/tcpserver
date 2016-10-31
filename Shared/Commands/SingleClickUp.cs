using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MouseProject;

namespace Shared.Commands
{
    public class SingleClickUp : BaseCommand
    {
        public SingleClickUp(IMouseManager manager) : base(manager)
        {
        }

        public override void Execute()
        {
            Manager.ClickUp();
        }
    }
}
