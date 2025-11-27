namespace CloudQA.Tests.Configuration
{
    public class TestSettings
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
            public const string BaseUrl = "http://app.cloudqa.io";
            public const string FormUrl = "http://app.cloudqa.io/home/AutomationPracticeForm";
        }

        public static class TestData
        {
            public static class ValidNames
            {
                public const string FirstName = "John";
                public const string LastName = "Doe";
                public const string NameWithSpecialCharacters = "Jean-Pierre O'Connor";
            }

            public static class ValidEmails
            {
                public const string StandardEmail = "john.doe@example.com";
                public const string EmailWithSubdomain = "user.name@subdomain.example.co.uk";
                public const string EmailWithPlus = "user+tag@example.org";
            }

            public static class GenderOptions
            {
                public const string Male = "Male";
                public const string Female = "Female";
                public const string Other = "Other";
            }
        }
    }
}
