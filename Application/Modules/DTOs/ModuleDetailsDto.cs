using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Modules.DTOs
{
    public class ModuleDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public bool IsPublic { get; set; }
        public List<CardDto> Words { get; set; }
    }
}
