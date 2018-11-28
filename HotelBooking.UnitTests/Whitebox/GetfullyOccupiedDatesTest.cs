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

        private Mock<IRepository<Booking>> fake2BookingRepository;
        private BookingManager booking2manager;
        private BookingViewModel booking2model;
        private BookingsController controller2;

        public GetfullyOccupiedDatesTest()
        {
            fakeBookingRepository = new Mock<IRepository<Booking>>();
            fake2BookingRepository = new Mock<IRepository<Booking>>();
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
            List<Booking> bookingstwo = new List<Booking>
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
                },
                new Booking
                {
                    Id = 2, StartDate = DateTime.Today.AddDays(1), EndDate = DateTime.Today.AddDays(4), IsActive = false, CustomerId = 2,
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
            fake2BookingRepository.Setup(x => x.GetAll()).Returns(bookingstwo);

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

            booking2manager = new BookingManager(fake2BookingRepository.Object, fakeRoomRepository.Object);
            booking2model = new BookingViewModel(fake2BookingRepository.Object, booking2manager);
            controller2 = new BookingsController(fake2BookingRepository.Object, fakeRoomRepository.Object,
                fakeCustomerRepository.Object, booking2manager, booking2model);
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

        /* Condition Coverage
         * T T T
         * T=True
         * F=False
         * N=Doesn't matter
         * Booking is active
         * Start date is after or at given start date
         * End date is before or at given end date
         * Expected result: Two datetimes
         * */
        /* Condition Coverage
        * F N N
        * T=True
        * F=False
        * N=Doesn't matter
        * Booking is inactive
        * Expected result: Two datetimes
        * */
        [Fact]
        public void ConditionTTT()
        {
            DateTime startdate = DateTime.Today;
            DateTime enddate = DateTime.Today.AddDays(10);
            List<DateTime> fullyOccupiedDates = booking2manager.GetFullyOccupiedDates(startdate, enddate);
            Assert.Equal(2, fullyOccupiedDates.Count);
        }
    }
} 
