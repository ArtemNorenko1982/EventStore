using Microsoft.EntityFrameworkCore;

namespace EventStore.DataAccess.EF
{
    public interface IDbEntityMapper
    {
        void Map(ModelBuilder modelBuilder);
    }
}
