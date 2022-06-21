using EOsk.Infrastructure.Models.Dtos.Instructors;
using EOsk.Infrastructure.Repository.Contract;
using EOsk.Instructor.Api.DbContexts;

namespace EOsk.Instructor.Api.Repository.Contract
{
    public interface IInstructorRepository : IGenericRepository<EOsk.Instructor.Api.Models.Instructor, ApplicationDbContext>
    {
        Task<List<Instructor.Api.Models.Instructor>> SearchInstructor(SearchInstructorDto searchInstructor);
    }
}
