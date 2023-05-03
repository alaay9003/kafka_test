using Microsoft.AspNetCore.Http;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using kafka_test.Models;
using kafka_test.Services;

namespace kafka_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IProducerServices _producer;
        public CarsController(IProducerServices producer)
        {
            _producer = producer;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CarDetails car)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            CarDto carDto = new CarDto();
            carDto.CarName = car.CarName;
            carDto.BookingStatus = car.BookingStatus;
            carDto.Method = "Post";
            string serializedData = JsonConvert.SerializeObject(carDto);
            await _producer.WriteMessage(serializedData);
            return Ok(serializedData);
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            DeleteDto deleteDto = new DeleteDto();
            deleteDto.CarId = id;
            deleteDto.Method = "Delete";
            string serializedData = JsonConvert.SerializeObject(deleteDto);
            await _producer.WriteMessage(serializedData);
            return Ok(serializedData);

        }
        [HttpPut]
        public async Task<ActionResult> Put(int id,[FromBody] CarDetails car)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            updateDto carDto = new updateDto();
            carDto.CarId = id; 
            carDto.CarName = car.CarName;
            carDto.BookingStatus = car.BookingStatus;
            carDto.Method = "Put";
            string serializedData = JsonConvert.SerializeObject(carDto);
            await _producer.WriteMessage(serializedData);
            return Ok(serializedData);
        }


    }
}