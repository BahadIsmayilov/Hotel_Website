using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service_Container.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Configurations
{
    public class HomeRoomImageSectionConfig : IEntityTypeConfiguration<HomeImageRoomSection>
    {
        public void Configure(EntityTypeBuilder<HomeImageRoomSection> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Image).IsRequired().HasMaxLength(200);
            builder.HasOne(x => x.HomeRoomSection).WithMany(x => x.HomeImageRoomSections).HasForeignKey(x => x.HomeRoomSectionId);
        }
    }
}
