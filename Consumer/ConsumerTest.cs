using System.Diagnostics;
using Confluent.Kafka;
public class ConsumerTest : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
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
                //var order = JsonSerializer.Deserialize<CarDto>(bookingDetails.Message.Value);
                //await _consumer.carDetails(order);
                Debug.WriteLine(bookingDetails.Message.Value);
            }

        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}