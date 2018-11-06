using System;
using HotelBooking.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace SpecFlowTests
{
    [Binding]
    public class CreateBookingFeatureSteps
    {
        CreateBookingFakeResources fakeResources = new CreateBookingFakeResources();


        [Given(@"Start date is before occupancy")]
        public void GivenStartDateIsBeforeOccupancy()
        {
            fakeResources.StartDate = DateTime.Today.AddDays(1);
        }
        
        [Given(@"End date is before occupancy")]
        public void GivenEndDateIsBeforeOccupancy()
        {
            fakeResources.EndDate = DateTime.Today.AddDays(9);
        }
        
        [Given(@"Start date is after occupancy")]
        public void GivenStartDateIsAfterOccupancy()
        {
            fakeResources.StartDate = DateTime.Today.AddDays(21);
        }
        
        [Given(@"End date is after occupancy")]
        public void GivenEndDateIsAfterOccupancy()
        {
            fakeResources.EndDate = DateTime.Today.AddDays(22);
        }
        
        [Given(@"Start date is at the start of occupancy")]
        public void GivenStartDateIsAtTheStartOfOccupancy()
        {
            fakeResources.StartDate = DateTime.Today.AddDays(10);
        }
        
        [Given(@"Start date is at the end of occupancy")]
        public void GivenStartDateIsAtTheEndOfOccupancy()
        {
            fakeResources.StartDate = DateTime.Today.AddDays(20);
        }
        
        [Given(@"Start date is during occupancy")]
        public void GivenStartDateIsDuringOccupancy()
        {
            fakeResources.StartDate = DateTime.Today.AddDays(10);
        }
        
        [Given(@"End date is during occupancy")]
        public void GivenEndDateIsDuringOccupancy()
        {
            fakeResources.EndDate = DateTime.Today.AddDays(20);
        }
        
        [Given(@"End date is at the start of occupancy")]
        public void GivenEndDateIsAtTheStartOfOccupancy()
        {
            fakeResources.EndDate = DateTime.Today.AddDays(10);
        }
        
        [Given(@"End date is at the end of occupancy")]
        public void GivenEndDateIsAtTheEndOfOccupancy()
        {
            fakeResources.EndDate = DateTime.Today.AddDays(20);
        }

        [Given(@"Start date is in Past")]
        public void GivenStartDateIsInPast()
        {
            fakeResources.StartDate = DateTime.Today.AddDays(-5);
        }


        [When(@"Booking is created")]
        public void WhenBookingIsCreated()
        {
            var booking = new Booking()
            {
                Id = 1,
                EndDate = fakeResources.EndDate,
                StartDate = fakeResources.StartDate,
                CustomerId = 1,
                Customer = new Customer() { Id = 1, Email = "some@email.com", Name = "Joe"},
                IsActive = true,
                Room =  new Room() {Id = 1, Description = "A"},
                RoomId = 1
            };
            try
            {
                fakeResources.result = fakeResources.bookingManager.CreateBooking(booking);
            }
            catch (ArgumentException e)
            {
                fakeResources.ex = e;
            }
        }
        
        [Then(@"Booking is valid")]
        public void ThenBookingIsValid()
        {
            Assert.IsTrue(fakeResources.result);
        }
        
        [Then(@"Booking is invalid")]
        public void ThenBookingIsInvalid()
        {
            Assert.IsFalse(fakeResources.result);
        }

        [Then(@"Booking Throws Exception")]
        public void ThenBookingThrowsException()
        {
            Assert.AreEqual(String.Format("The start date cannot be in the past or later than the end date."), fakeResources.ex.Message);
        }

    }
}
