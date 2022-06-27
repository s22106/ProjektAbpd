using ProjektAbpd.Server.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjektAbpd.Shared.Models;
using ProjektAbpd.Shared.Models.DTOs;
using Server.Models;

namespace ProjektAbpd.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<DbStock> Stocks { get; set; }
        public DbSet<DbStockPrice> StockPrices { get; set; }
        public DbSet<DbUserStocks> UserStocks { get; set; }
        public DbSet<DbArticle> Articles { get; set; }
        public DbSet<DbStockArticle> StockArticles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DbStock>(opt =>
            {
                opt.HasKey(e => e.StockId);
                opt.Property(e => e.Name).IsRequired();
                opt.Property(e => e.Ticker).IsRequired();

                opt.ToTable("Stock");
            });

            builder.Entity<DbStockPrice>(opt =>
            {
                opt.HasKey(e => new { e.StockId, e.Date });
                opt.HasOne(e => e.Stock).WithMany(e => e.Prices).HasForeignKey(e => e.StockId);
                opt.Property(e => e.Close).IsRequired();
                opt.Property(e => e.High).IsRequired();
                opt.Property(e => e.Low).IsRequired();
                opt.Property(e => e.Open).IsRequired();
                opt.Property(e => e.Volume).IsRequired();

                opt.ToTable("Price");
            });

            builder.Entity<DbUserStocks>(opt =>
            {
                opt.HasKey(e => new { e.StockId, e.UserId });

                opt.HasOne(e => e.Stock).WithMany(e => e.Watchlist).HasForeignKey(e => e.StockId);
                opt.HasOne(e => e.User).WithMany(e => e.Wishlisted).HasForeignKey(e => e.UserId);
                opt.ToTable("UserStocks");
            });

            builder.Entity<DbArticle>(opt =>
            {
                opt.HasKey(e => e.ArticleUrl);
                opt.HasIndex(e => e.ArticleUrl).IsUnique();
                opt.Property(e => e.ImageUrl);
                opt.Property(e => e.Title).IsRequired();
                opt.Property(e => e.Description);

                opt.ToTable("Article");
            });
            builder.Entity<DbStockArticle>(opt =>
            {
                opt.HasKey(e => new { e.ArticleUrl, e.StockId });
                opt.HasOne(e => e.Article).WithMany(e => e.StockArticles).HasForeignKey(e => e.ArticleUrl);
                opt.HasOne(e => e.Stock).WithMany(e => e.Articles).HasForeignKey(e => e.StockId);

                opt.ToTable("StockArticle");
            });
        }
    }
}
