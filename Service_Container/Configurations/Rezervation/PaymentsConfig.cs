using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service_Container.Models.Rezervation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Configurations.Rezervation
{
    public class PaymentsConfig : IEntityTypeConfiguration<Payments>
    {
        public void Configure(EntityTypeBuilder<Payments> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Amount).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(x => x.TotalPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(x => x.PaymentStatus).IsRequired().HasMaxLength(20);
            builder.HasOne(x => x.Bookings).WithOne(x => x.Payments).HasForeignKey<Payments>(x => x.BookingId);
            builder.HasOne(x => x.HomeRoomSection).WithOne(x => x.Payments).HasForeignKey<Payments>(x => x.HomeRoomSectionId);
        }
    }
}
