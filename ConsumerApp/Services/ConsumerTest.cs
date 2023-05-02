using Confluent.Kafka;
using kafka_test.Models;
using System.Diagnostics;
using System.Text.Json;

namespace ConsumerApp.Services
{
    public class ConsumerTest : BackgroundService
    {
        private readonly IConsumerService _consumer;
        public ConsumerTest(IConsumerService consumer)
        {
            _consumer = consumer;
        }

/*        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = "gid-consumers",
                BootstrapServers = "localhost:9092"
            };

            using (var consumer = new ConsumerBuilder<Null, string>(config).Build())
            {
                consumer.Subscribe("testdata");

                while (true)
                {
                    var bookingDetails = consumer.Consume();
                    var order = JsonSerializer.Deserialize<CarDto>(bookingDetails.Message.Value);
                    await _consumer.carDetails(order);
                    Debug.WriteLine(bookingDetails.Message.Value);
                }

            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }*/
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = "gid-consumers",
                BootstrapServers = "localhost:9092"
            };

            using (var consumer = new ConsumerBuilder<Null, string>(config).Build())
            {
                consumer.Subscribe("testdata");

                while (true)
                {
                    var bookingDetails = consumer.Consume();
                    var order = JsonSerializer.Deserialize<CarDto>(bookingDetails.Message.Value);
                    _consumer.carDetails(order);
                }

            }
        }

    }
}
