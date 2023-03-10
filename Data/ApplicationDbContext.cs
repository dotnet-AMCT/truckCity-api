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
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Part> Part { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Truck>().HasIndex(u => u.LicencePlate).IsUnique();
            builder.Entity<Truck>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.BrokenParts)
                    .HasConversion(
                        from => string.Join(";", from),
                        to => string.IsNullOrEmpty(to) ? new List<string>() : to.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList(),
                        new ValueComparer<List<string>>(
                            (c1, c2) => c1.SequenceEqual(c2),
                            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                            c => c.ToList()
                    )
                );
            });
            builder.Entity<Truck>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.CompatiblePartCodes)
                    .HasConversion(
                        from => string.Join(";", from),
                        to => string.IsNullOrEmpty(to) ? new List<string>() : to.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList(),
                        new ValueComparer<List<string>>(
                            (c1, c2) => c1.SequenceEqual(c2),
                            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                            c => c.ToList()
                    )
                );
            });
            builder.Entity<Plant>().HasIndex(u => u.Name).IsUnique();
            builder.Entity<Plant>().HasIndex(u => u.Address).IsUnique();
            builder.Entity<Part>().Property(c => c.Name).HasConversion<string>();
            base.OnModelCreating(builder);
        }
    }
}