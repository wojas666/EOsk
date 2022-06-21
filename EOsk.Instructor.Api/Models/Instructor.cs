using EOsk.Infrastructure.Models.Common;

namespace EOsk.Instructor.Api.Models
{
    public class Instructor : BaseDomainEntity
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
