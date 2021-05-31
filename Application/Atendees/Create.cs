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
    // Signs up user for an activity
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
                    throw new RestException(HttpStatusCode.NotFound, new { Activity = "The activity you are trying to attend to was not found!" });
                }

                if (await _context.ActivityAtendees.FindAsync(request.AtendeeEmail, request.ActivityId) != null)
                {
                    throw new RestException(HttpStatusCode.Conflict, new { Activity = "User with this email joined this activity already!" });
                }

                var count = _context.ActivityAtendees.Where(x => x.ActivityId == request.ActivityId).ToArray().Count();

                if (count == 25)
                {
                    throw new RestException(HttpStatusCode.Conflict, new { Activity = "The activity is full already!" });
                }


                var user = await _context.Atendees.FindAsync(request.AtendeeEmail);

                // Once a user signed up with his email for any event, he always has to give the same name for all events
                if (user != null && user.Name != request.AtendeeName)
                {
                    throw new RestException(HttpStatusCode.Conflict, new { Atendee = "Your email is already taken by someone with a different name!" });
                }

                // If user haven't ever signed for any activity, add him to the database
                if (user == null)
                {
                    user = new Atendee
                    {
                        Email = request.AtendeeEmail,
                        Name = request.AtendeeName
                    };

                    _context.Atendees.Add(user);
                }

                var activityAtendee = new ActivityAtendee
                {
                    Atendee = user,
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