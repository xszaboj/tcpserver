namespace Shared.TCPWrappers
{
    public interface ITcpClient
    {
        bool IsConnected();
        INetworkStream GetStream();
        int ReceiveBufferSize { get;}
        void Close();
    }
}
