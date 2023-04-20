using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.Microservice.Data;
using SharedModel;

namespace Product.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private readonly IBus _bus;

        public ConsumerController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> CreateConsumerExample(Message customer)
        {
            if (customer != null)
            {
                Uri uri = new Uri("rabbitmq://guest:guest@localhost/fila");
                var endpoint = await _bus.GetSendEndpoint(uri);
                await endpoint.Send(customer);
                return Ok();
            }
            return BadRequest();
        }
    }
}