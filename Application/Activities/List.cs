using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<List<ActivityList>> { }

        public class Handler : IRequestHandler<Query, List<ActivityList>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<ActivityList>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activities = await _context.Activities
                            .Include(x => x.ActivityAtendees)
                            .ThenInclude(x => x.Atendee)
                            .Select(x => new ActivityList
                            {
                                Id = x.Id,
                                Title = x.Title,
                                Description = x.Description,
                                Category = x.Category,
                                Date = x.Date,
                                Venue = x.Venue,
                                City = x.City,
                                Atendees = (ICollection<Atendee>)x.ActivityAtendees.Select(x => x.Atendee)

                            })
                            .ToListAsync();

                return activities;
            }
        }
    }
}