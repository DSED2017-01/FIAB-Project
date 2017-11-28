using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using FishInABox.Models;

namespace FishInABox.Models
{
    public partial class FishInABoxContext : DbContext
    {

        public FishInABoxContext(DbContextOptions<FishInABoxContext> opts) : base(opts) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {}

        public DbSet<FishInABox.Models.MarineSpecies> MarineSpecies { get; set; }
    }
}
