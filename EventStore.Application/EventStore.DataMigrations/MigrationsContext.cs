using System;
using System.IO;
using EventStore.Data;
using EventStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EventStore.DataMigrations
{
    public sealed class MigrationsContext : DbContext
    {
        public MigrationsContext(DbContextOptions options)
            : base(options)
        {
            Database.SetCommandTimeout(new TimeSpan(0, 1, 0));
        }

        public DbSet<PersonEntity> Persons { get; set; }
        public DbSet<EventEntity> Events { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(GetConnectionString()); ;
        }

        private string GetConnectionString()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            Console.WriteLine(Directory.GetCurrentDirectory());
            Console.WriteLine(environmentName);
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .Build();

            var connectionString = configuration.GetConnectionString("EventStoreDB");
            Console.WriteLine("--------");
            Console.WriteLine(connectionString);
            Console.WriteLine("--------");

            return connectionString;
        }
    }
}
