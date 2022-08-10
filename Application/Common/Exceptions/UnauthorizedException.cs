using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public string[] Errors { get; set; }

        public UnauthorizedException(string[] errors)
            : base("One or more authorization errors occurred")
        {
            Errors = errors;
        }

        public UnauthorizedException(string error)
            : this(new string[] { error })
        {
        }
    }
}
