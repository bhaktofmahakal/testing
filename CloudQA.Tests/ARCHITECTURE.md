# Architecture - CloudQA Selenium Test Framework

## 🏗️ System Architecture Overview

```
┌─────────────────────────────────────────────────────────────────┐
│                    NUnit Test Framework                         │
│              (AutomationPracticeFormTests.cs)                   │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  ┌──────────────────────────────────────────────────────────┐  │
│  │           Page Object Model (POM) Layer                  │  │
│  │                                                          │  │
│  │  ┌────────────────────────────────────────────────────┐ │  │
│  │  │   AutomationPracticeFormPage                      │ │  │
│  │  │   ├── First Name Field Operations                 │ │  │
│  │  │   ├── Email Field Operations                      │ │  │
│  │  │   ├── Gender/Radio Button Operations             │ │  │
│  │  │   └── Form Navigation & Submission               │ │  │
│  │  └────────────────────────────────────────────────────┘ │  │
│  │                                                          │  │
│  │  ┌────────────────────────────────────────────────────┐ │  │
│  │  │         BasePage (Abstract Base Class)            │ │  │
│  │  │   ├── WaitForElementToBeVisible()                │ │  │
│  │  │   ├── WaitForElementToBeClickable()              │ │  │
│  │  │   ├── FillTextInput()                            │ │  │
│  │  │   ├── ClickElement()                             │ │  │
│  │  │   ├── SelectDropdown()                           │ │  │
│  │  │   └── ScrollToElement()                          │ │  │
│  │  └────────────────────────────────────────────────────┘ │  │
│  └──────────────────────────────────────────────────────────┘  │
│                                                                 │
├─────────────────────────────────────────────────────────────────┤
│                    Utility Layer                                │
│                                                                 │
│  ┌──────────────────────────────────────────────────────────┐  │
│  │            WebDriverFactory                            │  │
│  │   ├── CreateChromeDriver()                             │  │
│  │   ├── CreateFirefoxDriver()                            │  │
│  │   └── CreateEdgeDriver()                               │  │
│  └──────────────────────────────────────────────────────────┘  │
│                                                                 │
│  ┌──────────────────────────────────────────────────────────┐  │
│  │            Configuration                               │  │
│  │   ├── Browser Settings                                 │  │
│  │   ├── Timeout Settings                                 │  │
│  │   ├── Test URLs                                        │  │
│  │   └── Test Data                                        │  │
│  └──────────────────────────────────────────────────────────┘  │
│                                                                 │
├─────────────────────────────────────────────────────────────────┤
│                    Framework Layer                              │
│                                                                 │
│  ├── Selenium WebDriver (Browser Automation)                   │
│  ├── WebDriverWait (Explicit Waits)                           │
│  ├── WebDriverManager (Auto Driver Management)                │
│  └── NUnit (Test Execution & Assertions)                      │
│                                                                 │
├─────────────────────────────────────────────────────────────────┤
│                    Browser Layer                                │
│                                                                 │
│  ├── Chrome Browser                                            │
│  ├── Firefox Browser                                           │
│  └── Edge Browser                                              │
│                                                                 │
├─────────────────────────────────────────────────────────────────┤
│                    Target Application                           │
│                                                                 │
│  └── app.cloudqa.io/home/AutomationPracticeForm               │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

---

## 📁 Project Structure

```
CloudQA.Tests/
│
├── 📁 PageObjects/                    # Page Object Classes
│   ├── BasePage.cs                    # Base class with common operations
│   │   ├── WaitForElementToBeVisible()
│   │   ├── WaitForElementToBeClickable()
│   │   ├── FillTextInput()
│   │   ├── ClickElement()
│   │   ├── SelectDropdown()
│   │   ├── ScrollToElement()
│   │   └── Error handling & assertions
│   │
│   └── AutomationPracticeFormPage.cs  # Form-specific page object
│       ├── Page navigation
│       ├── First Name field locators
│       ├── Email field locators
│       ├── Gender radio button locators
│       └── Form operations
│
├── 📁 Tests/                          # Test Classes
│   └── AutomationPracticeFormTests.cs # 16 test cases
│       ├── Setup & TearDown
│       ├── First Name tests (4)
│       ├── Email tests (5)
│       ├── Gender tests (5)
│       └── Integration tests (2)
│
├── 📁 Utilities/                      # Utility Classes
│   └── WebDriverFactory.cs            # Driver initialization
│       ├── CreateChromeDriver()
│       ├── CreateFirefoxDriver()
│       └── CreateEdgeDriver()
│
├── 📁 Configuration/                  # Configuration Classes
│   └── TestSettings.cs                # Constants
│       ├── Browser options
│       ├── Timeout values
│       ├── URLs
│       └── Test data
│
├── 📄 CloudQA.Tests.csproj           # Project file
│   └── Dependencies:
│       ├── Selenium.WebDriver (4.15.0)
│       ├── Selenium.Support (4.15.0)
│       ├── NUnit (4.0.1)
│       ├── WebDriverManager (2.16.2)
│       └── Microsoft.NET.Test.Sdk (17.8.2)
│
├── 📄 .runsettings                   # NUnit configuration
├── 📄 .gitignore                     # Git ignore patterns
│
├── 📁 Scripts/                       # Batch files for test execution
│   ├── run-all-tests.bat
│   ├── run-firstname-tests.bat
│   ├── run-email-tests.bat
│   ├── run-gender-tests.bat
│   └── run-integration-tests.bat
│
└── 📁 Documentation/
    ├── README.md                      # Main documentation
    ├── QUICK_START.md                # 30-second setup
    ├── LOCATING_STRATEGY.md          # Selector explanation
    ├── EXAMPLES.md                   # Code examples
    ├── ARCHITECTURE.md               # This file
    └── SUBMISSION_SUMMARY.md         # Assignment completion
```

---

## 🔄 Data Flow

### 1. Test Execution Flow
```
┌─────────────────────────────────┐
│  Test Method Execution Starts   │
│  (@SetUp)                       │
└────────────┬────────────────────┘
             │
             ▼
┌─────────────────────────────────┐
│  WebDriver Initialization       │
│  (WebDriverFactory)             │
└────────────┬────────────────────┘
             │
             ▼
┌─────────────────────────────────┐
│  Page Object Creation           │
│  (AutomationPracticeFormPage)   │
└────────────┬────────────────────┘
             │
             ▼
┌─────────────────────────────────┐
│  Navigate to Target Page        │
│  (form.NavigateToPage())        │
└────────────┬────────────────────┘
             │
             ▼
┌─────────────────────────────────┐
│  Test Execution                 │
│  (Test body)                    │
└────────────┬────────────────────┘
             │
             ▼
┌─────────────────────────────────┐
│  Assertions & Verification      │
│  (Assert.That(...))             │
└────────────┬────────────────────┘
             │
             ▼
┌─────────────────────────────────┐
│  Cleanup & Browser Close        │
│  (@TearDown)                    │
└─────────────────────────────────┘
```

### 2. Page Object Interaction Flow
```
┌──────────────────────────┐
│   Test Method            │
│  formPage.FillFirstName()│
└────────────┬─────────────┘
             │
             ▼
┌──────────────────────────────────────┐
│  AutomationPracticeFormPage          │
│  FillFirstName() method              │
└────────────┬─────────────────────────┘
             │
             ▼
┌──────────────────────────────────────┐
│  Get Locator                         │
│  By FirstNameLocator()               │
│  [Primary: XPath with label text]    │
└────────────┬─────────────────────────┘
             │
             ▼
┌──────────────────────────────────────┐
│  BasePage.FillTextInput()            │
│  (Common operation)                  │
└────────────┬─────────────────────────┘
             │
             ▼
┌──────────────────────────────────────┐
│  Wait for Element                    │
│  WaitForElementToBeVisible()         │
└────────────┬─────────────────────────┘
             │
             ▼
┌──────────────────────────────────────┐
│  Scroll to Element                   │
│  (Ensure in viewport)                │
└────────────┬─────────────────────────┘
             │
             ▼
┌──────────────────────────────────────┐
│  Clear Existing Value                │
│  element.Clear()                     │
└────────────┬─────────────────────────┘
             │
             ▼
┌──────────────────────────────────────┐
│  Send Keys / Input Text              │
│  element.SendKeys(text)              │
└────────────┬─────────────────────────┘
             │
             ▼
┌──────────────────────────────────────┐
│  Return to Test Method               │
│  (Assertion ready)                   │
└──────────────────────────────────────┘
```

### 3. Selector Resolution Flow
```
┌─────────────────────────────────┐
│  Element Locator Needed         │
│  FirstNameLocator()             │
└────────────┬────────────────────┘
             │
             ▼
┌─────────────────────────────────┐
│  Primary Selector               │
│  XPath with label text          │
│  "//label[contains(...)]"       │
└────────────┬────────────────────┘
             │
         Success? ─ Yes ──────┐
             │                │
            No                ▼
             │            ┌─────────────┐
             ├──────────► │   Found!    │
             │            │ Use Element │
             ▼            └─────────────┘
┌─────────────────────────────────┐
│  Secondary Selector             │
│  Placeholder-based XPath        │
│  "//input[@placeholder[...]]"   │
└────────────┬────────────────────┘
             │
         Success? ─ Yes ──────┐
             │                │
            No                ▼
             │            ┌─────────────┐
             ├──────────► │   Found!    │
             │            │ Use Element │
             ▼            └─────────────┘
┌─────────────────────────────────┐
│  Tertiary Selector              │
│  Name attribute                 │
│  By.Name("firstname")           │
└────────────┬────────────────────┘
             │
         Success? ─ Yes ──────┐
             │                │
            No                ▼
             │            ┌─────────────┐
             ├──────────► │   Found!    │
             │            │ Use Element │
             ▼            └─────────────┘
┌─────────────────────────────────┐
│  Throw Exception                │
│  "Element not found"            │
└─────────────────────────────────┘
```

---

## 🎭 Page Object Model (POM) Pattern

### Why POM?

```
❌ WITHOUT POM (Maintenance Nightmare):
┌──────────────────────────────┐
│  Test 1                      │
│  By.Id("firstName")          │
└──────────────────────────────┘

┌──────────────────────────────┐
│  Test 2                      │
│  By.Id("firstName")          │
└──────────────────────────────┘

┌──────────────────────────────┐
│  Test 3                      │
│  By.Id("firstName")          │
└──────────────────────────────┘

When id changes from "firstName" to "input-001":
→ Must update ALL 3 tests manually! 😰


✅ WITH POM (Single Point of Change):
┌──────────────────────────────┐
│  Page Object                 │
│  FirstNameLocator()          │
│  By.XPath("//label[...]")    │
└──────────────────────────────┘
         ▲
    ┌────┴─────┬─────────┐
    │           │         │
 Test 1      Test 2    Test 3

When selector needs updating:
→ Update ONCE in page object! 🎉
```

### POM Principles Used

1. **Encapsulation**
   - All locators in PageObjects/
   - Tests don't know about XPath details
   - Easy to refactor without touching tests

2. **Separation of Concerns**
   - PageObjects: WHAT to locate
   - Tests: WHAT to test
   - BasePage: HOW to interact

3. **Reusability**
   - Common operations in BasePage
   - Multiple selectors per field
   - Fallback strategies

4. **Maintainability**
   - Single source of truth for locators
   - Clear method naming
   - Comprehensive documentation

---

## 🚀 Key Design Patterns

### 1. Factory Pattern
```csharp
// WebDriverFactory creates appropriate driver instance
public static IWebDriver CreateDriver(string browserType)
{
    return browserType switch
    {
        "Firefox" => CreateFirefoxDriver(),
        "Edge" => CreateEdgeDriver(),
        _ => CreateChromeDriver()
    };
}
```

### 2. Template Method Pattern
```csharp
// BasePage defines interaction template
protected void FillTextInput(By locator, string text)
{
    var element = WaitForElementToBeVisible(locator);  // Step 1
    ScrollToElement(element);                          // Step 2
    element.Clear();                                   // Step 3
    element.SendKeys(text);                            // Step 4
}
```

### 3. Inheritance Hierarchy
```csharp
BasePage                                    // Abstract base
  ↓
AutomationPracticeFormPage                  // Concrete implementation
  ↓
Test methods (PageObject methods)           // Usage
```

### 4. Retry Pattern
```csharp
// Wait strategies automatically retry
protected IWebElement WaitForElementToBeVisible(By locator)
{
    // Retries every ~500ms for up to 10 seconds
    return wait.Until(ExpectedConditions.VisibilityOfElementLocated(locator));
}
```

### 5. Try-Catch Error Handling
```csharp
protected bool IsElementDisplayed(By locator)
{
    try
    {
        return WaitForElementToBeVisible(locator).Displayed;
    }
    catch
    {
        return false;  // Graceful failure
    }
}
```

---

## 🔒 Resilience Mechanisms

### 1. Multi-Tier Selectors
Each field has 3 selector strategies with automatic fallback.

### 2. Explicit Waits
All operations wait for elements with timeout, preventing flaky tests.

### 3. Scroll Before Interaction
Elements scrolled into viewport before clicking/typing.

### 4. Element State Verification
Always verify element is ready before interaction.

### 5. Resource Cleanup
Proper WebDriver disposal in TearDown methods.

---

## 📊 Test Organization

### By Category
```
├── FirstName (4 tests)
│   ├── Visibility
│   ├── Text input
│   ├── Clear and re-input
│   └── Special characters
│
├── Email (5 tests)
│   ├── Visibility
│   ├── Enablement
│   ├── Valid format
│   ├── Multiple formats
│   └── Clear and re-input
│
├── Gender (5 tests)
│   ├── Male selection
│   ├── Female selection
│   ├── Other selection
│   ├── Change selection
│   └── Submission persistence
│
└── Integration (2 tests)
    ├── Complete submission
    └── Form load verification
```

### By Scope
```
Unit Tests
├── Individual field operations
└── Single interaction per test

Integration Tests
├── Multiple fields together
└── Complete form workflows
```

---

## 🛠️ Configuration Management

```csharp
// Central configuration for easy updates
public static class TestSettings
{
    public static class Browser
    {
        public const string DefaultBrowser = "Chrome";
        public const string FirefoxBrowser = "Firefox";
        public const string EdgeBrowser = "Edge";
    }

    public static class Timeouts
    {
        public const int DefaultTimeout = 10;
        public const int LongTimeout = 30;
        public const int ShortTimeout = 5;
    }

    public static class Urls
    {
        public const string FormUrl = "http://app.cloudqa.io/home/AutomationPracticeForm";
    }

    public static class TestData
    {
        public static class ValidEmails
        {
            public const string StandardEmail = "john.doe@example.com";
            // ...
        }
    }
}
```

---

## 📈 Scalability

### Adding New Fields
1. Create locators in PageObjects (3 strategies)
2. Create operation methods in PageObjects
3. Create tests using existing patterns
4. All infrastructure reusable

### Adding New Pages
1. Create new PageObject extending BasePage
2. Reuse all utility methods
3. Add form-specific locators
4. Tests follow same pattern

### Adding New Test Scenarios
1. Use existing page object methods
2. Combine operations as needed
3. Follow naming conventions
4. No changes to base framework needed

---

## 🔄 Continuous Integration

The framework is CI/CD ready:

```bash
# Build
dotnet build

# Restore
dotnet restore

# Test (with XML reporting)
dotnet test --logger "trx" --results-directory TestResults

# Specific category
dotnet test --filter "Category=FirstName"

# Parallel execution
dotnet test --parallel
```

---
