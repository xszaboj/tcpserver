using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Commands.Data;

namespace Shared.Factories
{
    public class CommandDataFactory
    {
        private readonly IDictionary<TouchEnum, Type> _dictionary = new Dictionary<TouchEnum, Type>()
        {
            {TouchEnum.SingleClick, typeof(Empty)},
            {TouchEnum.SingleClickDown, typeof(Empty)},
            {TouchEnum.SingleClickUp, typeof(Empty)},
            {TouchEnum.RightClick, typeof(Empty)},
            {TouchEnum.DoubleClick, typeof(Empty)},
            {TouchEnum.Scroll, typeof(ScrollData)},
            {TouchEnum.Move, typeof(MoveData)},
        }; 

        public ICommandData GetData(ReceivedData data)
        {
            Type commandData;
            if (_dictionary.TryGetValue(data.DataType, out commandData))
            {
                return (ICommandData)Activator.CreateInstance(commandData, data);
            }
            throw new ArgumentException("Command data type not found");
        }
    }
}
