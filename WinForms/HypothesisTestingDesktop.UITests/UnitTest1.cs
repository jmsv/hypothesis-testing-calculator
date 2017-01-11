using System;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using System.IO;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HypothesisTestingDesktop.UITests
{
    [TestClass]
    public class UnitTest1
    {
        bool SlowedTests = false;

        [TestMethod]
        public void TestMethod01()
        {
            ConductTestWith("20", "0.5", "14", "5", 2, SlowedTests);
        }

        [TestMethod]
        public void TestMethod02()
        {
            ConductTestWith("20", "0.5", "15", "5", 2, SlowedTests);
        }

        [TestMethod]
        public void TestMethod03()
        {
            ConductTestWith("10", "0.5", "2", "5", 1, SlowedTests);
        }

        [TestMethod]
        public void TestMethod04()
        {
            ConductTestWith("10", "0.5", "2", "10", 1, SlowedTests);
        }

        [TestMethod]
        public void TestMethod05()
        {
            ConductTestWith("14", "0.2", "17", "5", 3, SlowedTests);
        }

        [TestMethod]
        public void TestMethod06()
        {
            ConductTestWith("20", "0.5", "10", "5", 0, SlowedTests);
        }

        [TestMethod]
        public void TestMethod07()
        {
            ConductTestWith("16", "1.1", "4", "2", 2, SlowedTests);
        }

        [TestMethod]
        public void TestMethod08()
        {
            ConductTestWith("20", "0.8", "15", "104", 2, SlowedTests);
        }

        [TestMethod]
        public void TestMethod09()
        {
            ConductTestWith("18", "0", "2", "5", 1, SlowedTests);
        }

        [TestMethod]
        public void TestMethod10()
        {
            ConductTestWith("18", "1", "12", "5", 1, SlowedTests);
        }

        [TestMethod]
        public void TestMethod11()
        {
            ConductTestWith("10", "0.5", "6", "100", 1, SlowedTests);
        }

        [TestMethod]
        public void TestMethod12()
        {
            ConductTestWith("12", "0.65", "12", "5", 1, SlowedTests);
        }

        [TestMethod]
        public void TestMethod13()
        {
            ConductTestWith("18", "0.65", "5", "5", 3, SlowedTests);
        }

        [TestMethod]
        public void TestMethod14()
        {
            ConductTestWith("85", "0.4", "10", "5", 2, SlowedTests);
        }

        [TestMethod]
        public void TestMethod15()
        {
            ConductTestWith("20", "0.32", "0", "5", 1, SlowedTests);
        }


        private static void SleepT(int time)
        {
            Thread.Sleep(time);
        }

        private static void ConductTestWith(string n, string p, string r, string a, int testType, bool slowDown)
        {
            // Arrange

            string thisLibraryPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            string appPath = Path.GetDirectoryName(Path.GetDirectoryName(thisLibraryPath));
#if DEBUG
            string config = "Debug";
#else
            string config = "Release";
#endif
            appPath = Path.Combine(appPath, "HypothesisTestingDesktop", "bin", config, "Hypothesis Testing Calculator.exe");

            // Act

            ProcessStartInfo psi = new ProcessStartInfo(appPath);
            Application app = Application.Launch(psi);

            try
            {
                Window window = app.Find(w => w.Equals("Hypothesis Testing Calculator"), InitializeOption.NoCache);

                var inputN = window.Get(SearchCriteria.ByAutomationId("inputN"));
                var inputP = window.Get(SearchCriteria.ByAutomationId("inputP"));
                var inputR = window.Get(SearchCriteria.ByAutomationId("inputR"));
                var inputA = window.Get(SearchCriteria.ByAutomationId("inputA"));

                var radioButton1 = window.Get(SearchCriteria.ByAutomationId("radioButton1"));
                var radioButton2 = window.Get(SearchCriteria.ByAutomationId("radioButton2"));
                var radioButton3 = window.Get(SearchCriteria.ByAutomationId("radioButton3"));

                var buttonCalc = window.Get(SearchCriteria.ByAutomationId("buttonCalc"));
                var buttonReset = window.Get(SearchCriteria.ByAutomationId("buttonReset"));


                Assert.IsNotNull(inputN);
                inputN.Enter(n.ToString());
                if (slowDown) SleepT(256);

                Assert.IsNotNull(inputP);
                inputP.Enter(p.ToString());
                if (slowDown) SleepT(256);

                Assert.IsNotNull(inputR);
                inputR.Enter(r.ToString());
                if (slowDown) SleepT(256);

                Assert.IsNotNull(inputA);
                inputA.Enter(a.ToString());
                if (slowDown) SleepT(256);

                if (testType == 1)
                {
                    Assert.IsNotNull(radioButton1);
                    radioButton1.Click();
                }
                if (testType == 2)
                {
                    Assert.IsNotNull(radioButton2);
                    radioButton2.Click();
                }
                if (testType == 3)
                {
                    Assert.IsNotNull(radioButton3);
                    radioButton3.Click();
                }

                if (slowDown) SleepT(512);
                Assert.IsNotNull(buttonCalc);
                buttonCalc.Click();
                if (slowDown) SleepT(1024);                
            }
            finally
            {
                Thread.Sleep(4096);
                if (slowDown) SleepT(1024);
                app.Close();
            }

        }
    }
}
