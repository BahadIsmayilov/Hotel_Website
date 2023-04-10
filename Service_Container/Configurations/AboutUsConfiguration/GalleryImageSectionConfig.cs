using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service_Container.Models.AboutModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Configurations.AboutUsConfiguration
{
    public class GalleryImageSectionConfig : IEntityTypeConfiguration<GalleryImageSection>
    {
        public void Configure(EntityTypeBuilder<GalleryImageSection> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.ImageTitle).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Image).IsRequired().HasMaxLength(200);
            builder.HasOne(x => x.GallerySection).WithMany(x => x.GalleryImageSections).HasForeignKey(x => x.GallerSectionId);
        }

    }
}
