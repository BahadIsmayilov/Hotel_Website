using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service_Container.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Configurations
{
    public class HomeRoomSectionConfig : IEntityTypeConfiguration<HomeRoomSection>
    {
        public void Configure(EntityTypeBuilder<HomeRoomSection> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.BedType).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Service).IsRequired().HasMaxLength(5000);
            builder.Property(x => x.RoomNumber).IsRequired().HasMaxLength(5);
            builder.HasOne(x => x.HomeRoomCategorySection).WithMany(x => x.HomeRoomSections).HasForeignKey(x=>x.CategoryId);
        }
    }
}
