using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence;
using System;

namespace Tests
{
    public static class DataContextFactory
    {
        public static DataContext Create()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var dbContext = new DataContext(options);

            dbContext.Database.EnsureCreated();

            return dbContext;
        }

        public static void SeedSampleData(DataContext dbContext)
        {
            var id = new Guid();
            var activity = new Activity
            {
                Id = id,
                Title = "Event1",
                Description = "Nice event",
                Category = "Test",
                Date = DateTime.Now,
                Venue = "Park",
                City = "Krakow"
            };
            var atendee = new Atendee
            {
                Email = "test@gmail.com",
                Name = "Tester"
            };

            dbContext.Activities.Add(activity);

            dbContext.Atendees.Add(atendee);

            dbContext.ActivityAtendees.Add(new ActivityAtendee
            {
                Atendee = atendee,
                Activity = activity
            });
        }
    }
}