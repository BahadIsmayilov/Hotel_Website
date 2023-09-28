using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service_Container.Models.UserResgistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.SeedData
{
    public class CountrySeedData : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(new Country
            {
                Id = 1,
                Name = "Azerbaijan"
            },
            new Country
            {
                Id = 2,
                Name = "Turkey"
            },
            new Country
            {
                Id = 3,
                Name = "Usa"
            },
            new Country
            {
                Id = 4,
                Name = "Ukrain"
            });
        }
    }
}
