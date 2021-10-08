using Microsoft.EntityFrameworkCore;
using SalesDbLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDbLib.Controllers
{
    public class OrdersController
    {
        private readonly SalesDbContext _context;

        public OrdersController()
        {
            _context = new SalesDbContext();
        }
        
        public async Task<List<Order>> GetAll()
        {
            return await _context.Orders
                                       .Include(x => x.Customer) 
                                       .ToListAsync();
        }
        public async Task<Order> GetbyPk(int Id)
        {
            return await _context.Orders.FindAsync(Id);
        }
        public async Task<bool> Create(Order order)
        {
            if(order.Id != 0)
            {
                throw new Exception("You must enter 0 for Id!");
            }
            await _context.Orders.AddAsync(order);
            var rowsAffected = await _context.SaveChangesAsync();
            if(rowsAffected != 1)
            {
                throw new Exception("Create failed!");
            }
            return true;
        }
        public async Task<bool> Change(int Id, Order order)
        {
            if(Id != order.Id)
            {
                throw new Exception("Id's do not match!");
            }
            _context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            var rowsAffected = await _context.SaveChangesAsync();
            if(rowsAffected != 1)
            {
                throw new Exception("Update failed!");
            }
            return true;
        }
        public async Task<bool> Remove(int Id)
        {
            var order = await _context.Orders.FindAsync(Id);
            if(order == null)
            {
                return false;
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
