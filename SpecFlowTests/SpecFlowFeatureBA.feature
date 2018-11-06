Feature: SpecFlowFeatureBA
	This feature tries to create a booking for a given period of time.

	Scenario: Start date is before and end date is after the room is occupied
	Given Start date is before occupancy
	And End date is after occupancy
	When Booking is created
	Then Booking is invalid