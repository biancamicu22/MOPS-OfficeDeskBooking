using System;
using DataLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLibrary
{
    public partial class MOPSContext : IdentityDbContext<User>
    {
        public MOPSContext()
        {
        }

        public MOPSContext(DbContextOptions<MOPSContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Desk> Desks { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                throw new Exception("options builder not configured");
            }
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne<User>(c => c.User)
                    .WithMany(u => u.Bookings)
                    .HasForeignKey(c => c.User_Id).OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<Desk>(c => c.Desk)
                    .WithMany(t => t.Bookings)
                    .HasForeignKey(c => c.DeskNumber).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Desk>(entity =>
            {
                entity.HasKey(e => e.DeskNumber);
            });


            OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
