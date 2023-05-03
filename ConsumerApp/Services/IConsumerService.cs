
using kafka_test.Models;

namespace ConsumerApp.Services
{
    public interface IConsumerService
    {
        Task postMessage(CarDto car);
        Task deleteMessage(int carId);
        Task updateMessage(CarDto car);
    }
}
