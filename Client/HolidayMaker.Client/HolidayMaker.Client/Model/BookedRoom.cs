﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayMaker.Client.Model
{
    public class BookedRoom
    {
        public int RoomId { get; set; }
        public string AccommodationName { get; set; }
        public string City { get; set; }
        public string RoomType { get; set; }
        public decimal Price { get; set; }
        public bool ExtraBedBooked { get; set; } = true;
        public bool FullBoard { get; set; } = true;
        public bool HalfBoard { get; set; } = false;
        public bool AllInclusive { get; set; } = false;
    }
}
