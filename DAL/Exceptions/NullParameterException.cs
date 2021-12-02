using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF.Exceptions
{
    public class NullParameterException : ArgumentException
    {
        public NullParameterException()
        {
        }

        public NullParameterException(string message) : base(message)
        {
        }
    }
}
