using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using System.Collections.Generic;

namespace Shop.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PendingCart> PendingCartItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<ConfirmationData> ConfirmationDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Event>().HasData(
                new Event
                {
                    EventId = 1,
                    EventName = "Electronics Discount",
                    EventHeader = "Once a year, take your chance",
                    Description = "Lorem impsum Lorem impsum Lorem impsumLorem impsum" +
                    "Lorem impsum Lorem impsumvLorem impsum Lorem impsumLorem impsum",
                    Discount = 10,
                    ImageUrl = "imageName.jpg",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    SellerId = "Test"
                }
                ) ;
            //builder.Entity<ProductCategory>().HasData(
            //    new ProductCategory
            //    {
            //        ProductCategoryId = 1,
            //        Name = "Electronics",
            //        Description = "All tech products that you can find, from home electronics to personal gadgets."
            //    },
            //    new ProductCategory
            //    {
            //        ProductCategoryId = 2,
            //        Name = "Drinks",
            //        Description = "Find your prefered endergy drinks here. From energy drings to pops"
            //    },
            //    new ProductCategory
            //    {
                    
            //        ProductCategoryId = 3,
            //        Name = "Clothes",
            //        Description = "Lastes models of clothing. See and dont miss lastes mode"
            //    }
            //    );
            builder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    ProductName = "drinkBlue",
                    Description= "Taste the feeling or sth like that. ",
                    MainImage ="drinkBlue1.jpg",
                    Price = 5,
                    DiscountedPrice = 5,
                    InStock = 1000,
                    CategoryId = 2,
                    SellerId = "abcd"
                },
                new Product
                {
                    ProductId = 2,
                    ProductName = "drinkTonic",
                    Description = "Tonic afert long night drinking ",
                    MainImage = "drinkTonic1.jpg",
                    Price = 3,
                    DiscountedPrice = 3,
                    InStock = 300,
                    CategoryId = 2,
                    SellerId = "abcd"
                });
        }

        public DbSet<Shop.Models.ConfirmationData>? ConfirmationData { get; set; }
    }
}