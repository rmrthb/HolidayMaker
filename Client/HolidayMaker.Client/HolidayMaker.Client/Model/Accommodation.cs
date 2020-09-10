﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMaker.Client.Model
{
    public class Accommodation
    {
        public string AccommodationName { get; set; }
        public string City { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }

        public Accommodation(string accommodationName, string city)
        {
            AccommodationName = accommodationName;
            City = city;
            Rooms = new ObservableCollection<Room>();
            Rooms.Add(new Room("Svit", 500));
            Rooms.Add(new Room("Skrubb", 900));
        }
    }
}
