using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RepositoryEF
{
    public class Database : IdentityDbContext
    {
        public Database() : base("SEDCTaskApp")
        {
            System.Data.Entity.Database.SetInitializer<Database>(null);
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

        }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Customer> Customers { get; set; }


    }
}
