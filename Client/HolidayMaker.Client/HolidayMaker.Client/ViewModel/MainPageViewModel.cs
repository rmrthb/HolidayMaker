﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HolidayMaker.Client.Model;
using HolidayMaker.Client.Service;

namespace HolidayMaker.Client.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public BookingService bookingService = new BookingService();
        
        public ObservableCollection<Accommodation> ListOfAccommodations = new ObservableCollection<Accommodation>();
        public ObservableCollection<Accommodation> SearchResult = new ObservableCollection<Accommodation>();

        public ObservableCollection<BookedRoom> AddedRooms = new ObservableCollection<BookedRoom>();
        public decimal TotalPrice = 0;
        AccommodationService accommodationService = new AccommodationService();
        private User user;
        public event PropertyChangedEventHandler PropertyChanged;

       

        //public void MockData()
        //{
        //    ListOfAccommodations.Add(new Accommodation("Erics Lya", "Malmö", 1.7m));
        //    ListOfAccommodations.Add(new Accommodation("Rays Lya", "Eslöv", 0.2m));
        //    ListOfAccommodations.Add(new Accommodation("Mickes hak", "Hjärup", 2.5m));
        //    ListOfAccommodations.Add(new Accommodation("Jennys Etage", "Los Angeles", 4.9m));
        //    ListOfAccommodations.Add(new Accommodation("Glenns koja", "Vardagsrummet", 5m));
        //}
        public async void GetAccommodations()
        {
            var accommodations = await accommodationService.GetAccommodationsAsync();
            foreach (Accommodation item in accommodations)
            {
                ListOfAccommodations.Add(item);
            }
        }

        public async void GetAvailableRooms()
        {
            var accommodations = await accommodationService.GetAccommodationsAsync();
            List<Room> roomList = new List<Room>();

            foreach(var acc in accommodations)
            {
                foreach(var room in acc.Rooms)
                {
                    roomList.Add(room);
                }
            }

            var bookedRooms = await bookingService.GetBookedRooms();

            IEnumerable<Room> availableRooms = roomList.Where((item) => !bookedRooms.Any((item2) => item.RoomId == item2.RoomId));

        }

        public void AddToBooking(Room room, Accommodation accommodation)
        {
           
            BookedRoom bookedRoom = new BookedRoom();

            bookedRoom.AccommodationName = accommodation.AccommodationName;
            bookedRoom.City = accommodation.City;
            bookedRoom.Price = room.Price;
            bookedRoom.RoomType = room.RoomType;
            bookedRoom.RoomId = room.RoomId;
            AddedRooms.Add(bookedRoom);
            CalculateTotalPrice();
            //booking.BookedRooms.Add(bookedRoom);
            //booking.TotalPrice = CalculateTotalPrice(booking);

            //return booking;

        }

        public async void CreateBooking()
        {
            Booking booking = new Booking();
            booking.BookingNumber = CreateBookingNumber();
            booking.CheckIn = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            booking.CheckOut = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(2);
            booking.TotalPrice = TotalPrice;
            booking.BookedRooms = AddedRooms;
            booking.Email = User.Email;
            
            await PostBookingAsync(booking);
        }
        
        public async Task PostBookingAsync(Booking booking)
        {
            await bookingService.PostBooking(booking);
        }

        public string CreateBookingNumber()
        {
            //bookingNumber += 1;
            //return bookingNumber;
            Random rndGenerator = new Random();
            StringBuilder myRandomString = new StringBuilder();

            for (int i = 0; i < 10; i++) // set how many random characters we want to generate
            {
                bool isAlpha = Convert.ToBoolean(rndGenerator.Next(0, 2));
                int rndNumber = isAlpha ? rndGenerator.Next(0, 26) : rndGenerator.Next(0, 10); // set the boundry 26 letters in alphabet, 10 numbers
                if (isAlpha)
                {
                    bool isUpper = Convert.ToBoolean(rndGenerator.Next(0, 2));
                    rndNumber += isUpper ? 65 : 97; // add an offset of 65 which gets us to an A in the ASCII table, 97 is same for a
                }

                myRandomString.Append(isAlpha ? ((char)rndNumber).ToString() : rndNumber.ToString());
            }

            return myRandomString.ToString();
        }

        public void CalculateTotalPrice()
        {
            TotalPrice = 0;
            foreach (BookedRoom room in AddedRooms)
            {
                TotalPrice += room.Price;
            }
        }

        public void SearchFunction(string search)
        {
            SearchResult.Clear();

            if (search == "")
            {
                SearchResult.Clear();
            }
            else
            {
                foreach (var s in ListOfAccommodations)
                {
                    if (s.AccommodationName.ToLower().Contains(search.ToLower())
                        || s.City.ToLower().Contains(search.ToLower()))
                    {
                        SearchResult.Add(s);
                    }
                }
            }
        }

        public void SortingFunction()
        {
            var sorted = SearchResult.OrderByDescending(x => x.Rating);

        }

        public User User
        {
            get
            {
                return this.user;
            }
            set
            {
                this.user = value;
                NotifyPropertyChanged("Email");
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

