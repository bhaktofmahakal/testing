# Quick Start Guide - CloudQA Selenium Tests

## 🚀 30-Second Setup

### Prerequisites
- **Windows 10+** with PowerShell or Command Prompt
- **.NET 8.0 SDK** installed (https://dotnet.microsoft.com/download)
- **Chrome, Firefox, or Edge** browser

### Step 1: Verify .NET Installation
```bash
dotnet --version
```
Should output: `8.0.xxx` or higher

### Step 2: Restore Dependencies
```bash
cd CloudQA.Tests
dotnet restore
```

### Step 3: Run Tests
```bash
dotnet test
```

**That's it!** ✅

---

## ⚡ Quick Commands

### Run All Tests (16 tests)
```bash
dotnet test
```

### Run Single Test Category
```bash
# First Name tests only
dotnet test --filter "Category=FirstName"

# Email tests only
dotnet test --filter "Category=Email"

# Gender tests only
dotnet test --filter "Category=Gender"

# Integration tests only
dotnet test --filter "Category=Integration"
```

### Using Batch Scripts (Windows)
```bash
run-all-tests.bat
run-firstname-tests.bat
run-email-tests.bat
run-gender-tests.bat
run-integration-tests.bat
```

---

## 📊 Expected Output

```
Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed: 0, Passed: 16, Skipped: 0, Total: 16
```

---

## 🎯 Three Fields Tested

| # | Field | Type | Tests |
|---|-------|------|-------|
| 1 | **First Name** | Text Input | 4 tests |
| 2 | **Email** | Email Input | 5 tests |
| 3 | **Gender** | Radio Button | 5 tests |
| - | **Integration** | Combined | 2 tests |

---

## 🔍 Resilience Features

### ✅ Resilient to HTML Changes
Tests will **continue working** even if:
- Element `id` changes
- Element `class` changes
- DOM position changes
- Parent-child structure changes

**Why?** Using XPath selectors based on:
- Visible text labels
- Structural hierarchy
- Element type attributes

### Example: First Name Selector
```csharp
// ❌ Breaks if id="firstName" changes
By firstName = By.Id("firstName");

// ✅ Works even if id/class changes
By firstName = By.XPath(
    "//label[contains(text(), 'First Name')]//following::input[@type='text'][1]"
);
```

---

## 🛠️ Project Structure

```
CloudQA.Tests/
├── PageObjects/
│   ├── BasePage.cs                    ← Common test utilities
│   └── AutomationPracticeFormPage.cs  ← Form interactions
├── Tests/
│   └── AutomationPracticeFormTests.cs ← 16 test cases
├── Utilities/
│   └── WebDriverFactory.cs            ← Browser management
├── Configuration/
│   └── TestSettings.cs                ← Config constants
├── CloudQA.Tests.csproj               ← Project definition
├── README.md                          ← Full documentation
└── QUICK_START.md                     ← This file
```

---

## 🔧 Changing Browser

Edit `Tests/AutomationPracticeFormTests.cs`:

```csharp
[SetUp]
public void Setup()
{
    // Change "Chrome" to "Firefox" or "Edge"
    driver = WebDriverFactory.CreateDriver("Chrome");
    formPage = new AutomationPracticeFormPage(driver);
    formPage.NavigateToPage();
}
```

Options: `"Chrome"`, `"Firefox"`, `"Edge"`

---

## ❓ FAQ

**Q: Tests timeout or fail?**  
A: Check that `app.cloudqa.io` is accessible from your network. Increase `DefaultTimeout` in `BasePage.cs` if needed.

**Q: Which test framework is this?**  
A: **NUnit 4.0.1** with **Selenium WebDriver 4.15.0** and **WebDriverManager 2.16.2**

**Q: Can I add more tests?**  
A: Yes! Follow the pattern in `AutomationPracticeFormTests.cs`. Use Page Object methods from `AutomationPracticeFormPage.cs`.

**Q: Can I use this for other forms?**  
A: Yes! Copy `PageObjects/BasePage.cs` and create new page classes for other forms.

---

## 📚 Learn More

See **README.md** for:
- Detailed locating strategy explanation
- All 16 test case descriptions
- Extending the framework
- Troubleshooting guide

---

**Status:** ✅ Production Ready  
**Test Framework:** NUnit 4.0.1  
**Selenium:** 4.15.0  
**Browser Support:** Chrome, Firefox, Edge
