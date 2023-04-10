using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service_Container.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Configurations
{
    public class BlogsImageSectionConfig:IEntityTypeConfiguration<BlogsImageSection>
    {
        public void Configure(EntityTypeBuilder<BlogsImageSection> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Image).IsRequired().HasMaxLength(200);
            builder.HasOne(x => x.BlogsSection).WithMany(x => x.BlogsImageSections).HasForeignKey(x => x.BlogsSectionId);
        }
    }

}
