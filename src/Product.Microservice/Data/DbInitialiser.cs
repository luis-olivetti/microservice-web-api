using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Product.Microservice.Data
{
    public class DbInitialiser
    {
        private readonly ApplicationDbContext _context;

        public DbInitialiser(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Run()
        {
            _context.Database.Migrate();
        }
    }
}