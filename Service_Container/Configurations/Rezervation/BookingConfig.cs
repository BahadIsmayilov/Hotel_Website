using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service_Container.Models.Rezervation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Configurations.Rezervation
{
    public class BookingConfig : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.CustomerName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PinCode).IsRequired().HasMaxLength(7);
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(12);
            builder.Property(x => x.RoomNumber).IsRequired().HasMaxLength(5);
            builder.Property(x => x.Roomtype).IsRequired().HasMaxLength(50);
        }
    }
}
