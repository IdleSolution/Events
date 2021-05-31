using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Application.Atendees;
using System.Threading;
using Application.Errors;
using Domain;

namespace Tests.Application.Atendees
{
    public class CreateTest : ApplicationTestBase
    {
        [Fact]
        public async Task Handle_ShouldPersistCorrectData()
        {
            var activityId = Guid.Parse("ab2bad5c-44cf-4ed2-b240-845784ee6f60");
            var command = new Create.Command 
            {
                ActivityId = activityId,
                AtendeeEmail = "test2@gmail.com",
                AtendeeName = "Test2"
            };

            var handler = new Create.Handler(dbContext);

            await handler.Handle(command, CancellationToken.None);

            var entity = dbContext.ActivityAtendees.Last();
            var userEntity = dbContext.Atendees.Last();

            Assert.NotNull(entity);
            Assert.Equal(entity.ActivityId, command.ActivityId);
            Assert.Equal(entity.AtendeeEmail, command.AtendeeEmail);

            Assert.NotNull(userEntity);
            Assert.Equal(userEntity.Email, command.AtendeeEmail);
            Assert.Equal(userEntity.Name, command.AtendeeName);
        }

        [Fact]
        public async Task Handle_ShouldReturnErrorOnActivityNotFound()
        {
            // this id doesnt exist on seeded data
            var activityId = Guid.Parse("cb2bad5c-44cf-4ed2-b240-845784ee6f60");
            var command = new Create.Command
            {
                ActivityId = activityId,
                AtendeeEmail = "test3@gmail.com",
                AtendeeName = "Test3"
            };

            var handler = new Create.Handler(dbContext);

            await Assert.ThrowsAsync<RestException>(() =>
                handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldReturnErrorOnDuplicateEmail()
        {
            // this id contains an user from seeded data
            var activityId = Guid.Parse("e0a2c36b-c679-4fbd-b966-574e11681c7c");
            var command = new Create.Command
            {
                ActivityId = activityId,
                AtendeeEmail = "test@gmail.com",
                AtendeeName = "Test"
            };

            var handler = new Create.Handler(dbContext);

            await Assert.ThrowsAsync<RestException>(() =>
                handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldReturnErrorOnFullActivity()
        {
            var activityId = Guid.Parse("cb2bad5c-44cf-4ed2-b240-845784ee6f60");

            // Adding 25 atendees so that the activity will get full
            for (int i = 0; i < 25; i++)
            {
                var atendee = new Atendee
                {
                    Email = $"test{i}@gmail.com",
                    Name = $"Tester{i}"
                };

                dbContext.Atendees.Add(atendee);

                var activityAtendee = new ActivityAtendee
                {
                    AtendeeEmail = $"test{i}@gmail.com",
                    ActivityId = activityId
                };

                dbContext.ActivityAtendees.Add(activityAtendee);
            }
            var command = new Create.Command
            {
                ActivityId = activityId,
                AtendeeEmail = "test26@gmail.com",
                AtendeeName = "Test26"
            };

            var handler = new Create.Handler(dbContext);

            await Assert.ThrowsAsync<RestException>(() =>
                handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldReturnErrorOnEmailWithDifferentName()
        {
            var activityId = Guid.Parse("ab2bad5c-44cf-4ed2-b240-845784ee6f60");
            var email = "test@gmail.com"; // this email was added to the other activity during seeding
            var name = "Tester1"; // different name

            var command = new Create.Command
            {
                ActivityId = activityId,
                AtendeeEmail = email,
                AtendeeName = name
            };

            var handler = new Create.Handler(dbContext);

            await Assert.ThrowsAsync<RestException>(() =>
                handler.Handle(command, CancellationToken.None));
        }
    }
}
