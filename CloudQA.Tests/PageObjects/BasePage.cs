using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace CloudQA.Tests.PageObjects
{
    public abstract class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        private const int DefaultTimeout = 10;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(DefaultTimeout));
        }

        protected IWebElement WaitForElementToBeVisible(By locator)
        {
            return wait.Until(ExpectedConditions.VisibilityOfElementLocated(locator));
        }

        protected IWebElement WaitForElementToBeClickable(By locator)
        {
            return wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        protected bool WaitForElementToBePresent(By locator)
        {
            try
            {
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void ScrollToElement(IWebElement element)
        {
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            System.Threading.Thread.Sleep(500);
        }

        protected string GetElementText(By locator)
        {
            var element = WaitForElementToBeVisible(locator);
            return element.Text;
        }

        protected void FillTextInput(By locator, string text)
        {
            var element = WaitForElementToBeVisible(locator);
            ScrollToElement(element);
            element.Clear();
            element.SendKeys(text);
        }

        protected void ClickElement(By locator)
        {
            var element = WaitForElementToBeClickable(locator);
            ScrollToElement(element);
            element.Click();
        }

        protected void SelectDropdownByValue(By locator, string value)
        {
            var element = WaitForElementToBeVisible(locator);
            ScrollToElement(element);
            var selectElement = new SelectElement(element);
            selectElement.SelectByValue(value);
        }

        protected void SelectDropdownByText(By locator, string text)
        {
            var element = WaitForElementToBeVisible(locator);
            ScrollToElement(element);
            var selectElement = new SelectElement(element);
            selectElement.SelectByText(text);
        }

        protected bool IsElementDisplayed(By locator)
        {
            try
            {
                return WaitForElementToBeVisible(locator).Displayed;
            }
            catch
            {
                return false;
            }
        }

        protected string GetAttributeValue(By locator, string attributeName)
        {
            var element = WaitForElementToBeVisible(locator);
            return element.GetAttribute(attributeName);
        }
    }
}
