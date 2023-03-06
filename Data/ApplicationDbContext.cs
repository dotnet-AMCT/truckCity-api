using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
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
            builder.Entity<Truck>().HasIndex(u => u.LicencePlate).IsUnique();
            builder.Entity<Plant>().HasIndex(u => u.Name).IsUnique();
            builder.Entity<Plant>().HasIndex(u => u.Address).IsUnique();

        }

        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Plant> Plants { get; set; }

    }
}