using System;

namespace Shared.Events
{
    public class ClientEventArg : EventArgs
    {
        public string Number { get; set; }
        public string RemoteAddress { get; set; }
    }
}