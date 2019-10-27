using System;
using System.Collections.Generic;
using EventStore.Data.Interfaces;

namespace EventStore.Data.Entities
{
    public class EventEntity : IBaseEntity
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Producer { get; set; }
        public string Source { get; set; }
        public string ActionType { get; set; }
        public string Content { get; set; }
        public DateTime EventDate { get; set; }
        //public IEnumerable<string, string> Meta { get; set; } = new Dictionary<string, string>();
        public PersonEntity Person { get; set; }
    }
}
