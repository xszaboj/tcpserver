using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shared;
using Shared.Commands;
using Shared.Factories;

namespace Tests.Factories
{
    [TestFixture]
    public class CommandFactoryTests
    {
        [TestCase(TouchEnum.Move, typeof(Move))]
        [TestCase(TouchEnum.SingleClick, typeof(SingleClick))]
        [TestCase(TouchEnum.SingleClickDown, typeof(SingleClickDown))]
        [TestCase(TouchEnum.SingleClickUp, typeof(SingleClickUp))]
        [TestCase(TouchEnum.DoubleClick, typeof(DoubleClick))]
        [TestCase(TouchEnum.RightClick, typeof(RightClick))]
        [TestCase(TouchEnum.Scroll, typeof(Scroll))]
        public void GetCommand(TouchEnum type, Type objectType)
        {
            CommandFactory factory = new CommandFactory();
            ICommand command = factory.GetCommand(type, null, null);
            Assert.That(command.GetType(), Is.EqualTo(objectType));
        }

        [Test]
        public void GetCommand_Argument_Exception()
        {
            CommandFactory factory = new CommandFactory();
            Assert.That(() => factory.GetCommand((TouchEnum)7, null, null), Throws.Exception.TypeOf<ArgumentException>().With.Message.EqualTo("Command not found"));
        }
    }
}
