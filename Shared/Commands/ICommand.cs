using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Commands.Data;

namespace Shared.Commands
{
    public interface ICommand
    {
        void Execute();
    }
}
