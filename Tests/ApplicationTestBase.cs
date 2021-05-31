using Persistence;

namespace Tests
{
    public class ApplicationTestBase
    {
        protected readonly DataContext dbContext;

        public ApplicationTestBase()
        {
            dbContext = DataContextFactory.Create();
        }
    }
}