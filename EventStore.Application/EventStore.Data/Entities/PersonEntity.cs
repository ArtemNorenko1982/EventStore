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
        public string Company { get; set; }
        public string TwitterId { get; set; }
        public string CrunchId { get; set; }
        public string FacebookId { get; set; }
        //public Dictionary<string, string> UserAccounts { get; set; } = new Dictionary<string, string>();
        public IEnumerable<EventEntity> Events { get; set; } = new List<EventEntity>();
    }
}
