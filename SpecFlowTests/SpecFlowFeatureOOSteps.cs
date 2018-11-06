using System;
using TechTalk.SpecFlow;

namespace SpecFlowTests
{
    [Binding]
    public class SpecFlowFeatureOOSteps
    {
        [Given(@"Start date is during occupancy")]
        public void GivenStartDateIsDuringOccupancy()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"End date is during occupancy")]
        public void GivenEndDateIsDuringOccupancy()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
