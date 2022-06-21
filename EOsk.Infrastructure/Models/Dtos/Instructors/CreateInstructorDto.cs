using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOsk.Infrastructure.Models.Dtos.Instructors
{
    public class CreateInstructorDto : IInstructorDto
    {
        public Guid? UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Pesel { get; set; }

        public string RegistrationNumber { get; set; }

        public DateTime? EndOfValidityOfTheCard { get; set; }

        public bool IsActive { get; set; }
    }
}
