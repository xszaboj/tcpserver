namespace Shared.TCPWrappers
{
    public interface INetworkStream
    {
        int Read(byte[] buffer, int offset, int size);
        bool DataAvailable { get;}
    }
}