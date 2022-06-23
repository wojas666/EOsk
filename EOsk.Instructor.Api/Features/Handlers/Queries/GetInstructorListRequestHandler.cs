using AutoMapper;
using EOsk.Infrastructure.Events.Instructors.Requestes.Queries;
using EOsk.Infrastructure.Models.Dtos.Instructors;
using EOsk.Instructor.Api.Services;
using MassTransit;

namespace EOsk.Instructor.Api.Features.Handlers.Queries
{
    public class GetInstructorListRequestHandler : IConsumer<GetInstructorListRequest>
    {
        private readonly IInstructorService _service;
        private readonly IMapper _mapper;

        public GetInstructorListRequestHandler(IInstructorService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<GetInstructorListRequest> context)
        {
            var instructorList = await _service.GetInstructors();

            await context.RespondAsync<InstructorDto[]>(_mapper.Map<InstructorDto[]>(instructorList));
        }
    }
}
