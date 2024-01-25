﻿// <auto-generated />
using System;
using HotelDDD.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelDDD.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240118171129_reservav2")]
    partial class reservav2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HotelDDD.Domain.Customer.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotDePasse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ReservationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("WalletId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("HotelDDD.Domain.Reservation.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CheckInDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckOutDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Reservation", (string)null);
                });

            modelBuilder.Entity("HotelDDD.Domain.Reservation.ReservationRoom", b =>
                {
                    b.Property<Guid>("ReservationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RoomType")
                        .HasColumnType("int");

                    b.HasKey("ReservationId", "RoomType");

                    b.ToTable("ReservationRoom", (string)null);
                });

            modelBuilder.Entity("HotelDDD.Domain.Room.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Amenities")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PricePerNight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Room", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("9bc27c47-44d4-4a98-807f-c647dd29bf68"),
                            Amenities = "Lit 1 place,Wifi,TV",
                            PricePerNight = 50m,
                            Type = "Standard"
                        },
                        new
                        {
                            Id = new Guid("7ea19e79-a901-47e5-9779-6e3dc022f98d"),
                            Amenities = "Lit 2 places,Wifi,TV écran plat,Minibar,Climatiseur",
                            PricePerNight = 100m,
                            Type = "Superior"
                        },
                        new
                        {
                            Id = new Guid("8e230753-bdb8-4bdc-b8e6-67eda791d03e"),
                            Amenities = "Lit 2 places,Wifi,TV écran plat,Minibar,Climatiseur,Baignoire,Terrasse",
                            PricePerNight = 200m,
                            Type = "Suite"
                        });
                });

            modelBuilder.Entity("HotelDDD.Domain.Wallet.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PreferredCurrency")
                        .IsRequired()
                        .HasColumnType("nvarchar(24)");

                    b.HasKey("Id");

                    b.ToTable("Wallet", (string)null);
                });

            modelBuilder.Entity("HotelDDD.Domain.Reservation.ReservationRoom", b =>
                {
                    b.HasOne("HotelDDD.Domain.Reservation.Reservation", "Reservation")
                        .WithMany("ReservationRooms")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("HotelDDD.Domain.Reservation.Reservation", b =>
                {
                    b.Navigation("ReservationRooms");
                });
#pragma warning restore 612, 618
        }
    }
}