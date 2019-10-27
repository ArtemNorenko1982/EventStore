using System;
using EventStore.DataContracts.Interfaces;

namespace EventStore.DataContracts.DTO
{
    public class PersonModel : IBaseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }

        public string TwitterId { get; set; }
        public string CrunchId { get; set; }

        public DateTime StartFrom { get; set; }
        //public IDictionary<string, string> UserAccounts { get; set; } = new Dictionary<string, string>();
    }
}
