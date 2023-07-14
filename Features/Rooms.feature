Feature: Rooms

Background: 	
	Given User is authenticated as administrator

Scenario: Get All Rooms
	Given I send GET all rooms request
	Then I validate rooms are returned

Scenario: Get a Newest Room
	Given I send GET all rooms request
	When I get a highest room id number
	Then I send GET a room request
	And I validate room is returned

Scenario: Create a New Room
	Given I create a new room
	| roomName | type   | accessible | image                                                                        | description             | roomPrice |
	| 113      | Double | true       | https://www.amenitiz.com/wp-content/uploads/2022/10/dervr7mcawpygipdzqvv.jpg | Fancy Room, believe me! | 113       |
	And I add room features
	| features     |
	| Radio        |
	| TV           |
	| WiFi         |
	| Refreshments |
	When I send POST room request
	Then I validate room is created and available

Scenario: Room CRUD
	Given I create a new room
	| roomName | type   | accessible | image                                                                                               | description                         | roomPrice |
	| 199      | Family | true       | https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQSU6tW7S1K4GRmnhzZD8wMlVGOETgK1odI0w&usqp=CAU | A perfect suite for a modern family | 999       |
	And I add room features
	| features     |
	| Radio        |
	| TV           |
	| WiFi         |
	| Refreshments |
	| Safe         |
	When I send POST room request
	Then I validate room is created and available
	And I update the room
	| roomName | type  | accessible | image                                                          | description                         | roomPrice |
	| 199      | Suite | true       | https://www.designingbuildings.co.uk/w/images/d/db/Bedroom.jpg | A perfect suite for a modern family | 888       |
	And I add updated room features
	| features     |
	| Views        |
	When I send PUT room request
	Then I validate room is updated and available
	And I delete the updated room
	And veirify status code is 202
