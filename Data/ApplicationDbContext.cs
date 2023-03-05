using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using truckCity_api.Models;

namespace truckCity_api.Data
{	
	public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Plant>().HasIndex(u => u.Name).IsUnique();
            builder.Entity<Plant>().HasIndex(u => u.Address).IsUnique();
        }

        public DbSet<Plant> Plants { get; set; }
    }
}