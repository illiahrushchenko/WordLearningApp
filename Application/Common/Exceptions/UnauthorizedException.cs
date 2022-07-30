using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public List<string> Errors { get; set; }

        public UnauthorizedException(IEnumerable<string> errors)
            : base("One or more authorization errors occurred")
        {
            Errors = (List<string>)errors;
        }

        public UnauthorizedException(string error)
            : this(new List<string> { error })
        {
        }
    }
}
