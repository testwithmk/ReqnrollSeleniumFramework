using NUnit.Framework;
using ReqnrollSeleniumFramework.Pages;
using ReqnrollSeleniumFramework.Utilities;

namespace ReqnrollSeleniumFramework.StepDefinitions
{
    [Binding]
    public class LoginFunctionalityStepDefinitions : CommonDriver
    {
        private LoginPage loginPageObj;

        [Given(@"user is on login page")]
        public void GivenUserIsOnLoginPage()
        {
            loginPageObj = new LoginPage();
            loginPageObj.NavigateToLoginPage();
        }

        [When(@"user enters valid credentials")]
        public void WhenUserEntersValidEmailAndPassword()
        {
            loginPageObj.EnterValidCredentials();
        }

        [When(@"user enters  ""(.*)"" and ""(.*)""")]
        public void WhenUserEntersInvalidEmailAndPassword(string email, string password)
        {
            loginPageObj.EnterCredentials(email, password);
        }

        [When(@"click on Login Button")]
        public void WhenClickOnLoginButton()
        {
            loginPageObj.ClickLoginButton();
        }

        [Then(@"the user is navigated to the Profile page")]
        public void ThenTheUserIsNavigatedToTheProfilePage()
        {
            string actualWelcomeText = loginPageObj.GetWelcomeText();
            Assert.AreEqual("Hi " + ConfigReader.GetValue("User_FirstName"), actualWelcomeText, "User did not navigate to Profile page.");
        }

        [Then(@"an error message ""(.*)"" should be displayed")]
        public void ThenAnErrorMessageShouldBeDisplayed(string expectedErrorMessage)
        {
            string actualErrorMessage = loginPageObj.GetErrorMessage();
            Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "Correct error message was not displayed");
        }

        [Then(@"alert message ""(.*)"" should be displayed")]
        public void ThenAlertMessageShouldBeDisplayed(string expectedEmailMessage)
        {
            string actualEmailMessage = loginPageObj.GetEmailMessage();
            Assert.AreEqual(expectedEmailMessage, actualEmailMessage, "Correct error message was not displayed");
        }
    }
}
