using System;
using System.Runtime.Serialization;

namespace BashSoft.NetCore.Executor.IO.Commands
{
    [Serializable]
    internal class InvalidCommandMessage : Exception
    {
        public InvalidCommandMessage()
        {
        }

        public InvalidCommandMessage(string message) : base(message)
        {
        }

        public InvalidCommandMessage(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidCommandMessage(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}