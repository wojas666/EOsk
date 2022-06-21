using EOsk.Infrastructure.Models.Dtos.Instructors;
using EOsk.Infrastructure.Responses;

namespace EOsk.Instructor.Api.Services
{
    public interface IInstructorService
    {
        Task<Guid> CreateInstructor(Instructor.Api.Models.Instructor newInstructor);

        Task<IReadOnlyList<Instructor.Api.Models.Instructor>> GetInstructors();

        Task<Instructor.Api.Models.Instructor> GetInstructorById(Guid id);

    }
}
