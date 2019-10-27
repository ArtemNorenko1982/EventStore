using EventStore.Data;
using EventStore.DataAccess.EF;
using Microsoft.EntityFrameworkCore;

namespace EventStore.DataAccess.DbMapping
{
    public class PersonMap : IDbEntityMapper
    {
        public void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonEntity>(entity =>
            {
                entity.HasMany(x => x.Events)
                    .WithOne(x => x.Person);
            });
        }
    }
}
