using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ProblemDetailses
{
    public class AuthorizationProblemDetails : ProblemDetails
    {
        public List<string> Errors { get; }

        public AuthorizationProblemDetails(List<string> errors) : base()
        {
            Errors = errors;
            Title = "One or many authorization errors occurred";
        }

        public AuthorizationProblemDetails() : base()
        {
            Errors = new List<string>();
        }
    }
}
