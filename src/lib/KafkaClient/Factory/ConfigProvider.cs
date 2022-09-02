using System;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace KafkaClient.Factory
{
    public class ConfigProvider: IConfigProvider
    {
       private IConfiguration _config;

       public ConfigProvider(IConfiguration config)
       {
           _config = config;
       }

       public ConsumerConfig BuildConfig()
       {
           //Console.WriteLine(_config["Kafka:Config:Consumer:BootstrapServers"]);

            //return new ConsumerConfig
            //{
                //BootstrapServers = "localhost:9092",
                //GroupId = "console-test",
                //AutoOffsetReset = AutoOffsetReset.Earliest
            //};

           return new ConfigBuilder(
               bootstrapServers: _config["Kafka:Config:Consumer:BootstrapServers"],
               groupId: _config["Kafka:Config:Consumer:GroupId"],
               autoOffsetReset: (AutoOffsetReset)(int.Parse(_config["Kafka:Config:Consumer:AutoOffsetReset"])),
               sessionTimeoutMs: int.Parse(_config["Kafka:Config:Consumer:SessionTimeoutMs"]),
               statisticsIntervalMs: int.Parse(_config["Kafka:Config:Consumer:StatisticsIntervalMs"]),
               enablePartitionEof: bool.Parse(_config["Kafka:Config:Consumer:EnablePartitionEof"]),
               enableAutoCommit: bool.Parse(_config["Kafka:Config:Consumer:EnableAutoCommit"])
           ).Build();
       }
    }
}