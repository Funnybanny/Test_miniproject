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

namespace HotelBooking.UnitTests.Whitebox
{
    public class GetfullyOccupiedDatesTest
    {
        private BookingsController controller;
        private Mock<IRepository<Booking>> fakeBookingRepository;
        private Mock<IRepository<Customer>> fakeCustomerRepository;
        private Mock<IRepository<Room>> fakeRoomRepository;
        private BookingManager bookingmanager;
        private BookingViewModel bookingmodel;

        private Mock<IRepository<Booking>> EmptyfakeBookingRepository;
        private BookingManager Emptybookingmanager;
        private BookingViewModel Emptybookingmodel;
        private BookingsController Emptycontroller;

        public GetfullyOccupiedDatesTest()
        {
            fakeBookingRepository = new Mock<IRepository<Booking>>();
            fakeCustomerRepository = new Mock<IRepository<Customer>>();
            fakeRoomRepository = new Mock<IRepository<Room>>();

            EmptyfakeBookingRepository = new Mock<IRepository<Booking>>();

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

            List<Booking> Emptybookings = new List<Booking>
            {

            };

            // Implement fake GetAll() method.
            fakeBookingRepository.Setup(x => x.GetAll()).Returns(bookings);
            fakeCustomerRepository.Setup(x => x.GetAll()).Returns(customers);
            fakeRoomRepository.Setup(x => x.GetAll()).Returns(rooms);

            EmptyfakeBookingRepository.Setup(x => x.GetAll()).Returns(Emptybookings);

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

            Emptybookingmanager = new BookingManager(EmptyfakeBookingRepository.Object, fakeRoomRepository.Object);
            Emptybookingmodel = new BookingViewModel(EmptyfakeBookingRepository.Object, Emptybookingmanager);
            Emptycontroller = new BookingsController(EmptyfakeBookingRepository.Object, fakeRoomRepository.Object,
                fakeCustomerRepository.Object, Emptybookingmanager, Emptybookingmodel);
        }

        /*  Edge coverage 
         * On diagram Node 1
         * if else
         * node 3
         * node 8
         */
        [Fact]
        public void Edge1()
        {
            var returnValue = bookingmanager.GetFullyOccupiedDates(DateTime.Today.AddDays(1), DateTime.Today.AddDays(3));
            Assert.Equal(2, returnValue.Count);
        }

        /*  Edge coverage 
        * On diagram Node 2
        * if then
        * 
        *  Loop Coverage
        * node 5
        * iteration = 0
        */
        [Fact]
        public void Edge2()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => bookingmanager.GetFullyOccupiedDates(DateTime.Today.AddDays(3), DateTime.Today));
            Assert.Equal(String.Format("The start date cannot be later than the end date."), ex.Message);
        }

        /*  Edge coverage 
        * On diagram Node 4
        * if else
        */
        [Fact]
        public void Edge4()
        {
            var temp = new List<DateTime>();
            var returnValue = Emptybookingmanager.GetFullyOccupiedDates(DateTime.Today.AddDays(1), DateTime.Today.AddDays(3));
            Assert.Equal(temp, returnValue);

        }

        /*  Edge coverage 
        * On diagram Node 7
        * if else
        */
        [Fact]
        public void Edge7()
        {
            var returnValue = bookingmanager.GetFullyOccupiedDates(DateTime.Today.AddDays(10), DateTime.Today.AddDays(13));
            Assert.Empty(returnValue);
        }

        /*  Loop Coverage
         * node 5
         * iteration = 1
         */
         [Fact]
         public void Loop1()
         {
            var returnValue = bookingmanager.GetFullyOccupiedDates(DateTime.Today, DateTime.Today);
            Assert.Empty(returnValue);
         }
        
    }
} 
