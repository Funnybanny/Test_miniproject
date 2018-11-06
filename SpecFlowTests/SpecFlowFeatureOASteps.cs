using System;
using TechTalk.SpecFlow;

namespace SpecFlowTests
{
    [Binding]
    public class SpecFlowFeatureOASteps
    {
        private CreateBookingFakeResources fakeResources = new CreateBookingFakeResources();

        [Given(@"Start date is at the start of occupancy")]
        public void GivenStartDateIsAtTheStartOfOccupancy()
        {
            GlobalCreateBookingVariables.StartDate = DateTime.Today.AddDays(10);
        }
        
        [Given(@"Start date is at the end of occupancy")]
        public void GivenStartDateIsAtTheEndOfOccupancy()
        {
            GlobalCreateBookingVariables.StartDate = DateTime.Today.AddDays(20);
        }
    }
}
