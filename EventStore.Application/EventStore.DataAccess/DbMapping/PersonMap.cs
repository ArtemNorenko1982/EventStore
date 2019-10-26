using System;
using System.Collections.Generic;
using System.Text;
using EventStore.Data;
using EventStore.Data.Entities;
using EventStore.DataAccess.EF;
using Microsoft.EntityFrameworkCore;

namespace EventStore.DataAccess.DbMapping
{
    class PersonMap : IDbEntityMapper
    {
        public void Map(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<PersonEntity>(entity =>
            //{
            //    entity.HasMany(x => x.Events)
            //        .WithOne(x => x.PersonId);
            //});
        }
    }
}
