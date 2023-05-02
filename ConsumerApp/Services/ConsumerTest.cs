using Confluent.Kafka;
using ConsumerApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ConsumerApp.Services
{
    public class ConsumerTest : BackgroundService
    {
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
                    Debug.WriteLine(bookingDetails.Message.Value);
                }

            }
        }

    }
}
