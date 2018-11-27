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


namespace WhiteBoxTests
{
    public class FindAvailableRoomTest
    {
        private BookingsController controller;
        private Mock<IRepository<Booking>> fakeBookingRepository;
        private Mock<IRepository<Customer>> fakeCustomerRepository;
        private Mock<IRepository<Room>> fakeRoomRepository;
        private BookingManager bookingmanager;
        private BookingManager loopmanager;
        private BookingViewModel bookingmodel;

        private BookingsController controllerLoop;
        private BookingManager bookingmanagerLoop;
        private BookingManager loopmanagerLoop;
        private BookingViewModel bookingmodelLoop;
        private Mock<IRepository<Booking>> fakeBookingRepositoryLoop;
        private Mock<IRepository<Room>> fakeRoomRepositoryLoop;

        public FindAvailableRoomTest()
        {
            fakeBookingRepository = new Mock<IRepository<Booking>>();
            fakeCustomerRepository = new Mock<IRepository<Customer>>();
            fakeRoomRepository = new Mock<IRepository<Room>>();

            fakeRoomRepositoryLoop = new Mock<IRepository<Room>>();
            fakeBookingRepositoryLoop = new Mock<IRepository<Booking>>();

            var rooms = new List<Room>
            {
                new Room {Id = 1, Description = "A"},
                new Room {Id = 2, Description = "B"},
            };

            var customers = new List<Customer>
            {
                new Customer {Id = 1, Email = "joe@outlook.com", Name = "Joe"},
                new Customer {Id = 2, Email = "bill@outlook.com", Name = "Billy"},
            };
            List<Booking> bookings = new List<Booking>
            {
                new Booking
                {
                    Id = 1, StartDate = DateTime.Today.AddDays(2), EndDate = DateTime.Today.AddDays(3), IsActive = true, CustomerId = 1,
                    RoomId = 1, Customer = customers[0], Room = rooms[0]
                },
                new Booking
                {
                    Id = 2, StartDate = DateTime.Today.AddDays(1), EndDate = DateTime.Today.AddDays(5), IsActive = true, CustomerId = 2,
                    RoomId = 2, Customer = customers[1], Room = rooms[1]
                }
            };

            var roomsLoop = new List<Room>
            {

            };
            List<Booking> bookingsLoop = new List<Booking>
            {

            };

            // Implement fake GetAll() method.
            fakeBookingRepository.Setup(x => x.GetAll()).Returns(bookings);
            fakeCustomerRepository.Setup(x => x.GetAll()).Returns(customers);
            fakeRoomRepository.Setup(x => x.GetAll()).Returns(rooms);

            fakeRoomRepositoryLoop.Setup(x => x.GetAll()).Returns(roomsLoop);
            fakeBookingRepositoryLoop.Setup(x => x.GetAll()).Returns(bookingsLoop);
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
            controller = new BookingsController(fakeBookingRepository.Object, fakeRoomRepository.Object,
                fakeCustomerRepository.Object, bookingmanager, bookingmodel);

            bookingmanagerLoop = new BookingManager(fakeBookingRepositoryLoop.Object, fakeRoomRepositoryLoop.Object);
            bookingmodelLoop = new BookingViewModel(fakeBookingRepositoryLoop.Object, bookingmanagerLoop);
            controllerLoop = new BookingsController(fakeBookingRepositoryLoop.Object, fakeRoomRepositoryLoop.Object,
                fakeCustomerRepository.Object, bookingmanagerLoop, bookingmodelLoop);
        }

        /*  Edge coverage 
         * On diagram Node 1
         * if then
         * node 3
         * node 7
         * node 5
         *  
         *  Loop Coverage
         * node 4
         * iteration = 2
         */
        [Fact]
        public void Edge1()
        {
            var returnValue = bookingmanager.FindAvailableRoom(DateTime.Today.AddDays(4), DateTime.Today.AddDays(5));
            Assert.Equal(1, returnValue);
        }

        /*edge coverage 
         * On diagram Node 2
         * if else
         */
        [Fact]
        public void Edge2()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => bookingmanager.FindAvailableRoom(DateTime.Today.AddDays(-1), DateTime.Today));
            Assert.Equal(String.Format("The start date cannot be in the past or later than the end date."), ex.Message);
        }

        /*edge coverage 
         * On diagram Node 6
         * if else
         */
        [Fact]
        public void Edge6()
        {
            var returnValue = bookingmanager.FindAvailableRoom(DateTime.Today.AddDays(2), DateTime.Today.AddDays(3));
            Assert.Equal(-1, returnValue);
        }

        /*  Loop Coverage
         * node 4
         * iteration = 0
         */
        [Fact]
        public void Loop0()
        {
            var returnValue = bookingmanagerLoop.FindAvailableRoom(DateTime.Today.AddDays(2), DateTime.Today.AddDays(3));
            Assert.Equal(-1, returnValue);
        }
    }
}
