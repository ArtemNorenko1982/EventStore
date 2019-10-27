using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AutoMapper;
using Confluent.Kafka;
using EventStore.Data.Entities;
using EventStore.DataContracts;
using EventStore.DataContracts.DTO;
using EventStore.Services.Contractors.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EventStore.Services.Services
{
    public class DataMinerService : IDataMinerService
    {
        private readonly IEventStoreRepository<EventModel, EventEntity> repository;
        private readonly IMapper mapper;
        private const string PostQueue = "persons-v1";
        private const string ReadQueue = "events-v1";
        private const string BootstrapServer = "172.26.3.99:9092";

        private readonly ProducerConfig _producerConfig;
        private readonly ConsumerConfig _consumerConfig;
        //private readonly Dictionary<string, string> _consumerConfig;

        public DataMinerService(IEventStoreRepository<EventModel, EventEntity> repository)
        {
            this.repository = repository;
            
            _producerConfig = new ProducerConfig
            {
                BootstrapServers = BootstrapServer
            };

            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = BootstrapServer,
                GroupId = ReadQueue,
                EnableAutoCommit = false
            };
        }

        public bool PostMessage(PersonModel model)
        {
            var result = false;
            Action<DeliveryReport<Null, string>> handler =
                r => Console.WriteLine($"{(!r.Error.IsError ? $"Delivered message to {r.TopicPartitionOffset}" : $"Delivery error {r.Error.Reason}")}");

            var jsonModel = JsonConvert.SerializeObject(model);
            //var jsonModel = JObject.FromObject(model);

            using (var producer = new ProducerBuilder<Null, string>(_consumerConfig).Build())
            {
                var message = new Message<Null, string> { Value = jsonModel };
                try
                {
                    producer.Produce(PostQueue, message);
                    producer.Flush(TimeSpan.FromSeconds(10));
                    result = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return result;
        }

        public bool ConsumeMessage()
        {
            var result = false;
            using (var consumer = new ConsumerBuilder<Null, string>(_consumerConfig).Build())
            {
                try
                {
                    consumer.Subscribe(ReadQueue);
                    try
                    {
                        var r = consumer.Consume();
                        
                        result = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        
                    }
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                
            }

            return result;
        }

        #region PrivateMethods

        private bool IsConnected()
        {

            return true;
        }

        #endregion
    }
}
