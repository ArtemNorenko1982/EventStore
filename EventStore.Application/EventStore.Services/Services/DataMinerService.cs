using System;
using System.Collections.Generic;
using System.Linq;
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
using Google.Protobuf;

namespace EventStore.Services.Services
{
    public class DataMinerService : IDataMinerService
    {
        private readonly IEventStoreRepository<EventModel, EventEntity> repository;
        private readonly IMapper mapper;
        private const string PersonQueue = "persons-v1";
        private const string GroupId = "evntListener1";
        private const string EventQueue = "events-v1";
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
                GroupId = GroupId,
                EnableAutoCommit = true,
                AutoCommitIntervalMs = 2000,
                StatisticsIntervalMs = 5000,
                SessionTimeoutMs = 6000,
                //AutoOffsetReset = AutoOffsetReset.Error,
                QueuedMinMessages = 10000,
                EnablePartitionEof = true
            };
        }

        public bool PostMessage(PersonModel model)
        {
            var result = false;
            Action<DeliveryReport<Null, string>> handler =
                r => Console.WriteLine($"{(!r.Error.IsError ? $"Delivered message to {r.TopicPartitionOffset}" : $"Delivery error {r.Error.Reason}")}");

            var jsonModel = JsonConvert.SerializeObject(model);
            
            using (var producer = new ProducerBuilder<Null, string>(_producerConfig).Build())
            {
                var message = new Message<Null, string> { Value = jsonModel };
                try
                {
                    producer.Produce(PersonQueue, message);
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
            using (var consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build())
            {

                consumer.Subscribe(EventQueue);
                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true; // prevent the process from terminating.
                    cts.Cancel();
                };

                try
                {
                    var message = consumer.Consume(cts.Token);
                    do
                    {
                        
                        try
                        {
                            var model = JsonConvert.DeserializeObject<EventModel>(message.Value);
                            if (model.PersonId != 0)
                            {
                                try
                                {
                                    repository.AddAsync(model);
                                    result = true;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                    result = false;
                                }
                            }

                        }
                        catch (Exception e)
                        {
                            var re = false;
                        }
                        finally
                        {
                            message = consumer.Consume(cts.Token);
                        }
                       
                    } while (!string.IsNullOrEmpty(message.Value));

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    result = false;
                }
            }

            return result;
        }
    }
}
