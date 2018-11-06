using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HotelBooking.Models;

namespace HotelBooking.UnitTests.DataGenerator
{
    class CreateBookingExceptionDataGenerator :IEnumerable<object[]>
    {
        private readonly List<object[]> _bookings = new List<object[]>
        {
            //Scenario: Start date - Past, end date - Anything later than past
            new object[] {2, DateTime.Today.AddDays(-10), DateTime.Today.AddDays(5), true, 1, 1,
                new Customer{  Id=2, Email = "bill@outlook.com", Name = "Billy" }, new Room{  Id=1, Description="A"} }
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
