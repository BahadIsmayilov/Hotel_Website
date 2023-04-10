using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service_Container.Models.AboutModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Configurations.AboutUsConfiguration
{
    public class AboutUsPreferncesSectionConfig : IEntityTypeConfiguration<AboutUsPreferncesSection>
    {
        public void Configure(EntityTypeBuilder<AboutUsPreferncesSection> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Prefernce).IsRequired().HasMaxLength(50);
            builder.HasOne(x => x.AboutUsSection).WithMany(x => x.AboutUsPreferncesSections).HasForeignKey(x => x.AboutUsSectionId);
        }
    }
}
