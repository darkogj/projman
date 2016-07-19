using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace RepositoryEF.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly Database _context;
        public CustomerRepository(Database context)
        {
            _context = context;
        }

        public void Add(Customer item)
        {
            _context.Customers.Add(item);
            item.DateCreated = DateTime.UtcNow;
            item.IsActive = true;
        }

        public Customer Get(int id)
        {
            return _context.Customers.Find(id);
        }

        public IEnumerable<Customer> GetAllActive()
        {
            return _context.Customers.Where(c => c.IsActive).ToList();
        }

        public void Deactivate(int id)
        {
            var dbCustomer = Get(id);
            if (dbCustomer != null) dbCustomer.IsActive = false;
        }

        public bool Exists(int id)
        {
            var dbCustomer = _context.Customers.Find(id);
            if (dbCustomer == null) return false;
            return true;
        }
    }
}
