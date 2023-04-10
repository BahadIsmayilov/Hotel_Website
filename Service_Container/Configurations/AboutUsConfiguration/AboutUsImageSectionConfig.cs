using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service_Container.Models.AboutModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Configurations.AboutUsConfiguration
{
    public class AboutUsImageSectionConfig : IEntityTypeConfiguration<AboutUsImageSection>
    {
        public void Configure(EntityTypeBuilder<AboutUsImageSection> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.ImageTitle).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Image).IsRequired().HasMaxLength(200);
            builder.HasOne(x => x.AboutUsSection).WithMany(x => x.AboutUsImageSections).HasForeignKey(x => x.AboutUsSectionId);
        }
    }
}
