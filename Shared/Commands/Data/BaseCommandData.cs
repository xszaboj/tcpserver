namespace Shared.Commands.Data
{
    public abstract class BaseCommandData : ICommandData
    {
        protected readonly ReceivedData ReceivedData;

        protected BaseCommandData(ReceivedData data)
        {
            ReceivedData = data;
        }
    }
}