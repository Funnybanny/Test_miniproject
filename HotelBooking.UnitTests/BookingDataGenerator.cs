using HotelBooking.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.UnitTests
{
    class BookingDataGenerator :IEnumerable<Booking>

    {
        private readonly List<Booking> _bookings = new List<Booking>
        {
            new Booking {Id=1, StartDate=DateTime.Now, EndDate=DateTime.Now, IsActive=true, CustomerId=1, RoomId=1,
                Customer =new Customer{  Id=1, Email = "joe@outlook.com", Name = "Joe"}, Room=new Room{  Id=1, Description="A"} },
            new Booking {Id=2, StartDate=DateTime.Now, EndDate=DateTime.Now, IsActive=true, CustomerId=2, RoomId=2,
                Customer =new Customer{  Id=2, Email = "bill@outlook.com", Name = "Billy" }, Room=new Room{  Id=2, Description="B"} }
        };


        public IEnumerator<Booking> GetEnumerator()
        {
            return _bookings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
