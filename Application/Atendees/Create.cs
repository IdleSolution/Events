using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Atendees
{
    public class Create
    {
        public class Command : IRequest
        {
            public Guid ActivityId { get; set; }
            public string AtendeeEmail { get; set; }
            public string AtendeeName { get; set; }

        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.ActivityId).NotEmpty();
                RuleFor(x => x.AtendeeEmail).NotEmpty();
                RuleFor(x => x.AtendeeName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.ActivityId);

                if (activity == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Activity = "The activity you are trying to delete was not found!" });
                }

                var count = _context.ActivityAtendees.Where(x => x.ActivityId == request.ActivityId).ToArray().Count();

                if (count == 25)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { Activity = "The activity is full already!" });
                }

                var atendee = new Atendee
                {
                    Email = request.AtendeeEmail,
                    Name = request.AtendeeName
                };

                var user = _context.Atendees.FindAsync(request.AtendeeEmail);

                if (user == null) _context.Atendees.Add(atendee);

                var activityAtendee = new ActivityAtendee
                {
                    Atendee = atendee,
                    AtendeeEmail = request.AtendeeEmail,
                    Activity = activity,
                    ActivityId = request.ActivityId
                };

                _context.ActivityAtendees.Add(activityAtendee);


                bool success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}