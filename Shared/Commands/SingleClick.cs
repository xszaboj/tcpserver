using MouseProject;
using Shared.Commands.Data;

namespace Shared.Commands
{
    public class SingleClick : BaseCommand
    {
        public SingleClick(IMouseManager manager, Empty data)
            : base(manager, data)
        {
        }

        public override void Execute()
        {
            Manager.Click();
        }
    }
}
