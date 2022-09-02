using System;
using Confluent.Kafka;

namespace KafkaClient.Factory
{
    public interface IConfigProvider
    {
       ConsumerConfig BuildConfig();
    }
}