using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.Exceptions
{
    public class InvalidPathException:Exception
    {
        public const string InvalidPath = "The source doesn't exist.";

        public InvalidPathException()
        :base(InvalidPath)
        {

        }

        public InvalidPathException(string message)
        :base(message)
        {
            
        }
    }
}
