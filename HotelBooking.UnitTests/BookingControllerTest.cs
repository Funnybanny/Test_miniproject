using System;
using System.Collections.Generic;
using HotelBooking.BusinessLogic;
using HotelBooking.Controllers;
using HotelBooking.Models;
using HotelBooking.UnitTests.Fakes;
using HotelBookingStartupProject.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HotelBooking.UnitTests
{
    public class BookingControllerTest
    {
        private BookingsController controller;
        private Mock<IRepository<Booking>> fakeBookingRepository;
        private Mock<IRepository<Customer>> fakeCustomerRepository;
        private Mock<IRepository<Room>> fakeRoomRepository;
        private BookingManager bookingmanager;
        private BookingViewModel bookingmodel;

        public BookingControllerTest()
        { 
            fakeBookingRepository = new Mock<IRepository<Booking>>();
            fakeCustomerRepository = new Mock<IRepository<Customer>>();
            fakeRoomRepository = new Mock<IRepository<Room>>();            

            var rooms = new List<Room>
            {
                new Room { Id=1, Description="A" },
                new Room { Id=2, Description="B" },
            };
            var customers = new List<Customer>
            {
                new Customer { Id=1, Email = "joe@outlook.com", Name = "Joe"},
                new Customer { Id=2, Email = "bill@outlook.com", Name = "Billy" },
            };
            List<Booking> bookings = new List<Booking>
            {
                new Booking {Id=1, StartDate=DateTime.Now, EndDate=DateTime.Now, IsActive=true, CustomerId=1, RoomId=1, Customer=customers[0], Room=rooms[0]},
                new Booking {Id=2, StartDate=DateTime.Now, EndDate=DateTime.Now, IsActive=true, CustomerId=2, RoomId=2, Customer=customers[1], Room=rooms[1]}
            };
            

            // Implement fake GetAll() method.
            fakeBookingRepository.Setup(x => x.GetAll()).Returns(bookings);
            fakeCustomerRepository.Setup(x => x.GetAll()).Returns(customers);
            fakeRoomRepository.Setup(x => x.GetAll()).Returns(rooms);

            // Implement fake Get() method.
            //fakeBookingRepository.Setup(x => x.Get(2)).Returns(bookings[1]);


            // Alternative setup with argument matchers:

            // Any integer:
            //roomRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(rooms[1]);

            // Integers from 1 to 2 (using a predicate)
            // If the fake Get is called with an another argument value than 1 or 2,
            // it returns null, which corresponds to the behavior of the real
            // repository's Get method.
            fakeBookingRepository.Setup(x => x.Get(It.Is<int>(id => id > 0 && id < 3))).Returns(bookings[1]);
            fakeCustomerRepository.Setup(x => x.Get(It.Is<int>(id => id > 0 && id < 3))).Returns(customers[1]);
            fakeRoomRepository.Setup(x => x.Get(It.Is<int>(id => id > 0 && id < 3))).Returns(rooms[1]);

            // Integers from 1 to 2 (using a range)
            //roomRepository.Setup(x => x.Get(It.IsInRange<int>(1, 2, Range.Inclusive))).Returns(rooms[1]);

            // Create BookingController
            bookingmanager = new BookingManager(fakeBookingRepository.Object, fakeRoomRepository.Object);
            bookingmodel = new BookingViewModel(fakeBookingRepository.Object, bookingmanager);
            controller = new BookingsController(fakeBookingRepository.Object, fakeRoomRepository.Object, fakeCustomerRepository.Object, bookingmanager, bookingmodel);
        }

        [Fact]
        public void Index_ReturnsViewResultWithCorrectListOfBookings()
        {
            // Act
            //var result = controller.Index(null) as ViewResult;

            // Assert
           // Assert.NotNull(result);

            // Act
            var result = controller.Index(null) as ViewResult;
            var bookingviewmodel = result.Model as BookingViewModel;
            var listofbookings = bookingviewmodel.Bookings as IList<Booking>;
            var count = listofbookings.Count;

            // Assert
            Assert.Equal(2, count);
        }

        [Fact]
        public void Details_BookingExists_ReturnsViewResultWithCustomer()
        {
            // Act
            var result = controller.Details(2) as ViewResult;
            var booking = result.Model as Booking;
            var bookingId = booking.Id;

            // Assert
            Assert.InRange<int>(bookingId, 1, 2);
        }

        [Fact]
        public void DeleteConfirmed_WhenIdIsLargerThanZero_RemoveIsCalled()
        {
            // Act
            controller.DeleteConfirmed(1);

            // Assert against the mock object
            fakeBookingRepository.Verify(x => x.Remove(It.IsAny<int>()));
        }

        [Fact]
        public void DeleteConfirmed_WhenIdIsLessThanOne_RemoveIsNotCalled()
        {
            // Act
            controller.DeleteConfirmed(0);

            // Assert against the mock object
            fakeBookingRepository.Verify(x => x.Remove(It.IsAny<int>()), Times.Never());
        }

        [Fact]
        public void DeleteConfirmed_WhenIdIsLargerThanTwo_RemoveThrowsException()
        {
            // Instruct the fake Remove method to throw an InvalidOperationException, if a room id that
            // does not exist in the repository is passed as a parameter. This behavior corresponds to
            // the behavior of the real repoository's Remove method.
            fakeBookingRepository.Setup(x =>
                    x.Remove(It.Is<int>(id => id < 1 || id > 2))).Throws<InvalidOperationException>();

            // Assert
            Assert.Throws<InvalidOperationException>(() => controller.DeleteConfirmed(3));

            // Assert against the mock object
            fakeBookingRepository.Verify(x => x.Remove(It.IsAny<int>()));
        }


    }
}
