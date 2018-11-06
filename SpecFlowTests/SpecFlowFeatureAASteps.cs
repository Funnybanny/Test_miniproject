using System;
using TechTalk.SpecFlow;

namespace SpecFlowTests
{
    [Binding]
    public class SpecFlowFeatureAASteps
    {
        private CreateBookingFakeResources fakeResources = new CreateBookingFakeResources();
        [Given(@"Start date is after occupancy")]
        public void GivenStartDateIsAfterOccupancy()
        {
            GlobalCreateBookingVariables.StartDate = DateTime.Today.AddDays(21);
        }
        
        [Given(@"End date is after occupancy")]
        public void GivenEndDateIsAfterOccupancy()
        {
            GlobalCreateBookingVariables.EndDate = DateTime.Today.AddDays(22);
        }
    }
}
