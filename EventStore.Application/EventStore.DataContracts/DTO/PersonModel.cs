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
        public Dictionary<string, string> UserAccounts { get; set; }
    }
}
