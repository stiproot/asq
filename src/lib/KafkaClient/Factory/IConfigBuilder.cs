using System;
using Confluent.Kafka;

namespace KafkaClient.Factory
{
    public interface IConfigBuilder
    {
        ConsumerConfig Build();
        IConfigBuilder SetBootstrapServers(string bootstrapServers);
        IConfigBuilder SetGroupId(string groupId);
        IConfigBuilder SetAutoOffsetReset(AutoOffsetReset autoOffsetReset);
        IConfigBuilder SetSessionTimeoutMs(int sessionTimeoutMs);
        IConfigBuilder SetStatisticsIntervalMs(int statisticsIntervalMs);
        IConfigBuilder SetEnablePartitionEof(bool enablePartitionEof);
        IConfigBuilder SetEnableAutoCommit(bool enableAutoCommit);
    }
}