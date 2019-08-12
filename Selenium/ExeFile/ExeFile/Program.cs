using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            GenCodeFromMember();
        }
        public static void GenCodeFromMember()
        {
            //options.BracingStyle = "C";
            //CodeMemberMethod method1 = new CodeMemberMethod();
            //method1.Name = "ReturnString";
            //method1.Attributes = MemberAttributes.Public;
            //method1.ReturnType = new CodeTypeReference("System.String");
            //method1.Parameters.Add(new CodeParameterDeclarationExpression("System.String", "text"));
            //method1.Statements.Add(new CodeMethodReturnStatement(new CodeArgumentReferenceExpression("text")));
            //StringWriter sw = new StringWriter();
            //provider.GenerateCodeFromMember(method1, sw, options);
            //CodeSnippetTypeMember snippetMethod = new CodeSnippetTypeMember(sw.ToString());
            //snippetMethod.

            //הקוד שיבוא מהתוסף
            string code = "";
            ICodeCompiler compiler = new CSharpCodeProvider().CreateCompiler();
            CompilerParameters parameters = new CompilerParameters();
            // Start by adding any referenced assemblies
            parameters.ReferencedAssemblies.Add(" Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll");
            parameters.ReferencedAssemblies.Add("System.dll");
            // Must create a fully functional assembly as a string
            //            code = @"using System;
            //using System.IO;
            //namespace MyNamespace {
            //public class MyClass {
            //public object DynamicCode() {
            //return 2;
            // } } }";
            code = @"using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestClass]
    public class Kata
    {
        private static IWebDriver driver;
        private StringBuilder verificationErrors;
        private static string baseURL;
        private bool acceptNextAlert = true;

        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            driver = new ChromeDriver();
            baseURL = 'https://www.katalon.com/';
        }

        [ClassCleanup]
        public static void CleanupClass()
        {
            try
            {
                //driver.Quit();// quit does not close the window
                driver.Close();
                driver.Dispose();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        [TestInitialize]
        public void InitializeTest()
        {
            verificationErrors = new StringBuilder();
        }

        [TestCleanup]
        public void CleanupTest()
        {
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [TestMethod]
        public void TheKataTest()
        {
            driver.Navigate().GoToUrl('https://www.google.co.il/search?q=selmnuim&oq=se&aqs=chrome.0.69i59j69i57j69i60l2j69i61.2421j0j7&sourceid=chrome&ie=UTF-8');
            driver.FindElement(By.XPath('(.//*[normalize-space(text()) and normalize-space(.)='תוצאות באינטרנט'])[2]/following::span[1]')).Click();
            driver.FindElement(By.LinkText('ChromeDriver')).Click();
            driver.FindElement(By.LinkText('ChromeDriver')).Click();
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
";




            //public object DynamicCode(params object[] Parameters) {
            // Load the resulting assembly into memory
            parameters.GenerateInMemory = false;
            // Now compile the whole thing
            CompilerResults compilerResults =
              compiler.CompileAssemblyFromSource(parameters, code);
            if (compilerResults.Errors.HasErrors)
            {
                string errorMsg = "";
                errorMsg = compilerResults.Errors.Count.ToString() +
                             " Errors:";
                for (int x = 0; x < compilerResults.Errors.Count; x++)
                    errorMsg = errorMsg + "\r\nLine: " +
                                 compilerResults.Errors[x].Line.ToString() +
                                  " - " + compilerResults.Errors[x].ErrorText;
                Console.WriteLine("Compiler Demo" + "\n" + errorMsg + "\r\n\r\n" + code);
                return;
            }
            Assembly assembly = compilerResults.CompiledAssembly;
            // Retrieve an obj ref - generic type only
            object o =
                   assembly.CreateInstance("MyNamespace.MyClass");
            if (o == null)
            {
                //MessageBox.Show("Couldn't load class.");
                return;
            }
            //object[] loCodeParms = new object[0];
            //loCodeParms[0] = "West Wind Technologies";
            try
            {
                object result = o.GetType().InvokeMember(
                                 "DynamicCode", BindingFlags.InvokeMethod,
                                 null, o, null);//, loCodeParms);
                                                //DateTime ltNow = (DateTime)loResult;
                                                //MessageBox.Show("Method Call Result:\r\n\r\n" +
                                                //                loResult.ToString(), "Compiler Demo");
                Console.WriteLine("Dynamic Code Result" + result.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Compiler Demo" + "\n" + ex.Message);
            }
        }
    }
}