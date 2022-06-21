using EOsk.Infrastructure.Models.Dtos.Instructors;
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
            var uri = new Uri("rabbitmq://localhost/create_instructor");
            var endpoint = await _bus.GetSendEndpoint(uri);
            await endpoint.Send(newInstructor);

            return Accepted("Instruktor utworzony pomyślnie!");
        }
    }
}
