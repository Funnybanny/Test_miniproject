Feature: CreateBookingFeature
	This feature tries to create a booking for a given period of time.

Scenario: Start and end dates are before the room is occupied
	Given Start date is before occupancy
	And End date is before occupancy
	When Booking is created
	Then Booking is valid

Scenario: Start and end dates are after the room is occupied
	Given Start date is after occupancy
	And End date is after occupancy
	When Booking is created
	Then Booking is valid

Scenario: Start date is before and end date is after the room is occupied
	Given Start date is before occupancy
	And End date is after occupancy
	When Booking is created
	Then Booking is invalid

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

Scenario: Start and end dates are during when the room is occupied
	Given Start date is during occupancy
	And End date is during occupancy
	When Booking is created
	Then Booking is invalid

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

Scenario: Start date is in the past
	Given Start date is in Past
	When Booking is created
	Then Booking Throws Exception