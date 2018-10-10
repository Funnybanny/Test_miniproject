using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HotelBooking.Models;

namespace HotelBooking.UnitTests
{
    public class CustomerDataGenerator : IEnumerable<Customer>
    {

        private readonly List<Customer> _customers = new List<Customer>
        {
            new Customer { Id=1, Email = "joe@outlook.com", Name = "Joe"},
            new Customer { Id=2, Email = "bill@outlook.com", Name = "Billy" },
        };


        public IEnumerator<Customer> GetEnumerator()
        {
            return _customers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
