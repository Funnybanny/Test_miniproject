using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HotelBooking.Models;

namespace HotelBooking.UnitTests.DataGenerator
{
    class CreateBookingScenarioDataGenerator :IEnumerable<object[]>
    {
        private readonly List<object[]> _bookings = new List<object[]>
        {
            //Scenario: Start date - before occupied, end date - Occupied start
            new object[] {2, DateTime.Today.AddDays(1), DateTime.Today.AddDays(10), true, 1, 1,
                new Customer{  Id=2, Email = "joe@outlook.com", Name = "Joe"}, new Room{  Id=1, Description="A"}, false },

            //Scenario: Start date - before occupied, end date - Occupied end
            new object[] {2, DateTime.Today.AddDays(1), DateTime.Today.AddDays(20), true, 1, 1,
                new Customer{  Id=2, Email = "bill@outlook.com", Name = "Billy" }, new Room{  Id=1, Description="A"}, false },

            //Scenario: Start date - before occupied, end date - after occupied
            new object[] {2, DateTime.Today.AddDays(1), DateTime.Today.AddDays(21), true, 1, 1,
                new Customer{  Id=2, Email = "bill@outlook.com", Name = "Billy" }, new Room{  Id=1, Description="A"}, false },

            //Scenario: Start date - occupied start, end date - after occupied
            new object[] {2, DateTime.Today.AddDays(10), DateTime.Today.AddDays(21), true, 1, 1,
                new Customer{  Id=2, Email = "bill@outlook.com", Name = "Billy" }, new Room{  Id=1, Description="A"}, false },

            //Scenario: Start date - occupied end, end date - after occupied
            new object[] {2, DateTime.Today.AddDays(20), DateTime.Today.AddDays(21), true, 1, 1,
                new Customer{  Id=2, Email = "bill@outlook.com", Name = "Billy" }, new Room{  Id=1, Description="A"}, false },

            //Scenario: Start date - occupied start, end date - occupied end
            new object[] {2, DateTime.Today.AddDays(10), DateTime.Today.AddDays(20), true, 1, 1,
                new Customer{  Id=2, Email = "bill@outlook.com", Name = "Billy" }, new Room{  Id=1, Description="A"}, false },

            //Scenario: Start date - occupied start, end date - occupied start
            new object[] {2, DateTime.Today.AddDays(10), DateTime.Today.AddDays(10), true, 1, 1,
                new Customer{  Id=2, Email = "bill@outlook.com", Name = "Billy" }, new Room{  Id=1, Description="A"}, false },

            //Scenario: Start date - occupied end, end date - occupied end
            new object[] {2, DateTime.Today.AddDays(20), DateTime.Today.AddDays(20), true, 1, 1,
                new Customer{  Id=2, Email = "bill@outlook.com", Name = "Billy" }, new Room{  Id=1, Description="A"}, false },

            //Scenario: Start date - Before occupied, end date - Before occupied, but later than first value
            new object[] {2, DateTime.Today.AddDays(1), DateTime.Today.AddDays(9), true, 1, 1,
            new Customer{  Id=1, Email = "joe@outlook.com", Name = "Joe"}, new Room{  Id=1, Description="A"}, true },

            //Scenario: Start date - After occupied, end date - After occpuied, but later than first value
            new object[] {2, DateTime.Today.AddDays(21), DateTime.Today.AddDays(22), true, 1, 1,
            new Customer{  Id=2, Email = "bill@outlook.com", Name = "Billy" }, new Room{  Id=1, Description="A"}, true }
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
