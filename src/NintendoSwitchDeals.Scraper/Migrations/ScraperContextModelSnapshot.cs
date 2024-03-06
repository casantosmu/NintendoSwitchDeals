﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using NintendoSwitchDeals.Scraper.Data;
using NintendoSwitchDeals.Scraper.Models;

#nullable disable

namespace NintendoSwitchDeals.Scraper.Migrations
{
    [DbContext(typeof(ScraperContext))]
    partial class ScraperContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("NintendoSwitchDeals.Scraper.Models.Game", b =>
                {
                    b.Property<long>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ThresholdPrice")
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("GameId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("NintendoSwitchDeals.Scraper.Models.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("DiscountPrice")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("TEXT");

                    b.Property<long>("GameId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("PublishedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("NotificationId");

                    b.HasIndex("GameId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("NintendoSwitchDeals.Scraper.Models.Notification", b =>
                {
                    b.HasOne("NintendoSwitchDeals.Scraper.Models.Game", "Game")
                        .WithMany("Notifications")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("NintendoSwitchDeals.Scraper.Models.Game", b =>
                {
                    b.Navigation("Notifications");
                });
#pragma warning restore 612, 618
        }
    }
}
