using Confluent.Kafka;
using ConsumerApp.Controllers;
using ConsumerApp.Models;
using kafka_test.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace ConsumerApp.Services
{
    public class ConsumerTest : BackgroundService
    {
        private readonly ApplicationDbContext _dbContext;
        public ConsumerTest(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /*        public Task StartAsync(CancellationToken cancellationToken)
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
                            Debug.WriteLine(order);
                        }

                    }
                }*/

        /*        public Task StopAsync(CancellationToken cancellationToken)
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
                    var order = JsonSerializer.Deserialize<CarDto>(bookingDetails.Message.Value); ;
                    Debug.WriteLine(order.CarName);
                }

            }
        }

    }
}
