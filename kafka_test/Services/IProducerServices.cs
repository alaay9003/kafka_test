using kafka_test.Models;
using Microsoft.AspNetCore.Mvc;

namespace kafka_test.Services
{
    public interface IProducerServices
    {
        Task WriteMessage(string message);
       // Task DeleteAsync(string message);
    }
}
