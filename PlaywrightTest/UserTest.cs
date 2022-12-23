using NUnit.Framework;
using Microsoft.Playwright.NUnit;
using System.Threading.Tasks;
using Microsoft.Playwright;
using System.Threading;
using System.Text.RegularExpressions;
using System;

namespace PlaywrightTest
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class UserTests
    {
        static IBrowser Browser;
        static IBrowserContext Context;
        static IPage Page;

        [OneTimeSetUp]
        public async Task Setup()
        {
            var pw = await Playwright.CreateAsync();

            Browser = await pw.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });

            Context = await Browser.NewContextAsync();
           
            Page = await Browser.NewPageAsync();

            Console.WriteLine("Test started...");
        }

        [Test]
        public async Task Test001_UserFailLogin()
        {
            await Page.GotoAsync("https://kurall.com");
            var btnLogin = Page.Locator("xpath=/html/body/section/div/div/div/div/div/div[2]/div/form/div[4]/button");

           
            await Page.Locator("id=username").FillAsync("mustafakural@outlook.com");
            await Page.Locator("id=password").FillAsync("3535");

            await btnLogin.ClickAsync();

            var lblInvalidUserOrPassword = Page.Locator("xpath=/html/body/section/div/div/div/div/div/div[2]/div/div/div/ul/li");

            await Assertions.Expect(lblInvalidUserOrPassword).ToBeVisibleAsync();

            Assert.Pass();

            Thread.Sleep(1000);
        }

        [Test]
        public async Task Test002_UserSuccessLogin()
        {
            await Page.GotoAsync("https://kurall.com");
            var btnLogin = Page.Locator("xpath=/html/body/section/div/div/div/div/div/div[2]/div/form/div[4]/button");


            await Page.Locator("id=username").FillAsync("mustafakural@outlook.com");
            await Page.Locator("id=password").FillAsync("1234");

            await btnLogin.ClickAsync();

            var btnReadBarcode = Page.Locator("xpath=//*[@id='content']/div[1]/div/a");


            await Assertions.Expect(btnReadBarcode).ToBeVisibleAsync();

            Assert.Pass();

            Thread.Sleep(1000);
        }

        [Test]
        public async Task Test003_UserMustNotAccessUserList()
        {
            await Page.GotoAsync("https://kurall.com/User/List");
            await Assertions.Expect(Page).Not.ToHaveURLAsync(new Regex(".*/User/List"));

            Assert.Pass();
        }

        [Test]
        public async Task Test004_UserLogout()
        {
            await Page.GotoAsync("https://kurall.com/User/");

            var lnkUser = Page.Locator("xpath=/html/body/header/nav/div/div/ul[2]/li/a");
            var lnkUserLogout = Page.Locator("xpath=/html/body/header/nav/div/div/ul[2]/li/ul/li[3]/a");

            await Assertions.Expect(lnkUser).ToBeVisibleAsync();

            await lnkUser.ClickAsync();

            await lnkUserLogout.ClickAsync();

            var btnLogin = Page.Locator("xpath=/html/body/section/div/div/div/div/div/div[2]/div/form/div[4]/button");

            await Assertions.Expect(btnLogin).ToBeVisibleAsync();
            
            Assert.Pass();

            Thread.Sleep(1000);
        }

        [OneTimeTearDown]
        public void CloseDriver()
        {
            Thread.Sleep(3000);
            Browser.CloseAsync();
        }
    }
}