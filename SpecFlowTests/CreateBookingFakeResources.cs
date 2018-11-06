using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.BusinessLogic;
using HotelBooking.Models;
using Moq;

namespace SpecFlowTests
{

    class CreateBookingFakeResources
    {
        public IBookingManager bookingManager;
        private Mock<IRepository<Room>> fakeRoomRepository;
        private Mock<IRepository<Booking>> fakeBookingRepository;

        public DateTime StartDate;
        public DateTime EndDate;
        public bool result;
        public ArgumentException ex;


        public CreateBookingFakeResources()
        {
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
    }
}
