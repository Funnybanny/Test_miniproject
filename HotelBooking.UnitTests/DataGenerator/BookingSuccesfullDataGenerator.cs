﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HotelBooking.Models;

namespace HotelBooking.UnitTests.DataGenerator
{
    class BookingSuccesfullDataGenerator :IEnumerable<object[]>
    {
        private readonly List<object[]> _bookings = new List<object[]>
        {
            new object[] {1, DateTime.Today.AddDays(1), DateTime.Today.AddDays(9), true, 1, 1,
                new Customer{  Id=1, Email = "joe@outlook.com", Name = "Joe"}, new Room{  Id=1, Description="A"} },
            new object[] {2, DateTime.Today.AddDays(21), DateTime.Today.AddDays(22), true, 2, 2,
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
