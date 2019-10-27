using EventStore.Data;
using EventStore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventStore.DataAccess.EF
{
    public class EventStoreContext : DbContext
    {
        public EventStoreContext(DbContextOptions<EventStoreContext> options)
            : base(options)
        {
        }

        public DbSet<PersonEntity> Persons { get; set; }
        public DbSet<EventEntity> Events { get; set; }
    }
}
