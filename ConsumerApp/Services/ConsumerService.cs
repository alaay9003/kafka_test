using Confluent.Kafka;
using ConsumerApp.Models;
using kafka_test.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsumerApp.Services
{
    public class ConsumerService : IConsumerService
    {
        private readonly ApplicationDbContext _dbContext;
        public ConsumerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task deleteMessage(int carId)
        {
            var result = await _dbContext.CarDetails.SingleOrDefaultAsync(x => x.Id == carId);
            if (result != null)
            {
                _dbContext.CarDetails.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task postMessage(CarDto car)
        {
            var carObject=new CarDetail
                {
                CarName=car.CarName,
                BookingStatus=car.BookingStatus,
                };
            await _dbContext.CarDetails.AddAsync(carObject);
            _dbContext.SaveChanges();
        }

        public async Task updateMessage(CarDto car)
        {
            var result = await _dbContext.CarDetails.SingleOrDefaultAsync(x => x.Id == car.CarId);
            if (result != null)
            {
                result.BookingStatus = car.BookingStatus;
                result.CarName = car.CarName;
                _dbContext.CarDetails.Update(result);
                _dbContext.SaveChanges();
            }
        }
    }
}
