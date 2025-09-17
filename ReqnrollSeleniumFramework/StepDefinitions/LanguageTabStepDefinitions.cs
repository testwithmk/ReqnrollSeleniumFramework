using NUnit.Framework;
using ReqnrollSeleniumFramework.Pages;
using ReqnrollSeleniumFramework.Pages.ProfilePages;
using ReqnrollSeleniumFramework.Utilities;

namespace ReqnrollSeleniumFramework.StepDefinitions
{
    [Binding]
    public class LanguageTabStepDefinitions : CommonDriver
    {
        private LoginPage loginPageObj;
        private LanguagePage languagePageObj;
        private List<string> actualMessages;
        private bool lang_deleted;

        [Given(@"the user is on the languages tab of the profile page")]
        public void GivenTheUserIsOnLanguagesTab()
        {
            loginPageObj = new LoginPage();
            loginPageObj.NavigateToLoginPage();
            loginPageObj.EnterValidCredentials();

            languagePageObj = new LanguagePage();
            string actualTabName = languagePageObj.GetTabName();
            Assert.AreEqual("Languages", actualTabName, "User is not on Languages tab");
        }

        [When(@"the user adds ""(.*)"" with ""(.*)""")]
        public void WhenTheUserAddsLanguageWithLanguageLevel(string language, string level)
        {
            languagePageObj.AddNewItem(language, level);
        }

        [Then(@"the ""(.*)"" with ""(.*)"" should be added successfully")]
        public void ThenLanguageShouldBeAddedSuccessfully(string expectedLanguage, string expectedLevel)
        {
            string actualMessage = languagePageObj.GetMessage();
            string expectedMessage = $"{expectedLanguage} has been added to your languages";
            Assert.AreEqual(expectedMessage, actualMessage, "Success Message is not correct");

            string actualLanguage = languagePageObj.GetItem();
            string actualLevel = languagePageObj.GetItemLevel();
            Assert.AreEqual(expectedLanguage, actualLanguage, "Language is not added correctly");
            Assert.AreEqual(expectedLevel, actualLevel, "Level is not added correctly");
        }

        [When(@"the user edit ""(.*)"" with ""(.*)""")]
        public void WhenTheUserEditLanguageWithLanguageLevel(string editedLanguage, string editedLevel)
        {
            languagePageObj.EditItem(editedLanguage, editedLevel);
        }

        [Then(@"the ""(.*)"" with ""(.*)"" should be edited successfully")]
        public void ThenTheLanguageWithLanguageLevelShouldBeEditedSuccessfully(string editedLanguage, string editedLevel)
        {
            string actualMessage = languagePageObj.GetMessage();
            string expectedMessage = $"{editedLanguage} has been updated to your languages";
            Assert.AreEqual(expectedMessage, actualMessage, "Update Message is not correct");

            string actualLanguage = languagePageObj.GetItem();
            string actualLevel = languagePageObj.GetItemLevel();
            Assert.AreEqual(editedLanguage, actualLanguage, "Language is not updated correctly");
            Assert.AreEqual(editedLevel, actualLevel, "Level is not updated correctly");
        }

        [When(@"the user delete ""(.*)""")]
        public void WhenTheUserDeleteLanguage(string language)
        {
            lang_deleted = languagePageObj.DeleteItem(language);
        }

        [Then(@"the ""(.*)"" with ""(.*)"" should be deleted successfully")]
        public void ThenTheLanguageWithLevelShouldBeDeletedSuccessfully(string expectedLanguage, string expectedLevel)
        {
            if (lang_deleted)
            {
                string actualMessage = languagePageObj.GetMessage();
                string expectedMessage = $"{expectedLanguage} has been deleted from your languages";
                Assert.AreEqual(expectedMessage, actualMessage, "Delete Message is not correct");

                bool isPresent = languagePageObj.IsItemPresent(expectedLanguage);
                Assert.IsFalse(isPresent, $"Language '{expectedLanguage}' is still present after deletion.");
            }
            else
            {
                Assert.Pass($"'{expectedLanguage}' was already deleted");
            }
        }

        [Then(@"the ""(.*)"" should be displayed successfully")]
        public void ThenTheMessageShouldBeDisplayedSuccessfully(string expectedMessage)
        {
            string actualMessage = languagePageObj.GetMessage();
            Assert.AreEqual(expectedMessage, actualMessage, "Correct Message is not displayed");
        }

        [When(@"the user adds multiple languages:")]
        public void WhenTheUserAddsMultipleLanguages(Table table)
        {
            actualMessages = languagePageObj.AddMultipleItems(table);
        }

        [Then(@"the following duplicate error messages should be displayed successfully:")]
        public void ThenDuplicateMessageShouldBeDisplayedSuccessfully(Table table)
        {
            var expectedMessages = table.Rows.Select(r => r["errorMessage"]).ToList();
            CollectionAssert.AreEqual(expectedMessages, actualMessages, "Error messages did not match");
        }

        [When(@"the user adds a language with a large payload and level")]
        public void WhenTheUserAddsALanguageWithALargePayloadAndLevel()
        {
            string largePayload = TestDataHelper.GenerateLargePayload(300, 400, 200, 300);
            languagePageObj.AddNewItem(largePayload, "Basic");
        }

        [Then(@"the system should display a validation message")]
        public void ThenTheSystemShouldDisplayAValidationMessage()
        {
            string actualMessage = languagePageObj.GetMessage();
            Assert.IsTrue(actualMessage.Contains("11111111111"), "Message does not contain expected payload part");
        }

    }
}
