using Microsoft.EntityFrameworkCore;
using ConsumerApp.Models;
using kafka_test.Models;

namespace ConsumerApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<CarDetail> CarDetails { get; set; }

    }
}
