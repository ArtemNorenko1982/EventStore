using System.Collections.Generic;

namespace EventStore.CommonContracts.SourceParameters
{
    public class EventSourceParameters
    {
        public int PageNumber { get; set; } = 1;

        public int PerPage { get; set; } = 20;

        public string KeyPhrase { get; set; }

        public List<int> PersonIds { get; set; } = new List<int>();
    }
}
