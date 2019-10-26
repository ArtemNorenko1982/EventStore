using AutoMapper;
using EventStore.Data;
using EventStore.Data.Entities;
using EventStore.DataContracts.DTO;

namespace EventStore.Services.AutoMapper
{
    public class LibraryAutoMapperProfile : Profile
    {
        public LibraryAutoMapperProfile()
        {
            CreateMap<PersonEntity, PersonModel>()
                .ReverseMap();

            CreateMap<EventEntity, EventModel>()
                .ReverseMap();
        }
    }
}
