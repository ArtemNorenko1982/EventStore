using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EventStore.DataMigrations
{
    public class ContextFactory : IDesignTimeDbContextFactory<MigrationsContext>
    {
        public MigrationsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MigrationsContext>();

            return new MigrationsContext(optionsBuilder.Options);
        }
    }
}
