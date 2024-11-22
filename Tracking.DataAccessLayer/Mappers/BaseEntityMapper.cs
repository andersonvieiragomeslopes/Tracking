using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.DataAccessLayer.Entities;

namespace Tracking.DataAccessLayer.Mappers
{
    public abstract class BaseEntityMapper<TEntity> : IEntityTypeConfiguration<TEntity>
          where TEntity : ModelBase
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(c => c.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();

            builder.Property(c => c.UpdatedAt)
                .HasColumnName("UpdatedAt")
                .IsRequired();

            ConfigureBuilder(builder);
        }

        public abstract void ConfigureBuilder(EntityTypeBuilder<TEntity> builder);
    }
}
