using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TheInternetApp.Pages;
using TheInternetTest.Pages;

namespace TheInternetTest
{
    class Program
    {
        static void Main(string[] args)
        {
             
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            try
            {
                //RunHomePageTests(driver);
                //RunAbTestPageTests(driver);
                //RunAddRemoveElementsPageTests(driver);
                //RunBasicAuthPageTests(driver);
                //RunBrokenImagesPageTests(driver);
                //RunChallengingDomPageTests(driver);
                //RunCheckboxesPageTests(driver);
                //RunContextMenuPageTests(driver);
                //RunDigestAuthPageTests(driver);
                //RunDisappearingElementsPageTests(driver);
                //RunDragAndDropPageTests(driver);
                //RunDropdownPageTests(driver);
                //RunDynamicContentPageTests(driver);
                //RunDynamicControlsPageTests(driver);
                //RunDynamicLoadingPageTests(driver);
                //RunEntryAdPageTests(driver);
                //RunExitIntentPageTests(driver);
                //RunFileDownloadPageTests(driver);
                //RunFileUploadPageTests(driver);
                //RunFloatingMenuPageTests(driver);
                //RunForgotPasswordPageTests(driver);
                //RunFormAuthenticationPageTests(driver);
                //RunFramesPageTests(driver);
                //RunGeolocationPageTests(driver);
                //RunSliderPageTests(driver);
                //RunHoversPageTests(driver);
                //RunInfiniteScrollPageTests(driver);
                //RunInputsPageTests(driver);
                //RunJQueryUIMenuPageTests(driver);
                //RunJavaScriptAlertsPageTests(driver);
                //RunKeyPressesPageTests(driver);
                //RunLargeAndDeepDomPageTests(driver);
                //RunMultipleWindowsPageTests(driver);
                //RunFramesetPageTests(driver);
                RunNotificationMessagePageTests(driver);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test Exception: " + ex.Message);
            }
            finally
            { 
                driver.Quit();
                Console.WriteLine("Browser closed.");
            }

            Console.WriteLine("All tests done.");
        }

        // Function to execute homepage tests
        static void RunHomePageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Home Page Tests =====");

            HomePage homePage = new HomePage(driver);
            homePage.ExecuteAllTests();

            Console.WriteLine("===== Home Page Tests Completed =====\n");
        }

        // Function to execute A/B Test Page tests
        static void RunAbTestPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting A/B Test Page Tests =====");

            AbTestPage abTestPage = new AbTestPage(driver);
            abTestPage.ExecuteAllTests();

            Console.WriteLine("===== A/B Test Page Tests Completed =====\n");
        }

        // Function to execute Add/Remove Elements Page tests
        static void RunAddRemoveElementsPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Add/Remove Elements Page Tests =====");

            AddRemoveElementsPage addRemovePage = new AddRemoveElementsPage(driver);
            addRemovePage.ExecuteAllTests();

            Console.WriteLine("===== Add/Remove Elements Page Tests Completed =====\n");
        }

        // Function to execute Basic Auth Page tests
        static void RunBasicAuthPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Basic Auth Page Tests =====");

            BasicAuthPage basicAuthPage = new BasicAuthPage(driver);
            basicAuthPage.ExecuteAllTests();

            Console.WriteLine("===== Basic Auth Page Tests Completed =====\n");
        }

        // Function to execute Broken Images Page tests
        static void RunBrokenImagesPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Broken Images Page Tests =====");

            BrokenImagesPage brokenImagesPage = new BrokenImagesPage(driver);
            brokenImagesPage.ExecuteAllTests();

            Console.WriteLine("===== Broken Images Page Tests Completed =====\n");
        }

        // Function to execute Challenging DOM Page tests
        static void RunChallengingDomPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Challenging DOM Page Tests =====");

            ChallengingDomPage challengingDomPage = new ChallengingDomPage(driver);
            challengingDomPage.ExecuteAllTests();

            Console.WriteLine("===== Challenging DOM Page Tests Completed =====\n");
        }
        // Function to execute Challenging Checkboxes Page tests
        static void RunCheckboxesPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Checkboxes Page Tests =====");

            CheckboxesPage checkboxesPage = new CheckboxesPage(driver);
            checkboxesPage.ExecuteAllTests(); 

            Console.WriteLine("===== Checkboxes Page Tests Completed =====\n");
        }

        // Function to execute Context Menu Page tests
        static void RunContextMenuPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Context Menu Page Tests =====");

            ContextMenuPage contextMenuPage = new ContextMenuPage(driver);
            contextMenuPage.ExecuteAllTests();

            Console.WriteLine("===== Context Menu Page Tests Completed =====\n");
        }

        // Function to execute Digest Auth Page tests
        static void RunDigestAuthPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Digest Auth Page Tests =====");

            DigestAuthPage digestAuthPage = new DigestAuthPage(driver);
            digestAuthPage.ExecuteAllTests();

            Console.WriteLine("===== Digest Auth Page Tests Completed =====\n");
        }

        // Function to execute Disappearing Elements Page tests
        static void RunDisappearingElementsPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Disappearing Elements Page Tests =====");

            DisappearingElementsPage disappearingPage = new DisappearingElementsPage(driver);
            disappearingPage.ExecuteAllTests();

            Console.WriteLine("===== Disappearing Elements Page Tests Completed =====\n");
        }

        // Function to execute drag and drop page tests
        static void RunDragAndDropPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Drag And Drop Page Tests =====");

            DragAndDropPage dragAndDropPage = new DragAndDropPage(driver);
            dragAndDropPage.ExecuteAllTests();

            Console.WriteLine("===== Drag And Drop Page Tests Completed =====\n");
        }

        // Function to execute Dropdown page tests
        static void RunDropdownPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Dropdown Page Tests =====");

            DropdownPage dropdownPage = new DropdownPage(driver);
            dropdownPage.ExecuteAllTests();

            Console.WriteLine("===== Dropdown Page Tests Completed =====\n");
        }

        // Function to execute Dynamic Content page tests
        static void RunDynamicContentPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Dynamic Content Page Tests =====");

            var dynamicContentPage = new DynamicContentPage(driver);
            dynamicContentPage.ExecuteAllTests();
;

            Console.WriteLine("===== Dynamic Content Page Tests Completed =====\n");
        }

        // Function to execute Dynamic Controls page tests
        static void RunDynamicControlsPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Dynamic Controls Page Tests =====");

            DynamicControlsPage dynamicControlsPage = new DynamicControlsPage(driver);
            dynamicControlsPage.ExecuteAll();

            Console.WriteLine("===== Dynamic Controls Page Tests Completed =====\n");
        }

        // Function to execute Dynamic Loading page tests
        static void RunDynamicLoadingPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Dynamic Loading Page Tests =====");

            var dynamicLoadingPage = new DynamicLoadingPage(driver);
            dynamicLoadingPage.ExecuteAll();

            Console.WriteLine("===== Dynamic Loading Page Tests Completed =====\n");
        }

        // Function to execute Entry Ad page tests
        static void RunEntryAdPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Entry Ad Page Tests =====");

            var entryAdPage = new TheInternetTest.Pages.EntryAdPage(driver);
            entryAdPage.ExecuteAll();

            Console.WriteLine("===== Entry Ad Page Tests Completed =====\n");
        }

        // Function to execute Exit Intent page tests
        static void RunExitIntentPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Exit Intent Page Tests =====");

            ExitIntentPage exitIntentPage = new ExitIntentPage(driver);
            exitIntentPage.ExecuteAllTests();

            Console.WriteLine("===== Exit Intent Page Tests Completed =====\n");
        }

        // Function to execute file download page tests
        static void RunFileDownloadPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting File Download Page Tests =====");

            FileDownloadPage fileDownloadPage = new FileDownloadPage(driver);
            fileDownloadPage.ExecuteAllTests();

            Console.WriteLine("===== File Download Page Tests Completed =====\n");
        }

        // Function to execute file upload page tests
        static void RunFileUploadPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting File Upload Page Tests =====");

            FileUploadPage fileUploadPage = new FileUploadPage(driver);
            fileUploadPage.ExecuteAllTests();

            Console.WriteLine("===== File Upload Page Tests Completed =====\n");
        }

        // Function to execute Floating Menu page tests
        static void RunFloatingMenuPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Floating Menu Page Tests =====");

            FloatingMenuPage floatingMenuPage = new FloatingMenuPage(driver);
            floatingMenuPage.ExecuteAllTests();

            Console.WriteLine("===== Floating Menu Page Tests Completed =====\n");
        }

        // Function to execute Forgot Password page tests
        static void RunForgotPasswordPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Forgot Password Page Tests =====");

            ForgotPasswordPage forgotPasswordPage = new ForgotPasswordPage(driver);
            forgotPasswordPage.ExecuteAllTests();

            Console.WriteLine("===== Forgot Password Page Tests Completed =====\n");
        }

        // Function to execute Forgot Password page tests
        static void RunFormAuthenticationPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Form Authentication Page Tests =====");

            FormAuthenticationPage formPage = new FormAuthenticationPage(driver);
            formPage.ExecuteAllTests();

            Console.WriteLine("===== Form Authentication Page Tests Completed =====\n");
        }

        // Function to execute Frames page tests
        static void RunFramesPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Frames Page Tests =====");

            FramesPage framesPage = new FramesPage(driver);
            framesPage.ExecuteAllTests();

            Console.WriteLine("===== Frames Page Tests Completed =====\n");
        }

        // Function to execute Geolocation page tests
        static void RunGeolocationPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Geolocation Page Tests =====");

            GeolocationPage geoPage = new GeolocationPage(driver);
            geoPage.ExecuteAllTests();


            Console.WriteLine("===== Geolocation Tests Completed =====\n");
        }

        // Function to execute Slider page tests
        static void RunSliderPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Slider Page Tests =====");

            SliderPage sliderPage = new SliderPage(driver);
            sliderPage.ExecuteAllTests();

            Console.WriteLine("===== Slider Page Tests Completed =====\n");
        }

        // Function to execute Hovers page tests
        static void RunHoversPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Hovers Page Tests =====");

            var hoversPage = new HoversPage(driver);
            hoversPage.ExecuteAllTests();

            Console.WriteLine("===== Hovers Page Tests Completed =====\n");
        }

        // Function to execute Infinite Scroll page tests
        static void RunInfiniteScrollPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Infinite Scroll Page Tests =====");

            var infiniteScrollPage = new InfiniteScrollPage(driver);
            infiniteScrollPage.ExecuteAllTests();

            Console.WriteLine("===== Infinite Scroll Page Tests Completed =====\n");
        }

        // Function to execute Inputs page tests
        static void RunInputsPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Inputs Page Tests =====");

            InputsPage inputsPage = new InputsPage(driver);
            inputsPage.ExecuteAllTests();

            Console.WriteLine("===== Inputs Page Tests Completed =====\n");
        }

        // Function to execute jQuery UI Menu page tests
        static void RunJQueryUIMenuPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting jQuery UI Menu Page Tests =====");

            JQueryUIMenuPage menuPage = new JQueryUIMenuPage(driver);
            menuPage.ExecuteAllTests();

            Console.WriteLine("===== jQuery UI Menu Page Tests Completed =====\n");
        }

        // Function to execute JavaScript Alerts page tests
        static void RunJavaScriptAlertsPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting JavaScript Alerts Page Tests =====");

            JavaScriptAlertsPage jsAlertsPage = new JavaScriptAlertsPage(driver);
            jsAlertsPage.ExecuteAllTests();

            Console.WriteLine("===== JavaScript Alerts Page Tests Completed =====\n");
        }

        // Function to execute Key Presses page tests
        static void RunKeyPressesPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Key Presses Page Tests =====");

            KeyPressesPage keyPressesPage = new KeyPressesPage(driver);
            keyPressesPage.ExecuteAllTests();

            Console.WriteLine("===== Key Presses Page Tests Completed =====\n");
        }

        // Function to execute Large & Deep DOM page tests
        static void RunLargeAndDeepDomPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Large & Deep DOM Page Tests =====");

            LargeAndDeepDomPage largeDomPage = new LargeAndDeepDomPage(driver);
            largeDomPage.ExecuteAllTests();

            Console.WriteLine("===== Large & Deep DOM Page Tests Completed =====\n");
        }

        // Function to execute Multiple Windows page tests
        static void RunMultipleWindowsPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Multiple Windows Page Tests =====");

            MultipleWindowsPage multipleWindowsPage = new MultipleWindowsPage(driver);
            multipleWindowsPage.ExecuteAllTests();

            Console.WriteLine("===== Multiple Windows Page Tests Completed =====\n");
        }

        // Function to execute Frameset page tests
        static void RunFramesetPageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Frameset Page Tests =====");

            NestedFramesPage framesetPage = new NestedFramesPage(driver);
            framesetPage.ExecuteAllTests();

            Console.WriteLine("===== Frameset Page Tests Completed =====\n");
        }

        // Function to execute Notification Message page tests
        static void RunNotificationMessagePageTests(IWebDriver driver)
        {
            Console.WriteLine("===== Starting Notification Message Page Tests =====");

            NotificationMessagePage notificationPage = new NotificationMessagePage(driver);
            notificationPage.ExecuteAllTests();

            Console.WriteLine("===== Notification Message Page Tests Completed =====\n");
        }

    }
}
