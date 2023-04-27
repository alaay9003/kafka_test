using Confluent.Kafka;

namespace ConsumerApp.Services
{
    public class ConsumerService : IConsumerService
    {
        /*
                private ConsumerConfig _configuration;
                private readonly IConfiguration _config;*/
        public ConsumerService()
        {
            /*            _configuration = configuration;
                        _config = config;*/
        }

        public string GetMessage()
        {
            /*            using (var consumer = new ConsumerBuilder<Null, string>(_configuration).Build())
                        {
                            consumer.Subscribe("testdata");
                            while (true)
                            {
                                var bookingDetails = consumer.Consume();
                                Console.WriteLine(bookingDetails.Message.Value);
                            }

                        }

                        var consumeResult = this._consumer.Consume();
                        return consumeResult.Value;*/
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
                    Console.WriteLine(bookingDetails.Message.Value);
                    return bookingDetails.Message.Value;
                }
            }
        }

    }
}
