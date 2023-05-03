using Confluent.Kafka;
using ConsumerApp.Models;
using ConsumerApp.Services;
using kafka_test.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsumerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ConsumersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.CarDetails.ToListAsync();
            if(result.Count == 0)
                return NotFound("There is no message to consume");
            return Ok(result);

        }
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _context.CarDetails.FirstOrDefaultAsync(x => x.Id == id);
            if(result == null)
                return NotFound($"There is no message With Id {id}");

            return Ok(result);
        }
    }
}
