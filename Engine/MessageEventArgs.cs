using System;

namespace Engine.Contracts
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(string message)
        {
            Message = message ?? "";
        }

        public string Message;
    }
}