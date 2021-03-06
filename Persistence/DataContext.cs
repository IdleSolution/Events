using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Atendee> Atendees { get; set; }
        public DbSet<ActivityAtendee> ActivityAtendees { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Atendee>(x => x.HasKey(y => y.Email));

            builder.Entity<ActivityAtendee>().HasKey(x => new { x.AtendeeEmail, x.ActivityId });
        }
    }
}