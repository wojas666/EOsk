using AutoMapper;
using EOsk.Infrastructure.Models.Dtos.Instructors;
using EOsk.Infrastructure.Models.Dtos.Instructors.Validators;
using EOsk.Infrastructure.Responses;
using EOsk.Instructor.Api.Repository.Contract;
using EOsk.Instructor.Api.Services;
using MassTransit;

namespace EOsk.Instructor.Api.Features.Handlers.Commands
{
    public class CreateInstructorCommandHandler : IConsumer<CreateInstructorDto>
    {
        private readonly IInstructorService _service;
        private readonly IMapper _mapper;

        public CreateInstructorCommandHandler(IInstructorService instructorService, IMapper mapper)
        {
            _service = instructorService;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<CreateInstructorDto> context)
        {
            // Map Dto object to datamodel.
            var newInstructor = _mapper.Map<EOsk.Instructor.Api.Models.Instructor>(context.Message);
            await _service.CreateInstructor(newInstructor);

            await Task.CompletedTask;
        }
    }
}
