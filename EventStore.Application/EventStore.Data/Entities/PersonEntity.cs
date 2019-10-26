using System.Collections.Generic;
using EventStore.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using EventStore.Data.Entities;

namespace EventStore.Data
{
    public class PersonEntity : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }

        public Dictionary<string, string> UserAccounts { get; set; }
        public IEnumerable<EventEntity> Events { get; set; }
    }
}
