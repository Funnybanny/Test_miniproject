Feature: SpecFlowFeatureP
	This feature tries to create a booking for a given period of time.

Scenario: Start date is in the past
	Given Start date is before occupancy
	When Booking is created
	Then Booking is invalid