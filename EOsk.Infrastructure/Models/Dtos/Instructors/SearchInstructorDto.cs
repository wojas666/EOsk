using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOsk.Infrastructure.Models.Dtos.Instructors
{
    public class SearchInstructorDto
    {
        public string? SearchPhrase { get; set; }

        public string? Pesel { get; set; }
    }
}
