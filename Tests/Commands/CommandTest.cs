using Moq;
using MouseProject;
using NUnit.Framework;
using Shared;
using Shared.Commands;
using Shared.Commands.Data;

namespace Tests.Commands
{
    [TestFixture]
    public class CommandTest
    {
        private Mock<IMouseManager> _mouseMock;

        [SetUp]
        public void init()
        {
            _mouseMock = new Mock<IMouseManager>();
        }

        [Test]
        public void SingleClickCommand()
        {
            _mouseMock.Setup(s=> s.Click()).Verifiable();
            ICommand cmd = new SingleClick(_mouseMock.Object, GetEmpty());
            cmd.Execute();
            _mouseMock.Verify(a=> a.Click(), Times.Once);
        }
        
        [Test]
        public void SingleClickDownCommand()
        {
            _mouseMock.Setup(s => s.ClickDown()).Verifiable();
            ICommand cmd = new SingleClickDown(_mouseMock.Object, GetEmpty());
            cmd.Execute();
            _mouseMock.Verify(a => a.ClickDown(), Times.Once);
        }

        [Test]
        public void SingleClickUpCommand()
        {
            _mouseMock.Setup(s => s.ClickUp()).Verifiable();
            ICommand cmd = new SingleClickUp(_mouseMock.Object, GetEmpty());
            cmd.Execute();
            _mouseMock.Verify(a => a.ClickUp(), Times.Once);
        }

        [Test]
        public void RightClickCommand()
        {
            _mouseMock.Setup(s => s.RightClick()).Verifiable();
            ICommand cmd = new RightClick(_mouseMock.Object, GetEmpty());
            cmd.Execute();
            _mouseMock.Verify(a => a.RightClick(), Times.Once);
        }

        [Test]
        public void DoubleClickCommand()
        {
            _mouseMock.Setup(s => s.DoubleClick()).Verifiable();
            ICommand cmd = new DoubleClick(_mouseMock.Object, GetEmpty());
            cmd.Execute();
            _mouseMock.Verify(a => a.DoubleClick(), Times.Once);
        }

        [Test]
        public void ScrollCommand()
        {
            _mouseMock.Setup(s => s.Scroll(It.Is<int>(i=> i == 5))).Verifiable();
            ICommand cmd = new Scroll(_mouseMock.Object, new ScrollData(new ReceivedData(0,5,TouchEnum.Scroll)));
            cmd.Execute();
            _mouseMock.Verify(a => a.Scroll(It.Is<int>(i => i == 5)), Times.Once);
        }

        private Empty GetEmpty()
        {
            return new Empty(new ReceivedData(0,0,TouchEnum.Single));
        }
    }
}
