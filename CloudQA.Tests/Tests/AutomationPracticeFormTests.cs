using NUnit.Framework;
using OpenQA.Selenium;
using CloudQA.Tests.PageObjects;
using CloudQA.Tests.Utilities;

namespace CloudQA.Tests.Tests
{
    [TestFixture]
    public class AutomationPracticeFormTests
    {
        private IWebDriver driver;
        private AutomationPracticeFormPage formPage;

        [SetUp]
        public void Setup()
        {
            driver = WebDriverFactory.CreateDriver("Chrome");
            formPage = new AutomationPracticeFormPage(driver);
            formPage.NavigateToPage();
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }

        #region First Name Field Tests

        [Test]
        [Category("FirstName")]
        [Description("Test Case 1: Verify First Name field is displayed on the form")]
        public void Test_FirstNameField_IsDisplayed()
        {
            var isDisplayed = formPage.IsFirstNameFieldDisplayed();
            Assert.That(isDisplayed, Is.True, "First Name field should be displayed on the form");
        }

        [Test]
        [Category("FirstName")]
        [Description("Test Case 2: Verify First Name field accepts text input")]
        public void Test_FirstNameField_FillsWithValidText()
        {
            var testFirstName = "John";
            formPage.FillFirstName(testFirstName);
            var filledValue = formPage.GetFirstNameValue();
            Assert.That(filledValue, Is.EqualTo(testFirstName), 
                $"First Name field should contain the entered value '{testFirstName}'");
        }

        [Test]
        [Category("FirstName")]
        [Description("Test Case 3: Verify First Name field clears and accepts new input")]
        public void Test_FirstNameField_ClearsAndAcceptsNewInput()
        {
            var firstInput = "Alice";
            var secondInput = "Bob";

            formPage.FillFirstName(firstInput);
            var firstValue = formPage.GetFirstNameValue();
            Assert.That(firstValue, Is.EqualTo(firstInput), "Field should contain first input");

            formPage.FillFirstName(secondInput);
            var secondValue = formPage.GetFirstNameValue();
            Assert.That(secondValue, Is.EqualTo(secondInput), 
                "Field should clear previous value and accept new input");
        }

        [Test]
        [Category("FirstName")]
        [Description("Test Case 4: Verify First Name field accepts special characters")]
        public void Test_FirstNameField_AcceptsSpecialCharacters()
        {
            var nameWithSpecialChars = "Jean-Pierre O'Connor";
            formPage.FillFirstName(nameWithSpecialChars);
            var filledValue = formPage.GetFirstNameValue();
            Assert.That(filledValue, Is.EqualTo(nameWithSpecialChars), 
                "First Name field should accept special characters like hyphens and apostrophes");
        }

        #endregion

        #region Email Field Tests

        [Test]
        [Category("Email")]
        [Description("Test Case 5: Verify Email field is displayed on the form")]
        public void Test_EmailField_IsDisplayed()
        {
            var isDisplayed = formPage.IsEmailFieldDisplayed();
            Assert.That(isDisplayed, Is.True, "Email field should be displayed on the form");
        }

        [Test]
        [Category("Email")]
        [Description("Test Case 6: Verify Email field is enabled for input")]
        public void Test_EmailField_IsEnabled()
        {
            var isEnabled = formPage.IsEmailFieldEnabled();
            Assert.That(isEnabled, Is.True, "Email field should be enabled for user input");
        }

        [Test]
        [Category("Email")]
        [Description("Test Case 7: Verify Email field accepts valid email format")]
        public void Test_EmailField_FillsWithValidEmailFormat()
        {
            var validEmail = "john.doe@example.com";
            formPage.FillEmail(validEmail);
            var filledValue = formPage.GetEmailValue();
            Assert.That(filledValue, Is.EqualTo(validEmail), 
                $"Email field should contain the entered email '{validEmail}'");
        }

        [Test]
        [Category("Email")]
        [Description("Test Case 8: Verify Email field accepts different valid email formats")]
        [TestCase("user@domain.com")]
        [TestCase("user.name@domain.co.uk")]
        [TestCase("user+tag@domain.org")]
        public void Test_EmailField_AcceptsVariousValidFormats(string testEmail)
        {
            formPage.FillEmail(testEmail);
            var filledValue = formPage.GetEmailValue();
            Assert.That(filledValue, Is.EqualTo(testEmail), 
                $"Email field should accept the email format: {testEmail}");
        }

        [Test]
        [Category("Email")]
        [Description("Test Case 9: Verify Email field clears and accepts new input")]
        public void Test_EmailField_ClearsAndAcceptsNewInput()
        {
            var firstEmail = "first@example.com";
            var secondEmail = "second@example.com";

            formPage.FillEmail(firstEmail);
            var firstValue = formPage.GetEmailValue();
            Assert.That(firstValue, Is.EqualTo(firstEmail), "Field should contain first email");

            formPage.FillEmail(secondEmail);
            var secondValue = formPage.GetEmailValue();
            Assert.That(secondValue, Is.EqualTo(secondEmail), 
                "Field should clear previous email and accept new email");
        }

        #endregion

        #region Gender (Radio Button) Field Tests

        [Test]
        [Category("Gender")]
        [Description("Test Case 10: Verify Male gender radio button can be selected")]
        public void Test_GenderField_SelectMale()
        {
            formPage.SelectGender("male");
            var isMaleSelected = formPage.IsGenderSelected("male");
            Assert.That(isMaleSelected, Is.True, "Male radio button should be selected");
        }

        [Test]
        [Category("Gender")]
        [Description("Test Case 11: Verify Female gender radio button can be selected")]
        public void Test_GenderField_SelectFemale()
        {
            formPage.SelectGender("female");
            var isFemaleSelected = formPage.IsGenderSelected("female");
            Assert.That(isFemaleSelected, Is.True, "Female radio button should be selected");
        }

        [Test]
        [Category("Gender")]
        [Description("Test Case 12: Verify Other gender radio button can be selected")]
        public void Test_GenderField_SelectOther()
        {
            formPage.SelectGender("other");
            var isOtherSelected = formPage.IsGenderSelected("other");
            Assert.That(isOtherSelected, Is.True, "Other radio button should be selected");
        }

        [Test]
        [Category("Gender")]
        [Description("Test Case 13: Verify radio button selection can be changed")]
        public void Test_GenderField_CanChangeSelection()
        {
            formPage.SelectGender("male");
            var isMaleSelected = formPage.IsGenderSelected("male");
            Assert.That(isMaleSelected, Is.True, "Male should be initially selected");

            formPage.SelectGender("female");
            var isFemaleSelected = formPage.IsGenderSelected("female");
            var isMaleStillSelected = formPage.IsGenderSelected("male");

            Assert.That(isFemaleSelected, Is.True, "Female should be selected after switching");
            Assert.That(isMaleStillSelected, Is.False, "Male should no longer be selected");
        }

        [Test]
        [Category("Gender")]
        [Description("Test Case 14: Verify gender field behavior with form submission")]
        public void Test_GenderField_WithFormSubmission()
        {
            formPage.FillFirstName("TestUser");
            formPage.FillEmail("test@example.com");
            formPage.SelectGender("female");

            var isFemaleSelected = formPage.IsGenderSelected("female");
            Assert.That(isFemaleSelected, Is.True, 
                "Gender selection should persist and be available for form submission");
        }

        #endregion

        #region Integrated Form Tests

        [Test]
        [Category("Integration")]
        [Description("Test Case 15: Complete form submission with all three fields")]
        public void Test_CompleteFormSubmission_WithAllFields()
        {
            var testFirstName = "Jane";
            var testEmail = "jane.doe@example.com";
            var testGender = "female";

            formPage.FillFirstName(testFirstName);
            formPage.FillEmail(testEmail);
            formPage.SelectGender(testGender);

            var firstName = formPage.GetFirstNameValue();
            var email = formPage.GetEmailValue();
            var genderSelected = formPage.IsGenderSelected(testGender);

            Assert.That(firstName, Is.EqualTo(testFirstName), "First Name should be correctly filled");
            Assert.That(email, Is.EqualTo(testEmail), "Email should be correctly filled");
            Assert.That(genderSelected, Is.True, "Gender should be correctly selected");

            Assert.That(formPage.IsSubmitButtonVisible(), Is.True, "Submit button should be visible");
        }

        [Test]
        [Category("Integration")]
        [Description("Test Case 16: Form loads successfully with all required fields present")]
        public void Test_FormLoads_WithAllRequiredFieldsPresent()
        {
            Assert.That(formPage.IsFormDisplayed(), Is.True, 
                "Form should display with First Name and Email fields");
            Assert.That(formPage.IsFirstNameFieldDisplayed(), Is.True, "First Name field should be present");
            Assert.That(formPage.IsEmailFieldDisplayed(), Is.True, "Email field should be present");
        }

        #endregion
    }
}
