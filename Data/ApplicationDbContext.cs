﻿using API.Models;
using API.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; } 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //add key for portfolio
            builder.Entity<Portfolio>(x => x.HasKey(c => new { c.AppUserId, c.StockId }));

            //relation to portfolio-AppUser
            builder.Entity<Portfolio>()
                .HasOne(x => x.AppUser)
                .WithMany(u => u.PortfolioList)
                .HasForeignKey(x => x.AppUserId);

            //relation portfolio-stock
            builder.Entity<Portfolio>()
                .HasOne(x => x.Stock)
                .WithMany(u => u.PortfolioList)
                .HasForeignKey(r => r.StockId);

            //Insert Identity Role

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName ="ADMIN"

                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName ="USER",

                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
