using System;
using System.Collections.Generic;
using EventStore.DataContracts.Interfaces;

namespace EventStore.DataContracts.DTO
{
    public class EventModel : IBaseModel
    {
        public EventModel()
        {
            this.Meta = new Dictionary<string, string>();
        }
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Producer { get; set; }
        public string Source { get; set; }
        public string ActionType { get; set; }
        public string Content { get; set; }
        public DateTime EventDate { get; set; }
        public IDictionary<string, string> Meta { get; set; }
    }
}
