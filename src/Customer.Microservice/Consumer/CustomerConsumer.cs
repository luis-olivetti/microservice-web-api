using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Customer.Microservice;
using Customer.Microservice.Data;
using Microsoft.Extensions.Logging;
using SharedModel;

namespace Customer.Microservice.Consumer
{
    public class CustomerConsumer : IConsumer<Message>
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<Message> _logger;
        public CustomerConsumer(IApplicationDbContext context, ILogger<Message> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<Message> context)
        {
            _logger.LogInformation(context.Message.Text);

            var data = context.Message;

            var customer = new Entities.Customer
            {
                City = "itajai",
                Contact = data.Text,
                Email = "teste@teste.com.br",
                Name = data.Text
            };

            _context.Customers.Add(customer);
            await _context.SaveChanges();
        }
    }
}