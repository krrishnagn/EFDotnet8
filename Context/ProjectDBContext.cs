using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BackendProj.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BackendProj.Context
{
    public class ProjectDBContext : DbContext
    {
      public ProjectDBContext(DbContextOptions<ProjectDBContext> options) : base(options)
      {

      }
      public DbSet<Empolyee> EmpolyeesTable { get; set;}
    }
    public class ProjectDbContextFactory : IDesignTimeDbContextFactory<ProjectDBContext>
    {
        public ProjectDBContext CreateDbContext(string[] args)
        {
            // Create a new configuration builder
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Create a DbContextOptionsBuilder and configure it to use SQL Server
            var optionsBuilder = new DbContextOptionsBuilder<ProjectDBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DBConnectionString"));

            return new ProjectDBContext(optionsBuilder.Options);
        }
    }

}