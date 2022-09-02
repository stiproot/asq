namespace KafkaClient.Providers
{
    public interface IKafkaSettingProvider
    {
        string GetMailTopicName();
        string GetMailTestTopicName();
    }
}