using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service_Container.Models.AboutModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Configurations.AboutUsConfiguration
{
    public class GallerySectionConfig : IEntityTypeConfiguration<GallerySection>
    {
        public void Configure(EntityTypeBuilder<GallerySection> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(30);
            builder.Property(x => x.SubTitle).IsRequired().HasMaxLength(50);
        }
    }
}
