using NUnit.Framework;
using ReqnrollSeleniumFramework.Pages;
using ReqnrollSeleniumFramework.Pages.ProfilePages;
using ReqnrollSeleniumFramework.Utilities;

namespace ReqnrollSeleniumFramework.StepDefinitions
{
    [Binding]
    public class SkillsTabStepDefinitions : CommonDriver
    {
        private LoginPage loginPageObj;
        private SkillsPage skillsPageObj;
        private List<string> actualMessages;
        private bool skillDeleted;

        [Given(@"the user is on the Skills tab of the profile page")]
        public void GivenTheUserIsOnSkillsTab()
        {
            loginPageObj = new LoginPage();
            loginPageObj.NavigateToLoginPage();
            loginPageObj.EnterValidCredentials();

            skillsPageObj = new SkillsPage();
            skillsPageObj.GoToTab();
            string actualTabName = skillsPageObj.GetTabName();
            Assert.AreEqual("Skills", actualTabName, "User is not on Skills tab");
        }

        [When(@"the user adds a skill ""(.*)"" with ""(.*)""")]
        public void WhenTheUserAddsSkillWithLevel(string skill, string level)
        {
            skillsPageObj.AddNewItem(skill, level);
        }

        [Then(@"the skill ""(.*)"" with ""(.*)"" should be added successfully")]
        public void ThenSkillShouldBeAddedSuccessfully(string expectedSkill, string expectedLevel)
        {
            string actualMessage = skillsPageObj.GetMessage();
            string expectedMessage = $"{expectedSkill} has been added to your skills";
            Assert.AreEqual(expectedMessage, actualMessage, "Success message is not correct");

            string actualSkill = skillsPageObj.GetItem();
            string actualLevel = skillsPageObj.GetItemLevel();
            Assert.AreEqual(expectedSkill, actualSkill, "Skill is not added correctly");
            Assert.AreEqual(expectedLevel, actualLevel, "Level is not added correctly");
        }

        [When(@"the user edit a skill ""(.*)"" with ""(.*)""")]
        public void WhenTheUserEditsSkillWithLevel(string editedSkill, string editedLevel)
        {
            skillsPageObj.EditItem(editedSkill, editedLevel);
        }

        [Then(@"the edited Skill ""(.*)"" with ""(.*)"" should be edited successfully")]
        public void ThenSkillShouldBeEditedSuccessfully(string editedSkill, string editedLevel)
        {
            string actualMessage = skillsPageObj.GetMessage();
            string expectedMessage = $"{editedSkill} has been updated to your skills";
            Assert.AreEqual(expectedMessage, actualMessage, "Update message is not correct");

            string actualSkill = skillsPageObj.GetItem();
            string actualLevel = skillsPageObj.GetItemLevel();
            Assert.AreEqual(editedSkill, actualSkill, "Skill is not updated correctly");
            Assert.AreEqual(editedLevel, actualLevel, "Level is not updated correctly");
        }

        [When(@"the user delete a skill ""(.*)""")]
        public void WhenTheUserDeletesSkill(string skill)
        {
            skillDeleted = skillsPageObj.DeleteItem(skill);
        }

        [Then(@"the skill ""(.*)"" with ""(.*)"" should be deleted successfully")]
        public void ThenSkillShouldBeDeletedSuccessfully(string expectedSkill, string expectedLevel)
        {
            if (skillDeleted)
            {
                string actualMessage = skillsPageObj.GetMessage();
                string expectedMessage = $"{expectedSkill} has been deleted";
                Assert.AreEqual(expectedMessage, actualMessage, "Delete message is not correct");

                bool isPresent = skillsPageObj.IsItemPresent(expectedSkill);
                Assert.IsFalse(isPresent, $"Skill '{expectedSkill}' is still present after deletion.");
            }
            else
            {
                Assert.Pass($"'{expectedSkill}' was already deleted");
            }
        }

        [Then(@"the message ""(.*)"" should be displayed successfully")]
        public void ThenMessageShouldBeDisplayedSuccessfully(string expectedMessage)
        {
            string actualMessage = skillsPageObj.GetMessage();
            Assert.AreEqual(expectedMessage, actualMessage, "Correct message is not displayed");
        }

        [When(@"the user adds multiple skills:")]
        public void WhenTheUserAddsMultipleSkills(Table table)
        {
            actualMessages = skillsPageObj.AddMultipleItems(table);
        }

        [Then(@"duplicate error messages for skills should be displayed successfully:")]
        public void ThenDuplicateSkillMessagesShouldBeDisplayedSuccessfully(Table table)
        {
            var expectedMessages = table.Rows.Select(r => r["errorMessage"]).ToList();
            CollectionAssert.AreEqual(expectedMessages, actualMessages, "Duplicate messages did not match");
        }

        [When(@"the user adds a skill with a large payload and level")]
        public void WhenTheUserAddsASkillWithLargePayload()
        {
            string largePayload = TestDataHelper.GenerateLargePayload(300, 400, 200, 300);
            skillsPageObj.AddNewItem(largePayload, "Expert");
        }

        [Then(@"a validation message should be displayed successfully")]
        public void ThenValidationMessageShouldBeDisplayed()
        {
            string actualMessage = skillsPageObj.GetMessage();
            Assert.IsTrue(actualMessage.Contains("11111111111"), "Message does not contain expected payload part");
        }
    }
}
