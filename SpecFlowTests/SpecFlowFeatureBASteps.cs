using System;
using TechTalk.SpecFlow;

namespace SpecFlowTests
{
    [Binding]
    public class SpecFlowFeatureBASteps
    {
        [Then(@"Booking is invalid")]
        public void ThenBookingIsInvalid()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
