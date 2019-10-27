using EventStore.DataContracts.DTO;

namespace EventStore.Services.Contractors.Interfaces
{
    public interface IDataMinerService
    {
        bool PostMessage(PersonModel model);
        bool ConsumeMessage();
    }
}
