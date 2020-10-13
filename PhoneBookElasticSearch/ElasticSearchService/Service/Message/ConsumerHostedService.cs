using Confluent.Kafka;
using ElasticSearchService.Service.Message.PhoneBookReadService.Service.Message;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElasticSearchService.Service.Message
{
    public class CosumerHostedService : IHostedService
    {
        private IConsumer<Null, string> consumer;
        IElasticClient client;
        private readonly ILogger<CosumerHostedService> _logger;

        public CosumerHostedService(IElasticClient client, ILogger<CosumerHostedService> logger)
        {
            _logger = logger;
            this.client = client;

            var consumerConfig = new ConsumerConfig
            {
                GroupId = "phonebook-read",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Latest,
                EnableAutoCommit = true
            };

            consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                consumer.Subscribe("phonebook-incoming");
                var consumeResult = consumer.Consume();

                new MessageConsumer(client).HandlePhonebookMessage(consumeResult.Message.Value);
                await Task.Delay(1000, cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            consumer?.Dispose();
            return Task.CompletedTask;
        }
    }
}
