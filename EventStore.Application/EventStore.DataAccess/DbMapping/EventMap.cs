using EventStore.Data;
using EventStore.Data.Entities;
using EventStore.DataAccess.EF;
using Microsoft.EntityFrameworkCore;

namespace EventStore.DataAccess.DbMapping
{
    public class EventMap : IDbEntityMapper
    {
        public void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventEntity>(entity =>
            {
                entity.HasOne<PersonEntity>(x => x.Person)
                    .WithMany(x => x.Events);
            });
        }
    }
}
