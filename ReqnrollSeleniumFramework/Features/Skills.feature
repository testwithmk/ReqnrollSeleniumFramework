Feature: Test scenarios for Skills tab on Profile page

As a user, I am able to add, edit or delete my Skills records

# ================= Add Skill =================
@Skill @Add
Scenario Outline: TC_002 Verify that user is able to add a new Skill 
	Given the user is on the Skills tab of the profile page	
	When the user adds a skill "<skill>" with "<level>"
	Then the skill "<skill>" with "<level>" should be added successfully

	Examples:
		 | skill		|   level			|
		 | Testing		|   Intermediate    | 
		 | Agile		|	Beginner        | 
		 | Communication|   Expert          | 

# ================= Edit Skill =================
@Skill @Edit
Scenario Outline: TC_003 Verify that user is able to edit a Skill
	Given  the user is on the Skills tab of the profile page	
	When the user adds a skill "<skill>" with "<level>"
	And the user edit a skill "<editedSkill>" with "<editedLevel>"
	Then the edited Skill "<editedSkill>" with "<editedLevel>" should be edited successfully

	Examples:
    | skill    | level    | editedSkill | editedLevel  |
    | Skating  | Beginner | Running     | Expert       |

# ================= Delete Skill =================
@Skill @Delete
Scenario Outline: TC_004 Verify that user is able to Delete a Skill
	Given  the user is on the Skills tab of the profile page		
	When the user adds a skill "<skill>" with "<level>"
	And the user delete a skill "<skill>"
	Then the skill "<skill>" with "<level>" should be deleted successfully
		Examples:
		 | skill   |     level         |
		 | API     |     Intermediate  | 

# ================= Invalid Skills Input =================
@Skill @InvalidInput
Scenario Outline: TC_005, TC_007, TC_009 Verify that correct message is displayed when adding invalid Skill
	Given  the user is on the Skills tab of the profile page		
	When the user adds a skill "<skill>" with "<level>"
	Then the message "<message>" should be displayed successfully

	Examples:
		 | skill		|	level          |   message										|
		 | %+&#56)g^5   |	Beginner       |   %+&#56)g^5 has been added to your skills	    |
		 |  			|                  |   Please enter skill and experience level		|
		 | <test>		|	Expert         |   <test> has been added to your skills			|

# ================= Duplicate Skills Error =================
@Skill @Duplicate
Scenario: TC_010, TC_011 Verify duplicate error messages
  Given  the user is on the Skills tab of the profile page
  When the user adds multiple skills:
    | skill      | level          |
    | Manual     | Expert         |
    | Manual     | Expert	      |
    | Manual     | Intermediate   |
  Then duplicate error messages for skills should be displayed successfully:
    | errorMessage                                          |
    | Manual has been added to your skills                  |
    | This skill is already exist in your skill list.       |
    | Duplicated data                                       |

# ================= Large Payload for destructive testing =================
@Language @Destructive
Scenario: TC_006 Verify that the skill field can handle large payloads
  Given the user is on the Skills tab of the profile page	
  When the user adds a skill with a large payload and level
  Then a validation message should be displayed successfully