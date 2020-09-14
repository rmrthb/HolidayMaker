﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HolidayMakerAPI.Model;

namespace HolidayMakerAPI.Data
{
    public class HolidayMakerAPIContext : DbContext
    {
        public HolidayMakerAPIContext (DbContextOptions<HolidayMakerAPIContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Model.BookingRoom>().HasKey(br => new { br.BookingID, br.RoomID });
        }

        public DbSet<HolidayMakerAPI.Model.User> User { get; set; }

        public DbSet<HolidayMakerAPI.Model.Booking> Booking { get; set; }

        public DbSet<HolidayMakerAPI.Model.Room> Room { get; set; }

        public DbSet<HolidayMakerAPI.Model.Accommodation> Accommodation { get; set; }
    }
}
