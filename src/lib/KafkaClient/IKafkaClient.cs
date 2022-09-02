using System.Collections.Generic;
using System.Threading.Tasks;

namespace KafkaClient
{
    public interface IKafkaClient
    {
        Task WriteToTopic(string topic, object data);
        Task<string> ReadRawFromTopic(string topic);
        Task<T> ReadFromTopic<T>(string topic);
    }
}