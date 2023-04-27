using Confluent.Kafka;
using ConsumerApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsumerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumersController : ControllerBase
    {
        /*        private readonly IConsumerService _consumer;

                public ConsumersController(IConsumerService consumer)
                {
                    _consumer = consumer;
                }*/
        [HttpGet]
        public string getAll()
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
                    return bookingDetails.Message.Value;
                }

            }
        }
    }
}
