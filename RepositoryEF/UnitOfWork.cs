using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;
using Domain.Interfaces;
using RepositoryEF.Repositories;

namespace RepositoryEF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Database _context;
        private readonly ApplicationDbContext _identityContext;

        public UnitOfWork(Database context, ApplicationDbContext identityDbContext)
        {
            _context = context;
            _identityContext = identityDbContext;
            Customers = new CustomerRepository(_context);
            Projects = new ProjectRepository(_context);
            Tasks = new TaskRepository(_context, identityDbContext);
            Users = new UserRepository(identityDbContext, context);

        }

        public IRepository<Customer> Customers { get;  }
        public IUserRepository Users { get; }

        public IProjectRepository Projects { get; }

        public ITaskRepository Tasks { get;  }

        public int Complete()
        {
            var appRepositoresSuccess = _context.SaveChanges();
            var identityRepositorySuccess = _identityContext.SaveChanges();
            var totalChanges = appRepositoresSuccess + identityRepositorySuccess; // both must succeed to return true
            return totalChanges;
        }
    }
}
