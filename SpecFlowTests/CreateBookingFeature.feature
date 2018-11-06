Feature: CreateBookingFeature
	This feature tries to create a booking for a given period of time.

Scenario: Start and end dates are before the room is occupied
	Given Start date is before occupancy
	And End date is before occupancy
	When Booking is created
	Then Booking is valid