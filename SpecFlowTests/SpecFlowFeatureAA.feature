Feature: SpecFlowFeatureAA
	This feature tries to create a booking for a given period of time.

Scenario: Start and end dates are after the room is occupied
	Given Start date is after occupancy
	And End date is after occupancy
	When Booking is created
	Then Booking is valid