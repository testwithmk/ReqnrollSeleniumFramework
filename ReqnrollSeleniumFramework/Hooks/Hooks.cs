using NUnit.Framework;
using ReqnrollSeleniumFramework.Pages.ProfilePages;
using ReqnrollSeleniumFramework.Utilities;

namespace ReqnrollSeleniumFramework.Hooks
{
    /**
    * Contains hooks that run before or after scenarios.
    * Used for setup and cleanup tasks, such as initializing pages, 
    * clearing test data, or performing browser actions.
    */
    [Binding]
    public class Hooks : CommonDriver
    {
        [BeforeScenario]
        public static void SetUp()
        {
            GetWebDriver();
        }

        [AfterScenario(Order = 1)]
        public void CleanUpData()
        {
            // Cleanup added Languages
            var languagePage = new LanguagePage();
            languagePage.ClearAddedItemsData();

            // Cleanup added Skills
            var skillsPage = new SkillsPage();
            skillsPage.ClearAddedItemsData();
        }

        [AfterScenario]
        public static void CloseTest()
        {
            QuitDriver();
        }
    }
}
