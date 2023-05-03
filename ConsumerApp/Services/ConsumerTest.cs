using Confluent.Kafka;
using ConsumerApp.Controllers;
using ConsumerApp.Models;
using kafka_test.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace ConsumerApp.Services
{
    public class ConsumerTest : IHostedService
    {
        private readonly IConsumerService _consumer;
        public ConsumerTest(IConsumerService consumer)
        {
            _consumer = consumer;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
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
                            switch (order.Method)
                                {
                                    case "Post":
                                        await _consumer.postMessage(order);
                                        break;
                                    default:
                                        break;
                                }
                            
                            //Debug.WriteLine(bookingDetails.Message.Value);
                        }
                    }
                    }
                    catch (OperationCanceledException)
                    {
                        // Ignore cancellation exceptions.
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error consuming message: {ex.Message}");
                    }
                }
            }, cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}



/*private readonly string topic = "test";
private readonly string groupId = "test_group";
private readonly string bootstrapServers = "localhost:9092";*/





/* protected override Task ExecuteAsync(CancellationToken stoppingToken)
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
             switch(order.Method)
             {
                case "Post" :
                     Debug.WriteLine(order.CarName);
                break;
             }
         }

     }
 }*/