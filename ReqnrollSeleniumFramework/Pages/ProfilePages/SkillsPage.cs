using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using OpenQA.Selenium;

namespace ReqnrollSeleniumFramework.Pages.ProfilePages
{
    /**
    * Represents the Skills tab on the Profile page.
    * Inherits from ProfileTabPage to reuse common tab functionality.
    * Initializes locators specific to the Skills tab in the constructor.
    */
    public class SkillsPage : ProfileTabPage
    {
        public SkillsPage()
        {
            string tabName = "Skills";
            string tabXpath = "//div[@data-tab='second']";

            // Dynamic tab locator
            tabNameLocator = By.XPath($"//a[normalize-space()='{tabName}']");

            //locators
            tabNameLocator = By.XPath("//a[normalize-space()='Skills']");
            addNewButton = By.XPath($"{tabXpath}//div[@class='ui teal button']");
            addTextbox = By.XPath("//input[@placeholder='Add Skill']");
            levelDropdown = By.XPath("//select[@name='level']");
            addButton = By.XPath("//input[@value='Add']");
            cancelButton = By.XPath("//input[@value='Cancel']");
            lastItem = By.XPath($"{tabXpath}//tbody[last()]/tr/td[1]");
            lastItemLevel = By.XPath($"{tabXpath}//tbody[last()]/tr/td[2]");
            message = By.XPath("//div[@class='ns-box-inner']");
            editButton = By.XPath($"{tabXpath}//tbody[last()]/tr/td[3]/span[1]");
            updateButton = By.XPath("//input[@value='Update']");
        }
    }
}


