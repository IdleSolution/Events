using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Application.Activities;

namespace Tests.Application.Activities
{
    public class CreateTest : ApplicationTestBase
    {
        [Fact]
        public async Task Handle_ShouldPersistAddedData()
        {
            var command = new Create.Command
            {
                Title = "Test event",
                Description = "This event is very nice",
                Category = "Test",
                Date = DateTime.Now,
                City = "Krakow",
                Venue = "Park"
            };

            var handler = new Create.Handler(dbContext);

            await handler.Handle(command, CancellationToken.None);

            var entity = dbContext.Activities.Last();

            Assert.NotNull(entity);
            Assert.Equal(command.Title, entity.Title);
            Assert.Equal(command.Description, entity.Description);
            Assert.Equal(command.Category, entity.Category);
            Assert.Equal(command.Date, entity.Date);
            Assert.Equal(command.City, entity.City);
            Assert.Equal(command.Venue, entity.Venue);

        }
    }
}
