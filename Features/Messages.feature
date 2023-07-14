Feature: Messages

Background: 	
	Given Message service is UP and running

Scenario: Get All Messages
	Given I send GET message request
	Then I validate messages are returned

Scenario: Send a New Message And Verify Message Is Received 
	Given I create a new message
	| name            | email                  | phone        | subject               | description                           |
	| Automation User | testing@automation.com | 01111 619211 | I want to book a room | Do you have a room avlialable for me? |
	When I send POST message request
	Then I verify message is received


Scenario: Send Messages And Verify Messages Are Received 
	Given I create messages and I verify status code is <201>
	| name               | email                  | phone        | subject               | description                                                                                                                 |
	| Automation User #1 | testing@automation.com | 01111 619211 | I want to book a room | Do you have a room avlialable for me?                                                                                       |
	| Automation User #2 | testing@automation.com | 01112 619212 | I want to book a room | Do you have a room for me?                                                                                                  |
	| Automation User #3 | testing@automation.com | 01113 619213 | I want to book a room | Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. |
	| Automation User #4 | testing@automation.com | 01114 619214 | I want to book a room | 1238886846515564684613123121131121318                                                                                       |
	| Automation User #5 | testing@automation.com | 01115 619215 | I want to book a room | 8787987978                 879779787  adskalkdsa           kdsalkdakslkss                                                   |
	| Automation User #6 | testing@automation.com | 01116 619216 | I want to book a room | veryLongWordThatMightBreakSomeUIComponentIfSometingIsNotCreatedAsItSHould                                                   |
	| Automation User #7 | testing@automation.com | 01117 619217 | I want to book a room | !@#$%^&*()__+++ 643763467643 aaaaaaaaaaa                                                                                    |
	| Automation User #8 | testing@automation.com | 01118 619218 | I want to book a room | something to talk about aaa 'and 1=1                                                                                        |