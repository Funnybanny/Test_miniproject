using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace SpecFlowTests
{
    [Binding]
    public class SpecFlowFeatureBASteps
    {
        private CreateBookingFakeResources fakeResources = new CreateBookingFakeResources();

        [Then(@"Booking is invalid")]
        public void ThenBookingIsInvalid()
        {
            Assert.IsFalse(GlobalCreateBookingVariables.result);
        }
    }
}
