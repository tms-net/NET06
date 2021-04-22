using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.NET06.BookingSystem;

namespace TMS.NET06.BookingService.Spa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly ILogger<ServicesController> _logger;
        private readonly IBookingRepository _bookingRepository;

        public ServicesController(
            ILogger<ServicesController> logger,
            IBookingRepository bookingRepository)
        {
            _logger = logger;
            _bookingRepository = bookingRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Service>> GetAsync()
        {
            return await _bookingRepository.GetServicesAsync();
        }

        [HttpPost]
        [Route("[action]")]
        public Task<IEnumerable<DateTime>> AvailableDatesAsync(int serviceId)
        {
            return Task.FromResult(new[] { DateTime.Now.Date }.AsEnumerable());
        }
    }
}
