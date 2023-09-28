using Microsoft.EntityFrameworkCore;
using Service_Container.Models;
using Service_Container.Models.AboutModels;
using Service_Container.Models.ContactModels;
using Service_Container.Models.HomeModels;
using Service_Container.Models.Rezervation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Service_Container.Models.UserResgistration;
using Microsoft.AspNetCore.Identity;

namespace Service_Container.DAL
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) :base(option)
        {
        }

        public DbSet<HeroTextSection> HeroTextSection { get; set; }
        public DbSet<HeroImgSection> HeroImgSections { get; set; }
        public DbSet<ServiceSection> ServiceSections { get; set; }
        public DbSet<TestimonialSection> TestimonialSection { get; set; }
        public DbSet<BlogsSection> BlogsSections { get; set; }
        public DbSet<BlogsImageSection> BlogsImageSections { get; set; }
        public DbSet<AboutUsSection> AboutUsSections { get; set; }
        public DbSet<AboutUsPreferncesSection> AboutUsPreferncesSections { get; set; }
        public DbSet<AboutUsImageSection> AboutUsImageSections { get; set; }
        public DbSet<AboutTextSection> AboutTextSections { get; set; }
        public DbSet<GallerySection> GallerySections { get; set; }
        public DbSet<GalleryImageSection> GalleryImageSections { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<HomeRoomSection> HomeRoomSections { get; set; }
        public DbSet<HomeImageRoomSection> HomeImageRoomSections { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<RoomOrderStatus> RoomOrderStatuses { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<HomeRoomCategorySection> HomeRoomCategorySections { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);


        }
    }
}
