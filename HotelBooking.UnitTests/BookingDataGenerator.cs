using HotelBooking.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.UnitTests
{
    class BookingDataGenerator :IEnumerable<Booking>
    {
        private readonly List<Booking> _customers = new List<Booking>
        {
            new Booking {Id=1, StartDate=DateTime.Now, EndDate=DateTime.Now, IsActive=true, CustomerId=1, RoomId=1, Customer=customers[0], Room=rooms[0]},
            new Booking {Id=2, StartDate=DateTime.Now, EndDate=DateTime.Now, IsActive=true, CustomerId=2, RoomId=2, Customer=customers[1], Room=rooms[1]}
        };


        public IEnumerator<Booking> GetEnumerator()
        {
            return _customers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
