using System;
using TechTalk.SpecFlow;

namespace SpecFlowTests
{
    [Binding]
    public class SpecFlowFeatureAASteps
    {
        [Given(@"Start date is after occupancy")]
        public void GivenStartDateIsAfterOccupancy()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"End date is after occupancy")]
        public void GivenEndDateIsAfterOccupancy()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
