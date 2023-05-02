using Confluent.Kafka;
using ConsumerApp.Models;
using kafka_test.Models;

namespace ConsumerApp.Services
{
    public class ConsumerService : IConsumerService
    {
        private readonly ApplicationDbContext _dbContext;
        public ConsumerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task carDetails(CarDto car)
        {
            var carObject=new CarDetail
                {
                CarName=car.CarName,
                BookingStatus=car.BookingStatus,
                };
            await _dbContext.CarDetails.AddAsync(carObject);
            _dbContext.SaveChanges();
        }
    }
}
