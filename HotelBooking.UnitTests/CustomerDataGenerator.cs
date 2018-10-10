using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HotelBooking.Models;

namespace HotelBooking.UnitTests
{
    public class CustomerDataGenerator : IEnumerable<object[]>
    {

        private readonly List<object[]> _customers = new List<object[]>
        {
            new object[] { 3, "joe@outlook.com", "Joe"},
            new object[] { 4, "bill@outlook.com", "Billy" },
        };


        public IEnumerator<object[]> GetEnumerator()
        {
            return _customers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
