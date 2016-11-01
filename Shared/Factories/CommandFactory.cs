using System;
using System.Collections.Generic;
using MouseProject;
using Shared.Commands;
using Shared.Commands.Data;

namespace Shared.Factories
{
    public class CommandFactory
    {
        private readonly IDictionary<TouchEnum, Type> _dictionary = new Dictionary<TouchEnum, Type>()
        {
            {TouchEnum.SingleClick, typeof(SingleClick)},
            {TouchEnum.SingleClickDown, typeof(SingleClickDown)},
            {TouchEnum.SingleClickUp, typeof(SingleClickUp)},
            {TouchEnum.RightClick, typeof(RightClick)},
            {TouchEnum.DoubleClick, typeof(DoubleClick)},
            {TouchEnum.Scroll, typeof(Scroll)}
        }; 

        public ICommand GetCommand(TouchEnum type, IMouseManager manager, ICommandData data)
        {
            Type commandType;
            if (_dictionary.TryGetValue(type, out commandType))
            {
                return (ICommand)Activator.CreateInstance(commandType, manager, data);
            }
            throw new ArgumentException("Command not found");
        }
    }
}
