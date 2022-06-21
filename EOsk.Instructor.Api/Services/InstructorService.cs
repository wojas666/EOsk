using EOsk.Infrastructure.Models.Dtos.Instructors;
using EOsk.Infrastructure.Responses;
using EOsk.Instructor.Api.Repository.Contract;

namespace EOsk.Instructor.Api.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _repository;

        public InstructorService(IInstructorRepository instructorRepository)
        {
            _repository = instructorRepository;
        }

        public async Task<Guid> CreateInstructor(Instructor.Api.Models.Instructor newInstructor)
        {
            var instructor = await _repository.Add(newInstructor);
            return instructor.Id;
        }

        public async Task<Instructor.Api.Models.Instructor> GetInstructorById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IReadOnlyList<Instructor.Api.Models.Instructor>> GetInstructors()
        {
            return await _repository.Get();
        }
    }
}
