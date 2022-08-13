using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class PermissionDeniedException : Exception
    {
        public PermissionDeniedException(string userEmail)
            : base($"User {userEmail} does'nt have access to this resource")
        {
        }

        public PermissionDeniedException(int userId)
            : base($"User {userId} does'nt have access to this resource")
        {
        }
    }
}
