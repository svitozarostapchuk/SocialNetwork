using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF.Exceptions
{
    public class NoRecordFoundException : ArgumentException
    {
        public NoRecordFoundException()
        {
        }

        public NoRecordFoundException(string message) : base(message)
        {
        }
    }
}
