using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace EtSistemTest
{
    [TestFixture]
    public class UserTests
    {
        static IWebDriver Driver;

        [OneTimeSetUp]
        public void Setup()
        {
            Driver = new ChromeDriver(@"C:\chromedriver_win32");

            Console.WriteLine("Test started...");
        }
        
        [Test]
        public void Test001_UserFailLogin()
        {
            Driver.Navigate().GoToUrl("https://kurall.com");

            var txtUserName = Driver.FindElement(By.Id("username"));
            var txtPassword = Driver.FindElement(By.Id("password"));
            var btnLogin = Driver.FindElement(By.XPath("/html/body/section/div/div/div/div/div/div[2]/div/form/div[4]/button"));

            txtUserName.SendKeys("mustafakural@outlook.com");
            txtPassword.SendKeys("3535");
            btnLogin.Submit();

            var lblInvalidUserOrPassword = Driver.FindElement(By.XPath("/html/body/section/div/div/div/div/div/div[2]/div/div/div/ul/li"));

            Assert.That(lblInvalidUserOrPassword.Displayed, Is.True, "Hatalý kullanýcý yada parola mesajý görünmeli");

            Assert.Pass();
        }

        [Test]
        public void Test002_UserSuccessLogin()
        {
            Driver.Navigate().GoToUrl("https://kurall.com");

            var txtUserName = Driver.FindElement(By.Id("username"));
            var txtPassword = Driver.FindElement(By.Id("password"));
            var btnLogin = Driver.FindElement(By.XPath("/html/body/section/div/div/div/div/div/div[2]/div/form/div[4]/button"));

            txtUserName.SendKeys("mustafakural@outlook.com");
            txtPassword.SendKeys("1234");
            btnLogin.Submit();
            
            var lnkReadBarcode = Driver.FindElement(By.XPath("//*[@id='content']/div[1]/div/a"));

            Assert.That(lnkReadBarcode.Displayed, Is.True, "Giriþ yapýldýysa barkod okuma butonu görünmeli");

            Assert.Pass();
        }
       
        [Test]
        public void Test003_UserMustNotAccessUserList()
        {
            Driver.Navigate().GoToUrl("https://kurall.com/User/List");

            Assert.IsTrue(!Driver.Url.EndsWith("/User/List"),"Kullanýcý, kullanýcýlar sayfasýna eriþemez");

            Assert.Pass();
        }

        [Test]
        public void Test004_UserLogout()
        {
            Driver.Navigate().GoToUrl("https://kurall.com/User/");
            
            var lnkUser = Driver.FindElement(By.XPath("/html/body/header/nav/div/div/ul[2]/li/a"));
            var lnkUserLogout = Driver.FindElement(By.XPath("/html/body/header/nav/div/div/ul[2]/li/ul/li[3]/a"));

            Assert.That(lnkUser.Displayed, Is.True, "Kullanýcý menüsü görünmeli");

            lnkUser.Click();

            lnkUserLogout.Click();

            var btnLogin = Driver.FindElement(By.XPath("/html/body/section/div/div/div/div/div/div[2]/div/form/div[4]/button"));

            Assert.That(btnLogin.Displayed, Is.True, "Giriþ sayfasý gelmeli");

            Assert.Pass();
        }
        
        [OneTimeTearDown]
        public void CloseDriver()
        {
            Thread.Sleep(3000);
            Driver.Close();
        }
    }
}