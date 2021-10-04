﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebTrade.Infrastructure;

namespace WebTrade.Infrastructure.Migrations
{
    [DbContext(typeof(WebTradeDbContext))]
    partial class WebTradeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("WebTrade.Domain.Models.Buyer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Buyer");
                });

            modelBuilder.Entity("WebTrade.Domain.Models.Market", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<double>("MarketPrice")
                        .HasColumnType("REAL");

                    b.Property<string>("SecurityCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Market");
                });

            modelBuilder.Entity("WebTrade.Domain.Models.Trade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MarketId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TradeDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("TradePrice")
                        .HasColumnType("REAL");

                    b.Property<int>("TradeQuantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("MarketId");

                    b.ToTable("Trade");
                });

            modelBuilder.Entity("WebTrade.Domain.Models.Trade", b =>
                {
                    b.HasOne("WebTrade.Domain.Models.Buyer", "Buyer")
                        .WithMany("Trades")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebTrade.Domain.Models.Market", "Market")
                        .WithMany("Trades")
                        .HasForeignKey("MarketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Buyer");

                    b.Navigation("Market");
                });

            modelBuilder.Entity("WebTrade.Domain.Models.Buyer", b =>
                {
                    b.Navigation("Trades");
                });

            modelBuilder.Entity("WebTrade.Domain.Models.Market", b =>
                {
                    b.Navigation("Trades");
                });
#pragma warning restore 612, 618
        }
    }
}
