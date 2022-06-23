using EOsk.Infrastructure.Models.Dtos.Instructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOsk.Infrastructure.Events.Instructors.Requestes.Commands
{
    public class CreateInstructorCommand 
    {
        public CreateInstructorDto InstructorToCreated { get; set; }
    }
}
