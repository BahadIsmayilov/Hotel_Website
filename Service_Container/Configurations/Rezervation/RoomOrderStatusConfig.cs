using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service_Container.Models.Rezervation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Configurations.Rezervation
{
    public class RoomOrderStatusConfig : IEntityTypeConfiguration<RoomOrderStatus>
    {
        public void Configure(EntityTypeBuilder<RoomOrderStatus> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Status).IsRequired().HasMaxLength(20);
            builder.HasOne(x => x.HomeRoomSection).WithOne(x => x.RoomOrderStatus).HasForeignKey<RoomOrderStatus>(x => x.HomeRoomSectionId);
            builder.HasOne(x => x.Booking).WithOne(x => x.RoomOrderStatus).HasForeignKey<RoomOrderStatus>(x => x.BookingId);
        }
    }
}
