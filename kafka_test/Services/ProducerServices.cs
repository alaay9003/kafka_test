using Confluent.Kafka;
using Newtonsoft.Json;
using kafka_test.Models;
namespace kafka_test.Services
{
    public class ProducerServices :IProducerServices
    {
        private ProducerConfig _configuration;
        private readonly IConfiguration _config;

        public ProducerServices(ProducerConfig configuration, IConfiguration config)
        {
            _configuration = configuration;
            _config = config;
        }
        
        
        public async Task WriteMessage(string message)
        {
            var topic = _config.GetSection("TopicName").Value;

            using (var producer = new ProducerBuilder<Null, string>(_configuration).Build())
            {
                await producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
                producer.Flush(TimeSpan.FromSeconds(10));
            }
        }


    }
}
