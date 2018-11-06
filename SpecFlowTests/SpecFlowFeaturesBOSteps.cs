using System;
using TechTalk.SpecFlow;

namespace SpecFlowTests
{
    [Binding]
    public class SpecFlowFeaturesBOSteps
    {
        [Given(@"End date is at the start of occupancy")]
        public void GivenEndDateIsAtTheStartOfOccupancy()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"End date is at the end of occupancy")]
        public void GivenEndDateIsAtTheEndOfOccupancy()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
