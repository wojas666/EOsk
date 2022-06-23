using EOsk.Infrastructure.Events.Instructors.Requestes.Commands;
using EOsk.Infrastructure.Events.Instructors.Requestes.Queries;
using EOsk.Infrastructure.Models.Dtos.Instructors;
using EOsk.Infrastructure.Models.Dtos.Instructors.Validators;
using EOsk.Infrastructure.Responses;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace EOsk.ApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructorController : Controller
    {
        private readonly IBusControl _bus;
        private readonly IConfiguration _configuration;
        private readonly IRequestClient<GetInstructorByIdRequest> _getInstructorByIdRequest;
        private readonly IRequestClient<GetInstructorListRequest> _getInstructorListRequest;

        public InstructorController(IBusControl busControl, IConfiguration configuration,
            IRequestClient<GetInstructorByIdRequest> getInstructorByIdRequest,
            IRequestClient<GetInstructorListRequest> getInstructorListRequest)
        {
            _bus = busControl;
            _getInstructorByIdRequest = getInstructorByIdRequest;
            _getInstructorListRequest = getInstructorListRequest;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstructorById(Guid id)
        {
            var getInstructorByIdRequest = new GetInstructorByIdRequest() { Id = id };
            var instructor = await _getInstructorByIdRequest.GetResponse<InstructorDto>(getInstructorByIdRequest);

            return Accepted(instructor);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var instructors = await _getInstructorListRequest.GetResponse<InstructorDto[]>(new GetInstructorListRequest());

            return Accepted(instructors);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInstructor([FromBody]CreateInstructorDto newInstructor)
        {
            var createInstructorResponse = new CreateObjectResponse();

            var validator = new CreateInstructorDtoValidator();
            var validationResult = await validator.ValidateAsync(newInstructor);

            if (!validationResult.IsValid)
            {
                createInstructorResponse.IsSucces = false;
                createInstructorResponse.Message = "Utworzenie nowego instruktora nie powiodło się!";
                createInstructorResponse.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return BadRequest(createInstructorResponse);
            }

            CreateInstructorCommand command = new CreateInstructorCommand() { InstructorToCreated = newInstructor };

            var uri = new Uri("rabbitmq://localhost/create_instructor");
            var endpoint = await _bus.GetSendEndpoint(uri);
            await endpoint.Send(command);

            createInstructorResponse.Message = "Instruktor utworzony pomyślnie!";

            return Accepted(createInstructorResponse);
        }
    }
}
