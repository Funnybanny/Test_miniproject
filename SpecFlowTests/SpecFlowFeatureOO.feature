Feature: SpecFlowFeatureOO
	This feature tries to create a booking for a given period of time.

Scenario: Start and end dates are during when the room is occupied
	Given Start date is during occupancy
	And End date is during occupancy
	When Booking is created
	Then Booking is invalid