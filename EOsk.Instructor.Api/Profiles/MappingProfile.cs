using AutoMapper;
using EOsk.Infrastructure.Models.Dtos.Instructors;

namespace EOsk.Instructor.Api.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EOsk.Instructor.Api.Models.Instructor, InstructorDto>().ReverseMap();
            CreateMap<EOsk.Instructor.Api.Models.Instructor, CreateInstructorDto>().ReverseMap();
            CreateMap<EOsk.Instructor.Api.Models.Instructor, UpdateInstructorDto>().ReverseMap();
        }
    }
}
