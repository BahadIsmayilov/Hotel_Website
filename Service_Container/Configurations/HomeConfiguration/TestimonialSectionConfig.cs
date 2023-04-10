using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service_Container.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Configurations
{
    public class TestimonialSectionConfig : IEntityTypeConfiguration<TestimonialSection>
    {
        public void Configure(EntityTypeBuilder<TestimonialSection> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Comment).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.User).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Image).IsRequired().HasMaxLength(200);
        }
    }
}
