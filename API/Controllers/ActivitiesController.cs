using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Activities;
using System.Collections.Generic;
using Domain;
using MediatR;

namespace API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActivitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActivityList>>> List()
        {
            return await _mediator.Send(new List.Query { });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete]
        public async Task<ActionResult<Unit>> Delete(Delete.Command command)
        {
            return await _mediator.Send(command);
        }

    }
}