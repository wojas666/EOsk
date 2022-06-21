using EOsk.Infrastructure.Models.Dtos.Instructors;
using EOsk.Infrastructure.Repository;
using EOsk.Instructor.Api.DbContexts;
using EOsk.Instructor.Api.Repository.Contract;
using Microsoft.EntityFrameworkCore;

namespace EOsk.Instructor.Api.Repository
{
    public class InstructorRepository : GenericRepository<EOsk.Instructor.Api.Models.Instructor, ApplicationDbContext>, IInstructorRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public InstructorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Models.Instructor>> SearchInstructor(SearchInstructorDto searchInstructor)
        {
            IQueryable<Instructor.Api.Models.Instructor> query = _dbContext.Set<Instructor.Api.Models.Instructor>();

            if (!String.IsNullOrEmpty(searchInstructor.SearchPhrase))
            {
                query = _dbContext.Set<Instructor.Api.Models.Instructor>()
                    .Where(x => x.FirstName.Contains(searchInstructor.SearchPhrase) 
                    || x.LastName.Contains(searchInstructor.SearchPhrase));
            }

            if(searchInstructor.Pesel != null)
            {
                query = _dbContext.Set<Instructor.Api.Models.Instructor>()
                    .Where(x => x.Pesel.Contains(searchInstructor.Pesel));
            }

            return await query.ToListAsync();
        }
    }
}
