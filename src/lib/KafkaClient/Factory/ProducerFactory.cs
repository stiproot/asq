using System;
using System.Collections.Generic;
//using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using DTO.Events;
using Newtonsoft.Json;

//using System.Text.Json;

namespace KafkaClient.Factory
{
    public class ProducerFactory: IProducerFactory
    {
        private IConfigProvider _configProvider;

        public ProducerFactory(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public IProducer<Null, string> CreateProducer()
        {
            try
            {
                var config = new ProducerConfig
                {
                    BootstrapServers = "localhost:9092",
                };

                var jsonSerializerConfig = new JsonSerializerConfig
                {
                    BufferBytes = 100
                };

                return new ProducerBuilder<Null, string>(config)
                    //.SetValueSerializer(new JsonSerializer<AccountCreationEvent>(null, jsonSerializerConfig))
                    //.SetErrorHandler((_, e) => {
                        //Console.WriteLine(e);
                    //})
                    //.SetStatisticsHandler((_, json) => {
                        //Console.WriteLine(json);
                    //})
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
            //try
            //{
                //using(var producer = _producerFactory.CreateProducer())
                //{
                    //var deliveryReport = await producer.ProduceAsync(topic: "topic_asq_test", 
                        //message: new Message<Null, string>{Value = JsonSerializer.Serialize(TestAccountCreationEvent)});

                    //Console.WriteLine($"delivered to: {deliveryReport.TopicPartitionOffset}");
                //}
            //}
            //catch(Exception ex)
            //{
                //Console.WriteLine("ex --> " + ex);
                //throw;
            //}