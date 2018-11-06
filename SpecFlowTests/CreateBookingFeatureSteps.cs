using HotelBooking.BusinessLogic;
using HotelBooking.Models;
using System;
using TechTalk.SpecFlow;
using Moq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Booking = HotelBooking.Models.Booking;

namespace SpecFlowTests
{
    [Binding]
    public class CreateBookingFeatureSteps
    {
        private CreateBookingFakeResources fakeResources = new CreateBookingFakeResources();

        [Given(@"Start date is before occupancy")]
        public void GivenStartDateIsBeforeOccupancy()
        {
            GlobalCreateBookingVariables.StartDate = DateTime.Today.AddDays(1);
        }
        
        [Given(@"End date is before occupancy")]
        public void GivenEndDateIsBeforeOccupancy()
        {
            GlobalCreateBookingVariables.EndDate = DateTime.Today.AddDays(9);
        }
        
        [When(@"Booking is created")]
        public void WhenBookingIsCreated()
        {
            var book = new Booking()
            {
                Id = 1,
                StartDate = GlobalCreateBookingVariables.StartDate,
                EndDate = GlobalCreateBookingVariables.EndDate,
                Customer = new Customer(){Id = 1, Email = "some@email.com", Name = "Name"},
                CustomerId = 1,
                IsActive = true,
                Room = new Room() { Id = 1, Description = "A"},
                RoomId = 1
            };
            GlobalCreateBookingVariables.result = fakeResources.bookingManager.CreateBooking(book);
        }
        
        [Then(@"Booking is valid")]
        public void ThenBookingIsValid()
        {
            Assert.IsTrue(GlobalCreateBookingVariables.result);
        }
    }
}
