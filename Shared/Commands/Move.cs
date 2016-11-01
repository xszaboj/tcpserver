using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MouseProject;
using Shared.Commands.Data;

namespace Shared.Commands
{
    public class Move: BaseCommand
    {
        private readonly MoveData _data;

        public Move(IMouseManager manager, MoveData data)
            : base(manager, data)
        {
            _data = data;
        }

        public override void Execute()
        {
            Manager.MoveCursor(_data.MoveX, _data.MoveY);
        }
    }
}
