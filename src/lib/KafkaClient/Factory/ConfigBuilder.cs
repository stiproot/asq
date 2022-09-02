using System;
using Confluent.Kafka;

namespace KafkaClient.Factory
{
    public class ConfigBuilder: IConfigBuilder
    {
        private ConsumerConfig _config;

        public ConfigBuilder(string bootstrapServers, 
                             string groupId, 
                             AutoOffsetReset autoOffsetReset, 
                             int sessionTimeoutMs,
                             int statisticsIntervalMs,
                             bool enablePartitionEof,
                             bool enableAutoCommit)
                             {
                                 _config = new ConsumerConfig
                                 {
                                     BootstrapServers = bootstrapServers,
                                     GroupId = groupId,
                                     AutoOffsetReset = autoOffsetReset,
                                     SessionTimeoutMs = sessionTimeoutMs,
                                     StatisticsIntervalMs = statisticsIntervalMs,
                                     EnablePartitionEof = enablePartitionEof,
                                     EnableAutoCommit = enableAutoCommit
                                 };
                             }


        public ConsumerConfig Build()
        {
            return _config;
        }

        public IConfigBuilder SetBootstrapServers(string bootstrapServers)
        {
            this._config.BootstrapServers = bootstrapServers;
            return this;
        }

        public IConfigBuilder SetGroupId(string groupId)
        {
            this._config.GroupId  = groupId;
            return this;
        }

        public IConfigBuilder SetAutoOffsetReset(AutoOffsetReset autoOffsetReset)
        {
            this._config.AutoOffsetReset = autoOffsetReset;
            return this;
        }

        public IConfigBuilder SetSessionTimeoutMs(int sessionTimeoutMs)
        {
            this._config.SessionTimeoutMs = sessionTimeoutMs;
            return this;
        }

        public IConfigBuilder SetStatisticsIntervalMs(int statisticsIntervalMs)
        {
            this._config.StatisticsIntervalMs = statisticsIntervalMs;
            return this;
        }

        public IConfigBuilder SetEnablePartitionEof(bool enablePartitionEof)
        {
            this._config.EnablePartitionEof = enablePartitionEof;
            return this;
        }

        public IConfigBuilder SetEnableAutoCommit(bool enableAutoCommit)
        {
            this._config.EnableAutoCommit = enableAutoCommit;
            return this;
        }
    }
}