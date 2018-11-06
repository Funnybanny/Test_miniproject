Feature: SpecFlowFeatureOA
	This feature tries to create a booking for a given period of time.

Scenario: Start date is at the start of when and end date is after the room is occupied
	Given Start date is at the start of occupancy
	And End date is after occupancy
	When Booking is created
	Then Booking is invalid

Scenario: Start date is at the end of when and end date is after the room is occupied
	Given Start date is at the end of occupancy
	And End date is after occupancy
	When Booking is created
	Then Booking is invalid