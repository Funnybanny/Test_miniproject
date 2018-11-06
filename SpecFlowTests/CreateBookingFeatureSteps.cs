using System;
using TechTalk.SpecFlow;

namespace SpecFlowTests
{
    [Binding]
    public class CreateBookingFeatureSteps
    {
        [Given(@"Start date is before occupancy")]
        public void GivenStartDateIsBeforeOccupancy()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"End date is before occupancy")]
        public void GivenEndDateIsBeforeOccupancy()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"Booking is created")]
        public void WhenBookingIsCreated()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"Booking is valid")]
        public void ThenBookingIsValid()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
