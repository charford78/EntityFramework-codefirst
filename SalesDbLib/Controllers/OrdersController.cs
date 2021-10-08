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
        public Order GetbyPk(int Id)
        {
            return _context.Orders.Find(Id);
        }
        public bool Create(Order order)
        {
            if(order.Id != 0)
            {
                throw new Exception("You must enter 0 for Id!");
            }
            _context.Orders.Add(order);
            var rowsAffected = _context.SaveChanges();
            if(rowsAffected != 1)
            {
                throw new Exception("Create failed!");
            }
            return true;
        }
        public bool Change(int Id, Order order)
        {
            if(Id != order.Id)
            {
                throw new Exception("Id's do not match!");
            }
            _context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            var rowsAffected = _context.SaveChanges();
            if(rowsAffected != 1)
            {
                throw new Exception("Update failed!");
            }
            return true;
        }
        public bool Remove(int Id)
        {
            var order = _context.Orders.Find(Id);
            if(order == null)
            {
                return false;
            }
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return true
        }
    }
}
