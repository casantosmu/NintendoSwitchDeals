﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NintendoSwitchDiscounts.Common.Data;

#nullable disable

namespace NintendoSwitchDiscounts.Common.Migrations
{
    [DbContext(typeof(NintendoSwitchDiscountsContext))]
    [Migration("20240321153930_RemoveGameThresholdPrice")]
    partial class RemoveGameThresholdPrice
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("NintendoSwitchDiscounts.Common.Models.Game", b =>
                {
                    b.Property<long>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("GameId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("NintendoSwitchDiscounts.Common.Models.Notification", b =>
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

            modelBuilder.Entity("NintendoSwitchDiscounts.Common.Models.Notification", b =>
                {
                    b.HasOne("NintendoSwitchDiscounts.Common.Models.Game", "Game")
                        .WithMany("Notifications")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("NintendoSwitchDiscounts.Common.Models.Game", b =>
                {
                    b.Navigation("Notifications");
                });
#pragma warning restore 612, 618
        }
    }
}