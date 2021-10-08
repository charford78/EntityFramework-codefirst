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
        public async Task<Customer> GetbyPk(int Id)
        {
            return await _context.Customers.FindAsync(Id);
        }
        public async Task<Customer> GetbyCode(string Code)
        {
            return await _context.Customers.SingleOrDefaultAsync(c => c.Code == Code);
        }
        public async Task<bool> Create(Customer customer)
        {
            if(customer.Id != 0)
            {
                throw new Exception("You must enter 0 for Id!");
            }
            await _context.Customers.AddAsync(customer);
            var rowsAffected = await _context.SaveChangesAsync();
            if(rowsAffected != 1)
            {
                throw new Exception("Create failed!");
            }
            return true;
        }
        public async Task<bool> Change(int Id, Customer customer)
        {
            if(Id != customer.Id)
            {
                throw new Exception("Id's do not match!");
            }
            _context.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            var rowsAffected = await _context.SaveChangesAsync();
            if(rowsAffected != 1)
            {
                throw new Exception("Update failed!");
            }
            return true;
        }
        public async Task<bool> Remove(int Id)
        {
            var customer = await _context.Customers.FindAsync(Id);
            if(customer == null)
            {
                return false;
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
