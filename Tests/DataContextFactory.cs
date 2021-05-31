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

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            SeedSampleData(dbContext);

            return dbContext;
        }

        public static void SeedSampleData(DataContext dbContext)
        {
            
            var id = Guid.Parse("e0a2c36b-c679-4fbd-b966-574e11681c7c");
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

            dbContext.Activities.Add(new Activity
            {
                Id = Guid.Parse("ab2bad5c-44cf-4ed2-b240-845784ee6f60"),
                Title = "Event2",
                Description = "Nice event",
                Category = "Test",
                Date = DateTime.Now,
                Venue = "Park",
                City = "Krakow"
            });

            dbContext.SaveChanges();
        }
    }
}