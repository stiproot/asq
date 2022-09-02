using System;
using  Confluent.Kafka;
using DTO.Events;

namespace KafkaClient.Factory
{
    public interface IConsumerFactory
    {
        IConsumer<Null, string> CreateConsumer();
    }
}