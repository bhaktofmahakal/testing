using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverManager;
using WebDriverManager.Helpers;

namespace CloudQA.Tests.Utilities
{
    public class WebDriverFactory
    {
        public static IWebDriver CreateDriver(string browserType = "Chrome")
        {
            return browserType.ToLower() switch
            {
                "firefox" => CreateFirefoxDriver(),
                "edge" => CreateEdgeDriver(),
                _ => CreateChromeDriver()
            };
        }

        private static IWebDriver CreateChromeDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.ExcludeArgument("enable-automation");
            options.AddExcludedArgument("enable-logging");
            return new ChromeDriver(options);
        }

        private static IWebDriver CreateFirefoxDriver()
        {
            new DriverManager().SetUpDriver(new FirefoxConfig(), VersionResolveStrategy.MatchingBrowser);
            var options = new FirefoxOptions();
            return new FirefoxDriver(options);
        }

        private static IWebDriver CreateEdgeDriver()
        {
            new DriverManager().SetUpDriver(new EdgeConfig(), VersionResolveStrategy.MatchingBrowser);
            var options = new EdgeOptions();
            options.AddArgument("--start-maximized");
            return new EdgeDriver(options);
        }
    }
}
