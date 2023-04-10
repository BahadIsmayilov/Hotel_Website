using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service_Container.Models.HomeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Configurations.Rezervation
{
    public class HomeRoomCategorySecConfig : IEntityTypeConfiguration<HomeRoomCategorySection>
    {
        public void Configure(EntityTypeBuilder<HomeRoomCategorySection> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        }
    }
}
