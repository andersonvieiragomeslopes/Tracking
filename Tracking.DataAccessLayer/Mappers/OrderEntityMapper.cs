using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.DataAccessLayer.Entities;

namespace Tracking.DataAccessLayer.Mappers
{
    public class OrderEntityMapper : BaseEntityMapper<Order>
    {
        public override void ConfigureBuilder(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.Property(x => x.Title)
                .HasColumnName("Title")
                .IsRequired();
            builder.Property(x => x.Description)
                .HasColumnName("Description")
                .IsRequired(false);
            builder.Property(x => x.Image)
                .HasColumnName("Image")
                .IsRequired();
            builder.Property(x => x.Latitude)
                .HasColumnName("Latitude")
                .IsRequired();
            builder.Property(x => x.Longitude)
                .HasColumnName("Longitude")
                .IsRequired();
            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);
        }
    }
}
