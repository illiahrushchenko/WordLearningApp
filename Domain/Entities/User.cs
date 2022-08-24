using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public List<Module> Modules { get; set; }
        public List<LearningProgress> LearningProgresses { get; set; }
    }
}
