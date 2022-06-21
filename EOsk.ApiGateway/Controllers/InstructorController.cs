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

        public InstructorController(IBusControl busControl)
        {
            _bus = busControl;
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

            var uri = new Uri("rabbitmq://localhost/create_instructor");
            var endpoint = await _bus.GetSendEndpoint(uri);
            await endpoint.Send(newInstructor);

            createInstructorResponse.Message = "Instruktor utworzony pomyślnie!";

            return Accepted(createInstructorResponse);
        }
    }
}
