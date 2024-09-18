using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FarmContext: DbContext
    {
        public DbSet<Livestock> Livestocks { get; set; }
        public DbSet<Cow> Cows { get; set; }    
        public DbSet<Goat> Goats { get; set; }
        public DbSet<Sheep> Sheeps { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-UG4BEPP;Initial Catalog=FarmManagement;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Livestock>().ToTable("Livestock")
                .HasDiscriminator<string>("LivestockType")
                .HasValue<Cow>("1")
                .HasValue<Goat>("2")
                .HasValue<Sheep>("3");
        }
    }
}
