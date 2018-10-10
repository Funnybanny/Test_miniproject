using HotelBooking.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.UnitTests
{
    public class RoomDataGenerator: IEnumerable<object[]>
    {
        private readonly List<object[]> _rooms = new List<object[]>
        {
            new object[] { 1, "A" },
            new object[] { 2, "B" },
        };


        public IEnumerator<object[]> GetEnumerator()
        {
            return _rooms.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
