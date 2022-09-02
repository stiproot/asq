using System;
using  Confluent.Kafka;
using DTO.Events;

namespace KafkaClient.Factory
{
    public interface IProducerFactory
    {
        IProducer<Null, string> CreateProducer();
    }
}