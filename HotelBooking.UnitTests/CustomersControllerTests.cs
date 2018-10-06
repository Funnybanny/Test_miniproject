using System;
using System.Collections.Generic;
using HotelBooking.Controllers;
using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HotelBooking.UnitTests
{
    public class CustomersControllerTests
    {
        private CustomersController controller;
        private Mock<IRepository<Customer>> fakeCustomerRepository;

        public CustomersControllerTests()
        {
            var customers = new List<Customer>
            {
                new Customer { Id=1, Email = "joe@outlook.com", Name = "Joe"},
                new Customer { Id=2, Email = "bill@outlook.com", Name = "Billy" },
            };

            // Create fake RoomRepository. 
            fakeCustomerRepository = new Mock<IRepository<Customer>>();

            // Implement fake GetAll() method.
            fakeCustomerRepository.Setup(x => x.GetAll()).Returns(customers);

            // Integers from 1 to 2 (using a predicate)
            // If the fake Get is called with an another argument value than 1 or 2,
            // it returns null, which corresponds to the behavior of the real
            // repository's Get method.
            fakeCustomerRepository.Setup(x => x.Get(It.Is<int>(id => id > 0 && id < 3))).Returns(customers[1]);

            // Create CustomersController
            controller = new CustomersController(fakeCustomerRepository.Object);
        }

        [Fact]
        public void Index_ReturnsViewResultWithCorrectListOfCustomers()
        {
            // Act
            var result = controller.Index() as ViewResult;
            var customersList = result.Model as IList<Customer>;
            var noOfCustomers = customersList.Count;

            // Assert
            Assert.Equal(2, noOfCustomers);
        }
    }
}
