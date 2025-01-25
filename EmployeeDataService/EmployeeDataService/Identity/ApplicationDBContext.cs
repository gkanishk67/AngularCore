using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeDataService.Identity
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext() : base(GetOptions())
        {

        }

        private static DbContextOptions GetOptions(string connectionString = "data source=PG02PVPJ\\SQLEXPRESS;initial catalog=EmployeeIdentity;integrated security=True;")
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }
    }
}
