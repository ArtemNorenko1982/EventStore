using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EventStore.CommonContracts;
using EventStore.Services.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace EventStore.Api.ContainerSettings.Registrations
{
    /// <summary>
    /// Add auto mapper config
    /// </summary>
    internal class AutoMapperRegistration : IRegistrationModule
    {
        public void Register(IServiceCollection services)
        {
            var typesWithMappings = new List<Type>()
            {
                typeof(LibraryAutoMapperProfile)
            };

            var assembliesWithMappings = typesWithMappings.Select(type => type.Assembly);
            services.AddAutoMapper(assembliesWithMappings);
        }
    }
}
