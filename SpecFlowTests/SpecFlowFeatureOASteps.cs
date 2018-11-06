using System;
using TechTalk.SpecFlow;

namespace SpecFlowTests
{
    [Binding]
    public class SpecFlowFeatureOASteps
    {
        [Given(@"Start date is at the start of occupancy")]
        public void GivenStartDateIsAtTheStartOfOccupancy()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"Start date is at the end of occupancy")]
        public void GivenStartDateIsAtTheEndOfOccupancy()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
