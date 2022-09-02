using Confluent.Kafka;
using System.Text.Json;
using System.Threading.Tasks;
using KafkaClient.Factory;
using System;

namespace KafkaClient
{
    public class KafkaClient: IKafkaClient
    {
        private readonly IProducerFactory _producerFactory;
        private readonly IConsumerFactory _consumerFactory;

        public KafkaClient(IProducerFactory producerFactory, IConsumerFactory consumerFactory) 
            => (this._producerFactory, this._consumerFactory) = (producerFactory, consumerFactory);

        public async Task WriteToTopic(string topic, object data)
        {
            try
            {
                using(var producer = _producerFactory.CreateProducer())
                {
                    var deliveryReport = await producer.ProduceAsync(topic: topic, 
                        message: new Message<Null, string>{ Value = JsonSerializer.Serialize(data) });

                    Console.WriteLine($"delivered to: {deliveryReport.TopicPartitionOffset}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("ex --> " + ex);
                throw;
            }
        }

        public async Task<string> ReadRawFromTopic(string topic)
        {
            try
            {
                if(topic == null) throw new ArgumentException("Invalid topic name provided");

                using(var consumer = this._consumerFactory.CreateConsumer())
                {
                    consumer.Subscribe(topic);

                    ConsumeResult<Null, string> result = consumer.Consume();
                    var message = result?.Message?.Value;
                    if(message == null)
                    {
                        result = consumer.Consume();
                        message = result?.Message?.Value;

                        if(message == null)
                            throw new Exception($"Consumtion result topic {topic} is null");
                    }

                    return await Task.FromResult<string>(message);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("ex --> " + ex);
                throw;
            }
        }

        public async Task<T> ReadFromTopic<T>(string topic)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(await this.ReadRawFromTopic(topic));
            }
            catch(Exception ex)
            {
                Console.WriteLine("ex --> " + ex);
                throw;
            }
        }
    }
}