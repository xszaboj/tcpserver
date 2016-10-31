using System;
using System.Collections.Generic;
using MouseProject;
using Shared.Commands;

namespace Shared.Factories
{
    public class CommandFactory
    {
        private readonly IDictionary<TouchEnum, Type> _dictionary = new Dictionary<TouchEnum, Type>()
        {
            {TouchEnum.Single, typeof(SingleClick)},
            {TouchEnum.SingleClickDown, typeof(SingleClickDown)},
            {TouchEnum.SingleClickUp, typeof(SingleClickUp)},
            {TouchEnum.RightClick, typeof(RightClick)},
            {TouchEnum.DoubleClick, typeof(DoubleClick)},
            {TouchEnum.Scroll, typeof(Scroll)}
        }; 

        public ICommand GetCommand(TouchEnum type, IMouseManager manager)
        {
            Type commandType;
            if (_dictionary.TryGetValue(type, out commandType))
            {
                return (ICommand)Activator.CreateInstance(commandType, manager);
            }
            throw new ArgumentException("Command not found");
        }
    }
}
