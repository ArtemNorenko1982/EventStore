using System;
using System.Collections.Generic;
using EventStore.DataContracts.Interfaces;

namespace EventStore.DataContracts.DTO
{
    public class PersonModel : IBaseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }

        public string TwitterId { get; set; }
        public string CrunchId { get; set; }
        public string FacebookId { get; set; }

        public DateTime StartFrom { get; set; }
        //public IDictionary<string, string> UserAccounts { get; set; } = new Dictionary<string, string>();
    }
}
