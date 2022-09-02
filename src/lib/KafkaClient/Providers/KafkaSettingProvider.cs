
using Microsoft.Extensions.Configuration;

namespace KafkaClient.Providers
{
    public class KafkaSettingProvider : IKafkaSettingProvider
    {
        private readonly IConfiguration _config;

        public KafkaSettingProvider(IConfiguration config)
        {
            this._config = config;
        }

        public string GetMailTopicName()
        {
            return _config["Kafka:Topics:Mail"];
        }
        public string GetMailTestTopicName()
        {
            return _config["Kafka:Topics:TestMail"];
        }
    }
}