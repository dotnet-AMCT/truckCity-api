using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using truckCity_api.Models;

namespace truckCity_api.Data
{	
	public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {}

        public DbSet<Truck> Truck { get; set; }
        public DbSet<Part> Part { get; set; }
    }
}