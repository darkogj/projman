using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IProjectRepository Projects { get; }
        IRepository<Customer> Customers { get; }
        ITaskRepository Tasks { get; }

        int Complete();

    }
}
