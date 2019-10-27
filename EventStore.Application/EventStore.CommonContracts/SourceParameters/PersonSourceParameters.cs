using System.Collections.Generic;

namespace EventStore.CommonContracts.SourceParameters
{
    /// <summary>
    /// Provides functionality for paging
    /// </summary>
    public class PersonSourceParameters
    {
        // TODO inherit the class from SourceParameters and remove the PersonIds property
        public List<int> PersonIds { get; set; } = new List<int>();

        //search params
        public string FirstName { get; set; } = null;

        public string LastName { get; set; } = null;

        public string CompanyName { get; set; } = null;
    }
}
