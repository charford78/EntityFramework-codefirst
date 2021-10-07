using Microsoft.EntityFrameworkCore;
using SalesDbLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDbLib.Controllers
{
    public class CustomersController
    {
        private readonly SalesDbContext _context;

        public CustomersController()
        {
            _context = new SalesDbContext();
        }
        public async Task<List<Customer>> GetAll()
        {
            return await _context.Customers
                                    .OrderBy(o => o.Name)
                                    .ToListAsync();
        }

    }
}
