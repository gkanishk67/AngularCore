using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EmployeeDataService.Models;

namespace EmployeeDataService.Data
{
    public partial class EmployeesAppDBContext : DbContext
    {
        public EmployeesAppDBContext()
        {
        }

        public EmployeesAppDBContext(DbContextOptions<EmployeesAppDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EmployeeModels> EmployeeModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("data source=PG02PVPJ\\SQLEXPRESS;initial catalog=EmployeesAppDB;integrated security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
