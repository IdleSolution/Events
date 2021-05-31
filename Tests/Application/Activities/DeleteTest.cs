using Application.Errors;
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
    public class DeleteTest : ApplicationTestBase
    {
        [Fact]
        public async Task Handle_ShouldRemovePersistedActivity()
        {
            // activity that was added during seeding
            var id = Guid.Parse("ab2bad5c-44cf-4ed2-b240-845784ee6f60");
            var command = new Delete.Command
            {
                Id = id
            };

            var handler = new Delete.Handler(dbContext);

            await handler.Handle(command, CancellationToken.None);

            var entity = dbContext.Activities.Find(id);

            Assert.Null(entity);
        }

        [Fact]
        public async Task Handle_ShouldReturnErrorOnWrongIds()
        {
            // this id doesnt exists in our data
            var id = Guid.Parse("cb2bad5c-44cf-4ed2-b240-845784ee6f60");
            var command = new Delete.Command
            {
                Id = id
            };

            var handler = new Delete.Handler(dbContext);

            await Assert.ThrowsAsync<RestException>(() => 
                handler.Handle(command, CancellationToken.None));
        }
    }
}
