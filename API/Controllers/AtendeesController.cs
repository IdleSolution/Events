using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Atendees;

namespace API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AtendeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AtendeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await _mediator.Send(command);
        }

    }
}