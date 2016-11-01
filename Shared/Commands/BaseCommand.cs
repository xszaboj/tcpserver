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
        protected readonly ICommandData Data;

        protected BaseCommand(IMouseManager manager, ICommandData data)
        {
            Manager = manager;
            Data = data;
        }

        public abstract void Execute();
    }
}
