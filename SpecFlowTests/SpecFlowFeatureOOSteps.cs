using System;
using TechTalk.SpecFlow;

namespace SpecFlowTests
{
    [Binding]
    public class SpecFlowFeatureOOSteps
    {
        private CreateBookingFakeResources fakeResources = new CreateBookingFakeResources();

        [Given(@"Start date is during occupancy")]
        public void GivenStartDateIsDuringOccupancy()
        {
            GlobalCreateBookingVariables.StartDate = DateTime.Today.AddDays(10);
        }
        
        [Given(@"End date is during occupancy")]
        public void GivenEndDateIsDuringOccupancy()
        {
            GlobalCreateBookingVariables.EndDate = DateTime.Today.AddDays(20);
        }
    }
}
