Feature: Test scenarios for Language tab on Profile page

As a user, I am able to add, edit or delete my language records

# ================= Add Language =================
@Language @Add
Scenario Outline: TC_002 Verify that user is able to add a new Language 
	Given the user is on the languages tab of the profile page	
	When the user adds "<language>" with "<level>"
	Then the "<language>" with "<level>" should be added successfully

	Examples:
		 | language    |    level			  |
		 | Hindi       |    Fluent            | 
		 | French	   |	Basic             | 
		 | Chinese	   |    Conversational    | 

# ================= Edit Language =================
@Language @Edit
Scenario Outline: TC_003 Verify that user is able to edit a Language
	Given the user is on the languages tab of the profile page		
	When the user adds "<language>" with "<level>"
	And the user edit "<editedLanguage>" with "<editedLevel>"
	Then the "<editedLanguage>" with "<editedLevel>" should be edited successfully

	Examples:
    | language | level  | editedLanguage | editedLevel |
    | English  | Fluent | Japanese        | Basic       |

# ================= Delete Language =================
@Language @Delete
Scenario Outline: TC_004 Verify that user is able to Delete a Language
	Given the user is on the languages tab of the profile page
	When the user adds "<language>" with "<level>"
	And the user delete "<language>"
	Then the "<language>" with "<level>" should be deleted successfully
		Examples:
		 | language     |     level        |
		 | Tamil        |     Fluent       | 

# ================= Invalid Language Input for negative/destructive testing =================
@Language @InvalidInput
Scenario Outline: TC_005, TC_007, TC_009 Verify that correct message is displayed when adding invalid Language
	Given the user is on the languages tab of the profile page
	When the user adds "<language>" with "<level>"
	Then the "<message>" should be displayed successfully

	Examples:
		 | language						|	level      |  message														|
		 | %!@#$%^&*()_+&#124;}			|	Fluent     |  %!@#$%^&*()_+&#124;} has been added to your languages			|
		 |  							|			   |  Please enter language and level								| 
		 | <script>alert('x')</script>	|	Basic	   |  <script>alert('x')</script> has been added to your languages	|

# ================= Duplicate Language Error =================
@Language @Duplicate
Scenario: TC_010, TC_011 Verify duplicate error messages
  Given the user is on the languages tab of the profile page
  When the user adds multiple languages:
    | language | level          |
    | Spanish  | Conversational |
    | Spanish  | Conversational |
    | Spanish  | Basic          |
  Then the following duplicate error messages should be displayed successfully:
    | errorMessage                                          |
    | Spanish has been added to your languages              |
    | This language is already exist in your language list. |
    | Duplicated data                                       |

# ================= Large Payload for destructive testing =================
@Language @Destructive
Scenario: TC_006 Verify that the language field can handle large payloads
  Given the user is on the languages tab of the profile page
  When the user adds a language with a large payload and level
  Then the system should display a validation message