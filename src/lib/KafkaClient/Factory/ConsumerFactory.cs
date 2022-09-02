using System;
using System.Collections.Generic;
using System.Text.Json;
//using System.Text.Json;
//using System.ComponentModel.DataAnnotations;
using Confluent.Kafka;
//using Confluent.Kafka.SyncOverAsync;
//using Confluent.SchemaRegistry;
//using Confluent.SchemaRegistry.Serdes;
//using DTO.Events;
//using Newtonsoft.Json;

//using System.Text.Json;

namespace KafkaClient.Factory
{
    public class ConsumerFactory: IConsumerFactory
    {
        private IConfigProvider _configProvider;

        public ConsumerFactory(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public IConsumer<Null, string> CreateConsumer()
        {
            try
            {
                //throw new Exception("consumer config -> " + JsonSerializer.Serialize(_configProvider.BuildConfig()));
                return new ConsumerBuilder<Null, string>(_configProvider.BuildConfig())
                    //.SetValueDeserializer(new JsonDeserializer<AccountCreationEvent>().AsSyncOverAsync())
                    .SetErrorHandler((_, e) => {
                        Console.WriteLine(e);
                    })
                    .SetStatisticsHandler((_, json) => {
                        Console.WriteLine(json);
                    })
                    .SetPartitionsAssignedHandler((_config, partitions) => {
                        Console.WriteLine(partitions);
                    })
                    .SetPartitionsRevokedHandler((c, partitions) => {
                        // called on consumer group rebalance immediately before 
                        // the consumer starts reading from the partitions. You
                        // can override the start offsets, and even the set of
                        // partitions to consume from by (optionally) returning
                        // a list from this method.
                    })
                    .Build();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}