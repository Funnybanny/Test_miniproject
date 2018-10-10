using HotelBooking.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.UnitTests
{
    public class RoomDataGenerator: IEnumerable<Room>
    {
        private readonly List<Room> _rooms = new List<Room>
        {
            new Room { Id=1, Description="A" },
            new Room { Id=2, Description="B" },
        };


        public IEnumerator<Room> GetEnumerator()
        {
            return _rooms.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
