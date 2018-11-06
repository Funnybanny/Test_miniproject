Feature: SpecFlowFeaturesBO
	This feature tries to create a booking for a given period of time.

Scenario: Start date is before and end date is at the start of occupancy
	Given Start date is before occupancy
	And End date is at the start of occupancy
	When Booking is created
	Then Booking is invalid

Scenario: Start date is before and end date is at the end of occupancy
	Given Start date is before occupancy
	And End date is at the end of occupancy
	When Booking is created
	Then Booking is invalid