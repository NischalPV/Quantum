using Microsoft.EntityFrameworkCore;
using Quantum.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quantum.Infrastructure.Data
{
    public class QuantumDbContext : DbContext
    {
        public QuantumDbContext(DbContextOptions<QuantumDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Product> Products { get; set; }
    }
}
