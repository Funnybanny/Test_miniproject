using System;
using System.Collections.Generic;
using HotelBooking.BusinessLogic;
using HotelBooking.Controllers;
using HotelBooking.Models;
using HotelBooking.UnitTests.DataGenerator;
using HotelBooking.UnitTests.Fakes;
using HotelBookingStartupProject.Models;
using Moq;
using Xunit;

namespace HotelBooking.UnitTests
{
    public class BookingManagerTests
    {
        private IBookingManager bookingManager;
        private Mock<IRepository<Room>> fakeRoomRepository;
        private Mock<IRepository<Booking>> fakeBookingRepository;

        public BookingManagerTests(){

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


            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);

            List<Booking> bookings = new List<Booking>
            {
                new Booking {Id=1, StartDate=start, EndDate=end, IsActive=true, CustomerId=1, RoomId=1, Customer=customers[0], Room=rooms[0]},
                new Booking {Id=2, StartDate=start, EndDate=end, IsActive=true, CustomerId=2, RoomId=2, Customer=customers[1], Room=rooms[1]}
            };

            fakeBookingRepository = new Mock<IRepository<Booking>>();
            fakeRoomRepository = new Mock<IRepository<Room>>(); 

            fakeBookingRepository.Setup(x => x.GetAll()).Returns(bookings);
            fakeRoomRepository.Setup(x => x.GetAll()).Returns(rooms);

            fakeBookingRepository.Setup(x => x.Get(It.Is<int>(id => id > 0 && id < 3))).Returns(bookings[1]);
            fakeRoomRepository.Setup(x => x.Get(It.Is<int>(id => id > 0 && id < 3))).Returns(rooms[1]);

            bookingManager = new BookingManager(fakeBookingRepository.Object, fakeRoomRepository.Object);

            bookingManager.CreateBooking(bookings[0]);
        }

        [Theory]
        [ClassData(typeof(CreateBookingExceptionDataGenerator))]
        public void CreateBookingTest_ThrowsArgumentException(int Id, DateTime StartDate, DateTime EndDate, bool IsActive,int CustomerId, int RoomId, Customer Customer, Room Room)
        {
            var book = new Booking { Id = Id, StartDate = StartDate, EndDate = EndDate, IsActive = IsActive, CustomerId = CustomerId, RoomId = RoomId, Customer = Customer, Room = Room };
            Exception ex = Assert.Throws<ArgumentException>(() => bookingManager.CreateBooking(book));
            Assert.Equal(String.Format("The start date cannot be in the past or later than the end date."), ex.Message);
        }

        [Theory]
        [ClassData(typeof(CreateBookingScenarioDataGenerator))]
        public void CreateBookingTest(int Id, DateTime StartDate, DateTime EndDate, bool IsActive, int CustomerId, int RoomId, Customer Customer, Room Room, bool result)
        {
            var book = new Booking { Id = Id, StartDate = StartDate, EndDate = EndDate, IsActive = IsActive, CustomerId = CustomerId, RoomId = RoomId, Customer = Customer, Room = Room };
            Assert.Equal(result, bookingManager.CreateBooking(book));
        }

        [Fact]
        public void FindAvailableRoom_StartDateNotInTheFuture_ThrowsArgumentException()
        {
            DateTime date = DateTime.Today;
            Assert.Throws<ArgumentException>(() => bookingManager.FindAvailableRoom(date, date));
        }

        [Fact]
        public void FindAvailableRoom_RoomAvailable_RoomIdNotMinusOne()
        {
            // Arrange
            DateTime date = DateTime.Today.AddDays(1);
            // Act
            int roomId = bookingManager.FindAvailableRoom(date, date);
            // Assert
            Assert.NotEqual(-1, roomId);
        }

        [Fact]
        public void GetFullyOccupiedDates_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => bookingManager.GetFullyOccupiedDates(DateTime.Today.AddDays(1), DateTime.Today));
        }

        /*[Theory]
        [ClassData(typeof(BookingDataGenerator))]
        public void GetFullyOccupiedDatesTest(DateTime start, DateTime end)
        {
            var list = bookingManager.GetFullyOccupiedDates(start, end);
            Assert.True(list.Exists(x => x <= end && x >= start));
            //foreach (var d in list)
            //{
            //    if (d <= end && d >= start)
            //    {

            //    }
            //}
        }*/
    }
}
