﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using HolidayMaker.Client.Model;
using System.Net.Http.Headers;
using System.Collections.ObjectModel;

namespace HolidayMaker.Client.Service
{
     public class BookingService
     {
        private static readonly string url = "http://localhost:59571/api/booking/";
        private static readonly string aUrl = "http://localhost:59571/api/booking/all/";
        private static readonly string bUrl = "http://localhost:59571/api/booking/all/?email=";
        HttpClient httpClient; 

        public BookingService()
        {
            httpClient = new HttpClient();
        }

        public async Task PostBookingAsync(Booking booking)
        {
            var jsonBooking = JsonConvert.SerializeObject(booking);
            HttpContent httpContent = new StringContent(jsonBooking);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var jsonBookingDB = await httpClient.PostAsync(url, httpContent);
        }

        public async Task<ObservableCollection<Booking>> GetBookingsAsync(string email)
        {
            var bookings = new ObservableCollection<Booking>();
            var jsonBookings = await httpClient.GetStringAsync(bUrl + email);
            bookings = JsonConvert.DeserializeObject<ObservableCollection<Booking>>(jsonBookings);

            //get room price
            foreach (Booking booking in bookings)
            {
                foreach (BookedRoom bookedRoom in booking.BookedRooms)
                {
                    var jsonRoom = await httpClient.GetStringAsync("http://localhost:59571/api/Room/" + bookedRoom.RoomId);
                    var room = JsonConvert.DeserializeObject<Room>(jsonRoom);

                    bookedRoom.Price = room.Price;
                }
            }

            return bookings;
        }

        public async Task<bool> UpdateUserBookingAsync(Booking booking)
        {
            string update = aUrl + booking.Email + "/" + booking.BookingNumber;
            var updatedBooking = JsonConvert.SerializeObject(booking);
            HttpContent httpContent = new StringContent(updatedBooking);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await httpClient.PutAsync(update, httpContent);
            return response.IsSuccessStatusCode; //om allt går bra är denna true
        }

        public async Task DeleteUserBookingAsync(Booking b)
        {
            string delete = aUrl + b.Email + "/" + b.BookingNumber;
            var deletedBooking = JsonConvert.SerializeObject(b);
            HttpContent httpContent = new StringContent(deletedBooking);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            await httpClient.DeleteAsync(delete);
         
        }

        public async Task<ObservableCollection<BookedRoom>> GetBookedRoomsAsync(int accommodationId, DateTime checkIn, DateTime checkOut)
        {
            var bookedRooms = new ObservableCollection<BookedRoom>();
            var jsonBookedRooms = await httpClient.GetStringAsync(url + $"booked?accommodationId={accommodationId}&checkIn={checkIn}&checkOut={checkOut}");
            bookedRooms = JsonConvert.DeserializeObject<ObservableCollection<BookedRoom>>(jsonBookedRooms);

            return bookedRooms;
        }

     }
}
