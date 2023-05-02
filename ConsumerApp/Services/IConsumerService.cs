
using kafka_test.Models;

namespace ConsumerApp.Services
{
    public interface IConsumerService
    {
        Task carDetails(CarDto car);  
    }
}
