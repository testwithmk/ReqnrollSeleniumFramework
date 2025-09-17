using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnrollSeleniumFramework.Pages.ProfilePages
{
    /** 
    * Abstract base class for profile tabs (e.g., Languages, Skills).
    * Encapsulates common functionality such as adding, editing, deleting, 
    * validating, and cleaning up tab items. 
    * Specific tab pages (LanguagePage, SkillsPage, etc.) inherit from this 
    * class and initialize tab-specific locators.
    */
    public abstract class ProfileTabPage : BasePage
    {
        // Common locators
        protected By tabNameLocator;
        protected By addNewButton;
        protected By addTextbox;
        protected By levelDropdown;
        protected By addButton;
        protected By cancelButton;
        protected By lastItem;
        protected By lastItemLevel;
        protected By message;
        protected By editButton;
        protected By updateButton;
        protected string tabXpath;

        // Track added items (skills/languages)
        protected List<string> addedItems = new List<string>();

        // Constructor
        public ProfileTabPage() { }

        /** Go to specific tab */
        public void GoToTab()
        {
            WaitToBeVisible(tabNameLocator, 5);
            ClickButton(tabNameLocator);
        }
        /** Get tab name dynamically */
        public string GetTabName()
        {
            return GetWebDriver().FindElement(tabNameLocator).Text.Trim();
        }

        /** Add new item (language/skill)*/
        public void AddNewItem(string item, string level)
        {
            ClickButton(addNewButton);

            WaitToBeVisible(addTextbox, 5);
            ClearText(addTextbox);
            EnterText(addTextbox, item);

            SelectValueFromDropdown(levelDropdown, level);
            ClickButton(addButton);

            WaitForPageToLoad(2);
            addedItems.Add(item);
        }
        /** Add multiple items (languages/skills) */
        public List<string> AddMultipleItems(Table table)
        {
            List<string> messages = new List<string>();

            foreach (var row in table.Rows)
            {
                string itemName;

                // Check column name 
                if (row.ContainsKey("language"))
                    itemName = row["language"];
                else if (row.ContainsKey("skill"))
                    itemName = row["skill"];
                else
                    throw new ArgumentException("Table must contain 'language' or 'skill' column");

                string level = row["level"];

                try
                {
                    AddNewItem(itemName, level);

                    // Fetch message
                    string msg = GetMessage();
                    messages.Add(msg);

                    // Click cancel if duplicate/error
                    if (msg.Contains("already exist") || msg.Contains("Duplicated"))
                    {
                        ClickButton(cancelButton);
                        WaitForPageToLoad(5);
                    }

                    WaitForPageToLoad(5);
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine($"Element not found {itemName}");
                }
                WaitForPageToLoad(2);
            }
            return messages;
        }

        /** Get item text */
        public string GetItem()
        {
            return GetText(lastItem);
        }

        /** Get item level */
        public string GetItemLevel()
        {
            return GetText(lastItemLevel);
        }

        /** Get message */
        public string GetMessage()
        {
            return GetText(message);
        }

        /** Edit item */
        public void EditItem(string newItem, string newLevel)
        {
            ClickButton(editButton);
            ClearText(addTextbox);
            EnterText(addTextbox, newItem);
            SelectValueFromDropdown(levelDropdown, newLevel);
            ClickButton(updateButton);
        }

        /** Delete item */
        public bool DeleteItem(string expectedItem)
        {
            try
            {
                var rows = GetWebDriver().FindElements(By.XPath($"{tabXpath}//tbody/tr"));

                foreach (var row in rows)
                {
                    var presentItem = row.FindElement(By.XPath("./td[1]")).Text;

                    if (presentItem == expectedItem)
                    {
                        row.FindElement(By.XPath("./td[3]/span[2]")).Click();
                        return true;
                    }
                }
                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /** Cleanup */
        public void ClearAddedItemsData()
        {
            foreach (var item in addedItems)
            {
                try
                {
                    bool deleted = DeleteItem(item);
                    if (!deleted)
                    {
                        Console.WriteLine($"'{item}' already deleted.");
                        WaitForPageToLoad(2);
                    }
                }
                catch (StaleElementReferenceException)
                {
                    Console.WriteLine($"'{item}' is no longer present.");
                }
            }
            addedItems.Clear();
        }

        /** Check if item exists */
        public bool IsItemPresent(string item)
        {
            try
            {
                var rows = GetWebDriver().FindElements(By.XPath($"{tabXpath}//tbody/tr/td[1]"));
                return rows.Any(r => r.Text.Trim() == item);
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
