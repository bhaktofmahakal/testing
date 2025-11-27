using OpenQA.Selenium;

namespace CloudQA.Tests.PageObjects
{
    public class AutomationPracticeFormPage : BasePage
    {
        private const string PageUrl = "http://app.cloudqa.io/home/AutomationPracticeForm";

        public AutomationPracticeFormPage(IWebDriver driver) : base(driver)
        {
        }

        public void NavigateToPage()
        {
            driver.Navigate().GoToUrl(PageUrl);
            WaitForPageToLoad();
        }

        private void WaitForPageToLoad()
        {
            WaitForElementToBePresent(FirstNameLocator());
        }

        #region First Name Field Locators
        
        private By FirstNameLocator()
        {
            return By.XPath("//label[contains(text(), 'First Name') or contains(., 'First Name')]//following::input[@type='text'][1]");
        }

        private By FirstNameByPlaceholder()
        {
            return By.XPath("//input[@placeholder[contains(., 'First')]]");
        }

        private By FirstNameByName()
        {
            return By.Name("firstname");
        }

        #endregion

        #region Last Name Field Locators
        
        private By LastNameLocator()
        {
            return By.XPath("//label[contains(text(), 'Last Name') or contains(., 'Last Name')]//following::input[@type='text'][1]");
        }

        private By LastNameByName()
        {
            return By.Name("lastname");
        }

        #endregion

        #region Email Field Locators
        
        private By EmailLocator()
        {
            return By.XPath("//label[contains(text(), 'Email') or contains(., 'Email')]//following::input[@type='email' or @type='text'][1]");
        }

        private By EmailByType()
        {
            return By.XPath("//input[@type='email']");
        }

        private By EmailByName()
        {
            return By.Name("email");
        }

        #endregion

        #region Gender/Radio Button Locators
        
        private By GenderRadioButtonLocator(string genderValue)
        {
            return By.XPath($"//label[contains(text(), '{genderValue}')]//preceding::input[@type='radio'][1] | //input[@type='radio' and @value='{genderValue.ToLower()}']");
        }

        private By MaleRadioButton()
        {
            return By.XPath("//label[contains(text(), 'Male')]//preceding::input[@type='radio'][1]");
        }

        private By FemaleRadioButton()
        {
            return By.XPath("//label[contains(text(), 'Female')]//preceding::input[@type='radio'][1]");
        }

        private By OtherRadioButton()
        {
            return By.XPath("//label[contains(text(), 'Other')]//preceding::input[@type='radio'][1]");
        }

        #endregion

        #region First Name Operations

        public void FillFirstName(string firstName)
        {
            FillTextInput(FirstNameLocator(), firstName);
        }

        public string GetFirstNameValue()
        {
            return GetAttributeValue(FirstNameLocator(), "value");
        }

        public bool IsFirstNameFieldDisplayed()
        {
            return IsElementDisplayed(FirstNameLocator());
        }

        #endregion

        #region Last Name Operations

        public void FillLastName(string lastName)
        {
            FillTextInput(LastNameLocator(), lastName);
        }

        public string GetLastNameValue()
        {
            return GetAttributeValue(LastNameLocator(), "value");
        }

        public bool IsLastNameFieldDisplayed()
        {
            return IsElementDisplayed(LastNameLocator());
        }

        #endregion

        #region Email Operations

        public void FillEmail(string email)
        {
            FillTextInput(EmailLocator(), email);
        }

        public string GetEmailValue()
        {
            return GetAttributeValue(EmailLocator(), "value");
        }

        public bool IsEmailFieldDisplayed()
        {
            return IsElementDisplayed(EmailLocator());
        }

        public bool IsEmailFieldEnabled()
        {
            try
            {
                var element = WaitForElementToBeVisible(EmailLocator());
                return element.Enabled;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Gender/Radio Button Operations

        public void SelectGender(string gender)
        {
            By genderLocator = gender.ToLower() switch
            {
                "male" => MaleRadioButton(),
                "female" => FemaleRadioButton(),
                "other" => OtherRadioButton(),
                _ => GenderRadioButtonLocator(gender)
            };

            ClickElement(genderLocator);
        }

        public bool IsGenderSelected(string gender)
        {
            By genderLocator = gender.ToLower() switch
            {
                "male" => MaleRadioButton(),
                "female" => FemaleRadioButton(),
                "other" => OtherRadioButton(),
                _ => GenderRadioButtonLocator(gender)
            };

            try
            {
                var element = WaitForElementToBeVisible(genderLocator);
                return element.Selected;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Submit Button Operations

        public void SubmitForm()
        {
            var submitButton = By.XPath("//button[contains(text(), 'Submit') or @type='submit']");
            ClickElement(submitButton);
        }

        public bool IsSubmitButtonVisible()
        {
            var submitButton = By.XPath("//button[contains(text(), 'Submit') or @type='submit']");
            return IsElementDisplayed(submitButton);
        }

        #endregion

        #region Form Validation

        public bool IsFormDisplayed()
        {
            return IsElementDisplayed(FirstNameLocator()) && IsElementDisplayed(EmailLocator());
        }

        #endregion
    }
}
