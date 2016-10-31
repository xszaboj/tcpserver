using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MouseProject;
using Shared.Commands.Data;

namespace Shared.Commands
{
    public abstract class BaseCommand : ICommand
    {
        protected IMouseManager Manager;

        protected BaseCommand(IMouseManager manager)
        {
            Manager = manager;
        }

        public abstract void Execute();
    }
}
