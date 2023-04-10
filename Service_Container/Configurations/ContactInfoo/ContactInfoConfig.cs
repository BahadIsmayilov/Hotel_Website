using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service_Container.Models.ContactModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.Configurations.ContactInfoo
{
    public class ContactInfoConfig : IEntityTypeConfiguration<ContactInfo>
    {
        public void Configure(EntityTypeBuilder<ContactInfo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Header).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Text).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(500);
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(30);
            builder.Property(x=>x.Mail).IsRequired().HasMaxLength(100);
            builder.Property(x=>x.Fax).IsRequired().HasMaxLength(30);
            builder.Property(x=>x.Map).IsRequired().HasMaxLength(5000);
        }
    }
}
