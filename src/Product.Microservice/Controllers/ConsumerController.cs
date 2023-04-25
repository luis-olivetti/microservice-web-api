using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SharedModel;

namespace Product.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IConfiguration _configuration;

        public ConsumerController(IBus bus, IConfiguration configuration)
        {
            _bus = bus;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> CreateConsumerExample(Message customer)
        {
            if (customer != null)
            {
                Uri uri = new Uri(_configuration.GetValue<string>("RabbitMQ:host") + "/fila");
                var endpoint = await _bus.GetSendEndpoint(uri);
                await endpoint.Send(customer);
                return Ok();
            }
            return BadRequest();
        }
    }
}