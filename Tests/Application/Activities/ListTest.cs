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
    public class ListTest : ApplicationTestBase
    {
        [Fact]
        public async Task Handle_ShouldReturnCorrectNumberOfActivities()
        {

            var query = new List.Query{};

            var handler = new List.Handler(dbContext);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(2, result.Count);

        }
    }
}
