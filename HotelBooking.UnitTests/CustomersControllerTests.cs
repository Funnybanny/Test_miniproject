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

        [Fact]
        public void Details_CustomerExists_ReturnsViewResultWithCustomer()
        {
            // Act
            var result = controller.Details(2) as ViewResult;
            var customer = result.Model as Customer;
            var customerId = customer.Id;

            // Assert
            Assert.InRange<int>(customerId, 1, 2);
        }

        [Theory]
        [ClassData(typeof(CustomerDataGenerator))]
        public void Create_ReturnsViewResultWithCustomer(int id, string email, string name )
        {
            // Act
            var customer = new Customer() {Id = id, Email = email, Name = name};
            controller.Create(customer);
            // Assert against the mock object
            fakeCustomerRepository.Verify(x => x.Add(It.Is<Customer>(z => z == customer)), Times.Once);
        }

        [Fact]
        public void DeleteConfirmed_WhenIdIsLargerThanZero_RemoveIsCalled()
        {
            // Act
            controller.DeleteConfirmed(1);

            // Assert against the mock object
            fakeCustomerRepository.Verify(x => x.Remove(It.IsAny<int>()));
        }

        [Fact]
        public void DeleteConfirmed_WhenIdIsLessThanOne_RemoveIsNotCalled()
        {
            // Act
            controller.DeleteConfirmed(0);

            // Assert against the mock object
            fakeCustomerRepository.Verify(x => x.Remove(It.IsAny<int>()), Times.Never());
        }

        [Fact]
        public void DeleteConfirmed_WhenIdIsLargerThanTwo_RemoveThrowsException()
        {
            // Instruct the fake Remove method to throw an InvalidOperationException, if a customer with id that
            // does not exist in the repository is passed as a parameter.
            fakeCustomerRepository.Setup(x =>
                x.Remove(It.Is<int>(id => id < 1 || id > 2))).Throws<InvalidOperationException>();

            // Assert
            Assert.Throws<InvalidOperationException>(() => controller.DeleteConfirmed(3));

            // Assert against the mock object
            fakeCustomerRepository.Verify(x => x.Remove(It.IsAny<int>()));
        }
    }
}
