using OpenQA.Selenium;

namespace ReqnrollSeleniumFramework.Pages.ProfilePages
{
    /**
    * Represents the Languages tab on the Profile page.
    * Inherits from ProfileTabPage to reuse common tab functionality.
    * Initializes locators specific to the Languages tab in the constructor.
    */
    public class LanguagePage : ProfileTabPage
    {
        public LanguagePage()
        {
            string tabName = "Languages";
            tabXpath = "//div[@data-tab='first']";

            // Dynamic tab locator
            tabNameLocator = By.XPath($"//a[normalize-space()='{tabName}']");

            //locators
            addNewButton = By.XPath($"{tabXpath}//div[@class='ui teal button ']");
            addTextbox = By.XPath("//input[@placeholder='Add Language']");
            levelDropdown = By.XPath("//select[@name='level']");
            addButton = By.XPath("//input[@value='Add']");
            cancelButton = By.XPath("//input[@value='Cancel']");
            lastItem = By.XPath($"{tabXpath}//tbody/tr[last()]/td[1]");
            lastItemLevel = By.XPath($"{tabXpath}//tbody/tr[last()]/td[2]");
            message = By.XPath("//div[@class='ns-box-inner']");
            editButton = By.XPath($"{tabXpath}//tbody/tr[last()]/td[3]/span[1]");
            updateButton = By.XPath("//input[@value='Update']");
        }
    }
}