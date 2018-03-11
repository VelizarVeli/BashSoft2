using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.Exceptions
{
    public class NoCourseException:Exception
    {
        private const string NoCourse = "Course {0} can not be found";

        public NoCourseException(string message)
        :base(string.Format(NoCourse, message))
        {
            
        }
    }
}
