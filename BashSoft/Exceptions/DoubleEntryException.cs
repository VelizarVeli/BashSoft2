using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.Exceptions
{
    public class DoubleEntryException:Exception
    {
       
            private const string DoubleEntry = "The {0} already exists in {1}.";

            public DoubleEntryException(string message)
                : base(message)
            {
            }

            public DoubleEntryException(string entry, string structure)
                : base(String.Format(DoubleEntry, entry, structure))
            {

            }

        
    }
}
