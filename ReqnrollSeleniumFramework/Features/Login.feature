Feature: Test scenarios for Login functionality

As a user, login scenarios with valid, invalid and missing credentials

# ================= Login with valid input =================
@Login @ValidInput
Scenario: TC_001 Check login is successful with valid credentials
	Given user is on login page
	When user enters valid credentials
	Then the user is navigated to the Profile page

# ================= Login with missing input =================
@Login @MissingInput
Scenario Outline: TC_0012 Check login with missing and special characters
	Given user is on login page
	When user enters  "<email>" and "<password>"
	And click on Login Button
    Then an error message "<errorMessage>" should be displayed

    Examples:
      | email          | password | errorMessage                                |
      |                | project  | Please enter a valid email address          |
      | user@gmail.com |          | Password must be at least 6 characters      |
      | %()$@&^''      | %'$5!3   | Please enter a valid email address          |

# ================= Login with invalid input =================
@Login @InvalidInput
Scenario Outline: TC_0013 Check login with invalid credentials
	Given user is on login page
	When user enters  "<email>" and "<password>"
	And click on Login Button
    Then alert message "<alertMessage>" should be displayed

  Examples:
      | email          | password    | alertMessage             |
      | new@gmail.com  | %$7546')    | Confirm your email       |
      | user@gmail.com | #$%()ftt    | Confirm your email       |
