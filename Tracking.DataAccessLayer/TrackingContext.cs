using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Tracking.DataAccessLayer.Entities;
using Tracking.DataAccessLayer.Mappers;

namespace Tracking.DataAccessLayer
{
    public class TrackingContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected readonly IConfiguration Configuration;
        public TrackingContext(DbContextOptions<TrackingContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
            Database.EnsureCreatedAsync().Wait();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserEntityMapper());
            modelBuilder.ApplyConfiguration(new OrderEntityMapper());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
        }
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            string createdAt = "CreatedAt";
            string updatedAt = "UpdatedAt";
            PropertyEntry propertyCreatedAt;
            PropertyEntry propertyUpdatedAt;
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                try
                {
                    propertyCreatedAt = entry.Property(createdAt);
                    propertyUpdatedAt = entry.Property(updatedAt);
                }
                catch (Exception ex)
                {
                    propertyCreatedAt = null;
                    continue;
                }

                if (propertyCreatedAt != null && entry.State == EntityState.Added)
                {
                    if ((DateTime)propertyCreatedAt.CurrentValue == default)
                    {
                        propertyCreatedAt.CurrentValue = DateTime.UtcNow;
                        propertyUpdatedAt.CurrentValue = DateTime.UtcNow;
                    }
                }

                try
                {
                    propertyUpdatedAt = entry.Property(updatedAt);
                }
                catch (Exception ex)
                {
                    propertyUpdatedAt = null;
                    continue;
                }

                if (propertyUpdatedAt != null && (entry.State == EntityState.Added
                    || entry.State == EntityState.Modified))
                {
                    entry.Property(updatedAt).CurrentValue = DateTime.Now;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
