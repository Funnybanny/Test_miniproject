using HotelBooking.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.UnitTests
{
    class BookingDataGenerator :IEnumerable<object[]>

    {
        private readonly List<object[]> _bookings = new List<object[]>
        {
            new object[] {1, DateTime.Now, DateTime.Now, true, 1, 1,
                new Customer{  Id=1, Email = "joe@outlook.com", Name = "Joe"}, new Room{  Id=1, Description="A"} },
            new object[] {2, DateTime.Now, DateTime.Now, true, 2, 2,
                new Customer{  Id=2, Email = "bill@outlook.com", Name = "Billy" }, new Room{  Id=2, Description="B"} }
        };


        public IEnumerator<object[]> GetEnumerator()
        {
            return _bookings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
