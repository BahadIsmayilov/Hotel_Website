using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service_Container.Models.UserResgistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Container.SeedData
{
    public class CitySeedData : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasData(new City
            {
                Id = 1,
                Name = "Baku",
                CountryId=1
            },
           new City
           {
               Id = 2,
               Name = "Sumqayit",
               CountryId=1
           },
           new City
           {
               Id = 3,
               Name = "Qazax",
               CountryId=1
           },
           new City
           {
               Id = 4,
               Name = "Istanbul",
               CountryId = 2
           },
           new City
           {
               Id = 5,
               Name = "Ankara",
               CountryId = 2
           },
           new City
           {
               Id = 6,
               Name = "EskiShehir",
               CountryId = 2
           },
           new City
           {
               Id = 7,
               Name = "NewYork",
               CountryId = 3
           },
           new City
           {
               Id = 8,
               Name = "Californiya",
               CountryId = 3
           },
           new City
           {
               Id = 9,
               Name = "Kiev",
               CountryId = 4
           },
           new City
           {
               Id = 10,
               Name = "Xarkov",
               CountryId = 4
           });
        }
    }
}
