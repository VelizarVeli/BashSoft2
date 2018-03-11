using System;

namespace BashSoft.NetCore
{
    public class InvalidStringException : Exception
    {
        private const string InvalidInput = "The name should not be empty!";

        public InvalidStringException()
            : base(InvalidInput)
        {
        }

        public InvalidStringException(string message)
            : base(message)
        {
        }
    }
}