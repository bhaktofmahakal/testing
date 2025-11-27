# CloudQA Selenium Test Framework

A production-grade, resilient automated test suite for the CloudQA Automation Practice Form using C# and Selenium WebDriver with NUnit and Page Object Model (POM) architecture.

---

## 📋 Project Overview

This test framework demonstrates **industry best practices** for automated UI testing, focusing on **resilience to HTML and position changes**. It includes comprehensive test coverage for three critical form fields:

1. **First Name Field** (Text Input)
2. **Email Field** (Email Input)
3. **Gender Field** (Radio Button Group)

---

## 🏗️ Project Architecture

```
CloudQA.Tests/
├── PageObjects/
│   ├── BasePage.cs                 # Base class with common wait strategies
│   └── AutomationPracticeFormPage.cs  # Page Object for the form
├── Tests/
│   └── AutomationPracticeFormTests.cs  # Test cases (16 tests total)
├── Utilities/
│   └── WebDriverFactory.cs         # WebDriver initialization
├── Configuration/
│   └── TestSettings.cs             # Configuration constants
├── CloudQA.Tests.csproj            # Project file
└── README.md                        # This file
```

---

## 🔍 Locating Strategy - Resilience to Changes

### Why This Approach?

Traditional selectors fail when:
- **Position changes**: Elements move in the DOM
- **HTML attributes change**: `id` or `class` values are modified
- **Layout restructures**: Parent-child relationships change

### Robust Selector Techniques Used

#### 1. **XPath with Visible Text** (Most Resilient)
```csharp
// Instead of: By.Id("firstName") or By.ClassName("form-input")
// We use:
By FirstNameLocator() => By.XPath(
    "//label[contains(text(), 'First Name')]//following::input[@type='text'][1]"
);
```

**Benefits:**
- Works even if `id` or `class` changes
- Finds elements by user-visible labels
- Hierarchy-based (label → following input)
- Handles typos with `contains()` instead of exact text match

#### 2. **Fallback Selectors**
Multiple selector strategies for maximum resilience:
```csharp
private By FirstNameLocator()          // Primary: XPath with label text
private By FirstNameByPlaceholder()    // Secondary: Placeholder attribute
private By FirstNameByName()           // Tertiary: Name attribute
```

If one selector fails, alternative selectors provide backup access.

#### 3. **Structural CSS Selectors**
```csharp
// Finds elements by their position in the DOM hierarchy
// Independent of `id`, `class`, or attribute values
```

#### 4. **Radio Button Selection**
```csharp
By MaleRadioButton() => By.XPath(
    "//label[contains(text(), 'Male')]//preceding::input[@type='radio'][1]"
);
```

Uses label text combined with XPath axes (`preceding::`, `following::`) for position-independent selection.

---

## ✅ Test Coverage - 16 Comprehensive Tests

### **First Name Field (4 tests)**
- ✅ Field visibility verification
- ✅ Text input acceptance
- ✅ Clear and re-input behavior
- ✅ Special character handling

### **Email Field (5 tests)**
- ✅ Field visibility verification
- ✅ Field enablement validation
- ✅ Standard email format acceptance
- ✅ Parameterized tests for multiple email formats
- ✅ Clear and re-input behavior

### **Gender Field (5 tests)**
- ✅ Male radio button selection
- ✅ Female radio button selection
- ✅ Other radio button selection
- ✅ Radio button switch behavior
- ✅ Selection persistence with form submission

### **Integration Tests (2 tests)**
- ✅ Complete form submission with all three fields
- ✅ Form load verification with all required fields

---

## 🚀 Prerequisites & Setup

### System Requirements
- **Windows 10 or later** (tested on Windows 11)
- **.NET 8.0 SDK** or later
- **Google Chrome**, **Firefox**, or **Edge** (WebDriverManager handles driver downloads)

### Installation Steps

1. **Install .NET SDK** (if not already installed)
   - Download from: https://dotnet.microsoft.com/download
   - Verify installation: `dotnet --version`

2. **Clone/Extract Project**
   ```bash
   cd CloudQA.Tests
   ```

3. **Restore NuGet Packages**
   ```bash
   dotnet restore
   ```

---

## 🏃 Running the Tests

### **Option 1: Run All Tests**
```bash
dotnet test
```

### **Option 2: Run Specific Test Category**
```bash
# Run only First Name tests
dotnet test --filter "Category=FirstName"

# Run only Email tests
dotnet test --filter "Category=Email"

# Run only Gender tests
dotnet test --filter "Category=Gender"

# Run integration tests
dotnet test --filter "Category=Integration"
```

### **Option 3: Run Specific Test**
```bash
dotnet test --filter "Name~Test_FirstNameField_FillsWithValidText"
```

### **Option 4: Run with Verbose Output**
```bash
dotnet test --verbosity detailed
```

### **Option 5: Run with Different Browser**
Modify the `Setup()` method in `AutomationPracticeFormTests.cs`:
```csharp
[SetUp]
public void Setup()
{
    driver = WebDriverFactory.CreateDriver("Firefox");  // or "Edge"
    // ...
}
```

---

## 📊 Expected Test Results

After running the tests, you should see output similar to:
```
Passed:  16
Failed:  0
Skipped: 0
Total:   16
```

Each test will:
1. Launch a browser instance
2. Navigate to the form page
3. Perform the specified interaction
4. Assert expected outcomes
5. Close the browser

---

## 🛡️ Resilience Features

### 1. **Explicit Waits (Not Sleep)**
```csharp
protected IWebElement WaitForElementToBeVisible(By locator)
{
    return wait.Until(ExpectedConditions.VisibilityOfElementLocated(locator));
}
```
- Waits up to 10 seconds for elements to appear
- Exits immediately when element is found
- Prevents flaky tests due to timing issues

### 2. **Scroll Before Interaction**
```csharp
protected void ScrollToElement(IWebElement element)
{
    var js = (IJavaScriptExecutor)driver;
    js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
}
```
- Ensures elements are in viewport before clicking
- Handles elements below the fold
- Prevents "element not clickable" errors

### 3. **Multiple Selector Strategies**
- Primary, secondary, and tertiary selectors
- Graceful fallback if one selector fails
- Adapts to minor DOM structure changes

### 4. **Error Handling**
All operations include try-catch blocks:
```csharp
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
```

---

## 🔧 Extending the Framework

### Add a New Test
1. Create a new test method in `AutomationPracticeFormTests.cs`
2. Use the existing helper methods from `BasePage`
3. Follow the naming convention: `Test_[FieldName]_[Behavior]`

Example:
```csharp
[Test]
[Category("FirstName")]
public void Test_FirstNameField_ValidatesMinLength()
{
    formPage.FillFirstName("A");
    // Assert expected behavior
}
```

### Add a New Field
1. Add locator methods to `AutomationPracticeFormPage.cs`
2. Add interaction methods (FillField, GetValue, IsSelected, etc.)
3. Create test methods in `AutomationPracticeFormTests.cs`

### Use Different Browser
Modify `WebDriverFactory.cs` or pass browser type:
```csharp
driver = WebDriverFactory.CreateDriver("Firefox");
```

---

## 📝 Key Dependencies

- **Selenium.WebDriver** (4.15.0) - WebDriver API
- **Selenium.Support** (4.15.0) - SelectElement, WebDriverWait
- **WebDriverManager** (2.16.2) - Automatic browser driver management
- **NUnit** (4.0.1) - Test framework
- **Microsoft.NET.Test.Sdk** (17.8.2) - Test infrastructure

---

## 🐛 Troubleshooting

### **Tests fail with "Element not found"**
- Verify the target page URL is accessible
- Check that the form elements exist on the page
- Review selector XPath in browser Developer Tools (F12)

### **Browser won't launch**
- Ensure Chrome/Firefox/Edge is installed
- WebDriverManager will auto-download drivers
- Check firewall/proxy settings

### **Tests timeout**
- Increase `DefaultTimeout` in `BasePage.cs` (if page is slow)
- Verify internet connection to target URL
- Check if elements load dynamically

### **WebDriver errors**
- Run: `dotnet restore` to reinstall dependencies
- Delete `bin/` and `obj/` folders and rebuild
- Try with a different browser

---

## 📦 Deliverables Included

- ✅ **Source Code** - Complete C# Selenium test framework
- ✅ **Page Object Model** - Maintainable, scalable architecture
- ✅ **Robust Selectors** - XPath with label text, fallback selectors
- ✅ **16 Test Cases** - Comprehensive coverage
- ✅ **Error Handling** - Graceful failure management
- ✅ **Documentation** - This README with complete instructions
- ✅ **Production Quality** - Follows SOLID principles and best practices

---

## 📄 Locating Strategy Summary

| Field | Primary Selector | Strategy |
|-------|------------------|----------|
| **First Name** | XPath with label text | `//label[contains(text(), 'First Name')]//following::input[@type='text'][1]` |
| **Email** | XPath with label text | `//label[contains(text(), 'Email')]//following::input[@type='email'][1]` |
| **Gender** | XPath label + preceding axis | `//label[contains(text(), 'Male')]//preceding::input[@type='radio'][1]` |

**Key Advantages:**
- ✅ Resilient to position changes
- ✅ Resilient to HTML attribute changes
- ✅ Resilient to class/id modifications
- ✅ Based on user-visible text (semantic)
- ✅ Multiple fallback strategies

---

## ✨ Contact & Support

For questions about the framework or locating strategies, refer to:
- Selenium Documentation: https://www.selenium.dev/documentation/
- NUnit Documentation: https://docs.nunit.org/
- XPath Tutorial: https://www.w3schools.com/xml/xpath_intro.asp

---

**Framework Version:** 1.0  
**Last Updated:** November 2025  
**Status:** Production Ready
