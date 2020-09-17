﻿// <auto-generated />
using System;
using HolidayMakerAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HolidayMakerAPI.Migrations
{
    [DbContext(typeof(HolidayMakerAPIContext))]
    partial class HolidayMakerAPIContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HolidayMakerAPI.Model.Accommodation", b =>
                {
                    b.Property<int>("AccommodationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccommodationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DistanceToBeach")
                        .HasColumnType("int");

                    b.Property<int>("DistanceToCenter")
                        .HasColumnType("int");

                    b.Property<bool>("HasEntertainment")
                        .HasColumnType("bit");

                    b.Property<bool>("HasKidsClub")
                        .HasColumnType("bit");

                    b.Property<bool>("HasPool")
                        .HasColumnType("bit");

                    b.Property<bool>("HasRestaurant")
                        .HasColumnType("bit");

                    b.Property<decimal>("Rating")
                        .HasColumnType("decimal(10,1)");

                    b.Property<string>("TypeOfAccommodation")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccommodationID");

                    b.ToTable("Accommodation");
                });

            modelBuilder.Entity("HolidayMakerAPI.Model.Booking", b =>
                {
                    b.Property<int>("BookingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BookingNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CheckIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CheckOut")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(10,1)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("BookingID");

                    b.HasIndex("UserID");

                    b.ToTable("Booking");
                });

            modelBuilder.Entity("HolidayMakerAPI.Model.BookingRoom", b =>
                {
                    b.Property<int>("BookingID")
                        .HasColumnType("int");

                    b.Property<int>("RoomID")
                        .HasColumnType("int");

                    b.Property<bool>("AllInclusive")
                        .HasColumnType("bit");

                    b.Property<bool>("ExtraBedBooked")
                        .HasColumnType("bit");

                    b.Property<bool>("FullBoard")
                        .HasColumnType("bit");

                    b.Property<bool>("HalfBoard")
                        .HasColumnType("bit");

                    b.HasKey("BookingID", "RoomID");

                    b.HasIndex("RoomID");

                    b.ToTable("BookingRoom");
                });

            modelBuilder.Entity("HolidayMakerAPI.Model.Room", b =>
                {
                    b.Property<int>("RoomID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccommodationID")
                        .HasColumnType("int");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,1)");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.Property<string>("RoomType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoomID");

                    b.HasIndex("AccommodationID");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("HolidayMakerAPI.Model.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("HolidayMakerAPI.Model.Booking", b =>
                {
                    b.HasOne("HolidayMakerAPI.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HolidayMakerAPI.Model.BookingRoom", b =>
                {
                    b.HasOne("HolidayMakerAPI.Model.Booking", "Booking")
                        .WithMany("BookedRooms")
                        .HasForeignKey("BookingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HolidayMakerAPI.Model.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HolidayMakerAPI.Model.Room", b =>
                {
                    b.HasOne("HolidayMakerAPI.Model.Accommodation", "Accommodation")
                        .WithMany("Rooms")
                        .HasForeignKey("AccommodationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
