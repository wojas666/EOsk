using AutoMapper;
using EOsk.Infrastructure.Events.Instructors.Requestes.Queries;
using EOsk.Infrastructure.Models.Dtos.Instructors;
using EOsk.Instructor.Api.Services;
using MassTransit;

namespace EOsk.Instructor.Api.Features.Handlers.Queries
{
    public class GetInstructorByIdRequestHandler : IConsumer<GetInstructorByIdRequest>
    {
        private readonly IInstructorService _service;
        private readonly IMapper _mapper;

        public GetInstructorByIdRequestHandler(IInstructorService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<GetInstructorByIdRequest> context)
        {
            var instructor = await _service.GetInstructorById(context.Message.Id);

            await context.RespondAsync(_mapper.Map<InstructorDto>(instructor));
        }
    }
}
