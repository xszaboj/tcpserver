using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shared;
using Shared.Commands.Data;
using Shared.Factories;

namespace Tests.Factories
{
    [TestFixture]
    public class CommandDataFactoryTests
    {
        [Test]
        public void GetData_empty()
        {
            var factory = new CommandDataFactory();
            var data = factory.GetData(GetReceivedData(TouchEnum.SingleClick));
            Assert.That(data.GetType(), Is.EqualTo(typeof(Empty)));
        }

        [Test]
        public void GetData_scroll()
        {
            var factory = new CommandDataFactory();
            var data = factory.GetData(GetReceivedData(TouchEnum.Scroll));
            Assert.That(data.GetType(), Is.EqualTo(typeof(ScrollData)));
        }

        [Test]
        public void GetData_move()
        {
            var factory = new CommandDataFactory();
            var data = factory.GetData(GetReceivedData(TouchEnum.Move));
            Assert.That(data.GetType(), Is.EqualTo(typeof(MoveData)));
        }

        private ReceivedData GetReceivedData(TouchEnum type)
        {
            return new ReceivedData(1,5, type);
        }
    }
}
