using System;
using System.Collections.Generic;
using HotelBooking.BusinessLogic;
using HotelBooking.Models;
using HotelBooking.UnitTests.Fakes;
using Moq;
using Xunit;

namespace HotelBooking.UnitTests
{
    public class BookingManagerTests
    {
        private IBookingManager bookingManager;
        private Mock<IRepository<Booking>> fakeBookingRepository;
        private Mock<IRepository<Room>> fakeRoomRepository;

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
            List<Booking> bookings = new List<Booking>
            {
                new Booking {Id=1, StartDate=DateTime.Now, EndDate=DateTime.Now, IsActive=true, CustomerId=1, RoomId=1, Customer=customers[0], Room=rooms[0]},
                new Booking {Id=2, StartDate=DateTime.Now, EndDate=DateTime.Now, IsActive=true, CustomerId=2, RoomId=2, Customer=customers[1], Room=rooms[1]}
            };

            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);
            fakeBookingRepository.Setup(x => x.GetAll()).Returns(bookings);
            fakeRoomRepository.Setup(x => x.GetAll()).Returns(rooms);

            fakeBookingRepository.Setup(x => x.Get(It.Is<int>(id => id > 0 && id < 3))).Returns(bookings[1]);
            fakeRoomRepository.Setup(x => x.Get(It.Is<int>(id => id > 0 && id < 3))).Returns(rooms[1]);

            bookingManager = new BookingManager(fakeBookingRepository.Object, fakeRoomRepository.Object);
        }

        public IEnumerable<object[]> GetBookings()
        {
            yield return new object[] { 1, DateTime.Now, DateTime.Now, true, 1, 1, customers[0], rooms[0] };
            yield return new object[] { 2, DateTime.Now, DateTime.Now, true, 2, 2, customers[1], rooms[1] }
        }

        [Theory]
        []
        public void CreateBookingTest()
        {
            bookingManager
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
    }
}
