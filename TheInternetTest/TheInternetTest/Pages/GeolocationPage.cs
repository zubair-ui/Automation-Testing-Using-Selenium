using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TheInternetTest.Pages
{
    public class GeolocationPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        string geoUrl = "https://the-internet.herokuapp.com/geolocation";
        private By footerLinkSelector = By.CssSelector("#page-footer a");
        private By githubRibbonSelector = By.CssSelector("a[href='https://github.com/tourdedave/the-internet'] img[alt='Fork me on GitHub']");


        public GeolocationPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void ExecuteAllTests()
        {
            Console.WriteLine("\n-- Running Allowed Geolocation test --");
            // Geolocation Allowed
            var optionsAllow = new ChromeOptions();
            optionsAllow.AddUserProfilePreference("profile.default_content_setting_values.geolocation", 1);
            IWebDriver driverAllow = new ChromeDriver(optionsAllow);

            var geoPageAllow = new GeolocationPage(driverAllow);
            geoPageAllow.RunAllowedGeolocationTest();
            driverAllow.Quit();

            Console.WriteLine("\n-- Running Blocked Geolocation test --");
            var optionsBlock = new ChromeOptions();
            optionsBlock.AddUserProfilePreference("profile.default_content_setting_values.geolocation", 2);
            IWebDriver driverBlock = new ChromeDriver(optionsBlock);

            var geoPageBlock = new GeolocationPage(driverBlock);
            geoPageBlock.RunBlockedGeolocationTest();
            driverBlock.Quit();

            driver.Navigate().GoToUrl(geoUrl);

            Console.WriteLine("\n-- Verifying footer link --");
            VerifyFooterLink();

            Console.WriteLine("\n-- Verifying Github link --");
            VerifyGithubLink();
        }

        public void RunAllowedGeolocationTest()
        {
            string geoUrl = "https://the-internet.herokuapp.com/geolocation";
            driver.Navigate().GoToUrl(geoUrl);
            Console.WriteLine("Navigated to Geolocation page (Allowed).");

            try
            {
                var whereAmIButton = wait.Until(d => d.FindElement(By.TagName("button")));
                whereAmIButton.Click();
                Console.WriteLine("Clicked 'Where am I?' button.");

                // Wait until either lat-value is present or error message appears
                wait.Until(d =>
                {
                    var latElements = d.FindElements(By.CssSelector("#lat-value"));
                    var messageText = d.FindElement(By.Id("demo")).Text;
                    return latElements.Count > 0 || messageText.Contains("not supported");
                });

                // Now check if we got coordinates
                if (driver.FindElements(By.CssSelector("#lat-value")).Count > 0)
                {
                    string latitude = driver.FindElement(By.Id("lat-value")).Text.Trim();
                    string longitude = driver.FindElement(By.Id("long-value")).Text.Trim();

                    Console.WriteLine("Latitude: " + latitude);
                    Console.WriteLine("Longitude: " + longitude);

                    if (string.IsNullOrEmpty(latitude) || string.IsNullOrEmpty(longitude))
                        throw new Exception("Latitude or Longitude value is empty!");

                    var mapLink = driver.FindElement(By.Id("map-link")).FindElement(By.TagName("a"));
                    string mapUrl = mapLink.GetAttribute("href");

                    if (!mapUrl.Contains(latitude) || !mapUrl.Contains(longitude))
                        throw new Exception("Map link does not contain correct latitude and longitude.");

                    mapLink.Click();
                    Console.WriteLine("Clicked 'See it on Google' link.");


                    var tabs = driver.WindowHandles;
                    driver.SwitchTo().Window(tabs[1]);


                    string googleMapsUrl = driver.Url;
                    Console.WriteLine("Google Maps URL: " + googleMapsUrl);

                    if (!googleMapsUrl.Contains("google.com/maps") && !googleMapsUrl.Contains("q="))
                        throw new Exception("Did not navigate to expected Google Maps URL.");

                    driver.Close();
                    driver.SwitchTo().Window(tabs[0]);

                    Console.WriteLine("Allowed geolocation test passed.\n");
                }
                else
                {
                    string message = driver.FindElement(By.Id("demo")).Text.Trim();
                    Console.WriteLine("Geolocation not supported or blocked. Message: " + message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Allowed Geolocation test: " + ex.Message);
            }
        }


        public void RunBlockedGeolocationTest()
        {
            string geoUrl = "https://the-internet.herokuapp.com/geolocation";
            driver.Navigate().GoToUrl(geoUrl);
            Console.WriteLine("Navigated to Geolocation page (Blocked).");

            try
            {
                var whereAmIButton = wait.Until(d => d.FindElement(By.TagName("button")));
                whereAmIButton.Click();
                Console.WriteLine("Clicked 'Where am I?' button.");

                // Wait for either a message or a timeout
                wait.Until(d =>
                {
                    var messageText = d.FindElement(By.Id("demo")).Text;
                    return !string.IsNullOrWhiteSpace(messageText);
                });

                string message = driver.FindElement(By.Id("demo")).Text.Trim();
                Console.WriteLine("Blocked geolocation message: " + message);

                if (string.IsNullOrEmpty(message))
                    throw new Exception("No message displayed when geolocation is blocked.");

                Console.WriteLine("Blocked geolocation test passed.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Blocked Geolocation test: " + ex.Message);
            }
        }


        public void VerifyFooterLink()
        {
            try
            {
                var footerLink = driver.FindElement(footerLinkSelector);
                string linkText = footerLink.Text;
                string linkHref = footerLink.GetAttribute("href");

                if (linkText.Contains("Elemental Selenium"))
                {
                    Console.WriteLine($"Found footer link: {linkText}");
                    Console.WriteLine($"Link points to: {linkHref}");
                }
                else
                {
                    Console.WriteLine("Footer link text mismatch.");
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("'Powered by Elemental Selenium' link not found.");
            }
        }

        public void VerifyGithubLink()
        {
            try
            {
                var githubRibbon = driver.FindElement(githubRibbonSelector);
                var parentLink = githubRibbon.FindElement(By.XPath(".."));
                string href = parentLink.GetAttribute("href");

                if (href == "https://github.com/tourdedave/the-internet")
                {
                    Console.WriteLine("Found GitHub ribbon link.");
                    Console.WriteLine($"Link points to: {href}");
                }
                else
                {
                    Console.WriteLine("GitHub ribbon link href mismatch.");
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("GitHub ribbon link not found.");
            }
        }
    }
}
