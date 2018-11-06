using System;
using TechTalk.SpecFlow;

namespace SpecFlowTests
{
    [Binding]
    public class SpecFlowFeaturesBOSteps
    {
        private CreateBookingFakeResources fakeResources = new CreateBookingFakeResources();

        [Given(@"End date is at the start of occupancy")]
        public void GivenEndDateIsAtTheStartOfOccupancy()
        {
            GlobalCreateBookingVariables.EndDate = DateTime.Today.AddDays(10);
        }
        
        [Given(@"End date is at the end of occupancy")]
        public void GivenEndDateIsAtTheEndOfOccupancy()
        {
            GlobalCreateBookingVariables.EndDate = DateTime.Today.AddDays(20);
        }
        
    }
}
