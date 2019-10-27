﻿using System;
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
        private const string GroupId = "evntListener";
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
                GroupId = GroupId,
                EnableAutoOffsetStore = true,
                BootstrapServers = BootstrapServer,
                EnableAutoCommit = true,
                EnablePartitionEof = true
            };
        }

        public bool PostMessage(PersonModel model)
        {
            var result = false;
            Action<DeliveryReport<Null, string>> handler =
                r => Console.WriteLine($"{(!r.Error.IsError ? $"Delivered message to {r.TopicPartitionOffset}" : $"Delivery error {r.Error.Reason}")}");

            var jsonModel = JsonConvert.SerializeObject(model);
            //var jsonModel = JObject.FromObject(model);

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
                try
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
                        var r = consumer.Consume(cts.Token);
                        var val = $"{r.Value} at {r.TopicPartitionOffset}";
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