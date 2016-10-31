using MouseProject;

namespace Shared.Commands
{
    public class SingleClick : BaseCommand
    {
        public SingleClick(IMouseManager manager) : base(manager)
        {
            
        }

        public override void Execute()
        {
            Manager.Click();
        }
    }
}
