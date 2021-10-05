using Microsoft.EntityFrameworkCore;
using WebTrade.Domain.Models;

namespace WebTrade.Infrastructure
{
    public class WebTradeDbContext : DbContext
    {
        public WebTradeDbContext(DbContextOptions<WebTradeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Buyer> Buyers{ get; set; }
        public DbSet<Market> Markets{ get; set; }
        public DbSet<Trade> Trades{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Buyer>(entity =>
            {
                entity.ToTable("Buyer");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(20).IsRequired();
            });

            modelBuilder.Entity<Market>(entity =>
            {
                entity.ToTable("Market");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SecurityCode).HasMaxLength(20).IsRequired();
                entity.Property(e => e.MarketPrice).IsRequired();
            });

            modelBuilder.Entity<Trade>(entity =>
            {
                entity.ToTable("Trade");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TradePrice).IsRequired();
                entity.Property(e => e.TradeQuantity).IsRequired();
                entity.Property(e => e.TradeDate).IsRequired();
                entity.HasOne(e => e.Buyer).WithMany(e => e.Trades).HasForeignKey(f => f.BuyerId);
                entity.HasOne(e => e.Market).WithMany(e => e.Trades).HasForeignKey(f => f.MarketId);
            });
        }
    }
}
