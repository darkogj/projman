using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace ProjectManagement.Models.Configurations
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<User>
    {
        public ApplicationUserConfiguration()
        {
            Property(u => u.UserName).IsRequired();
            HasMany<IdentityUserRole>(u => u.Roles);
        }
    }
}