using System;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using System.IO;
using System.Diagnostics;
using System.Threading;
using Xunit;
using System.Collections.Generic;

namespace HypothesisTestingDesktop.UITests
{
    public class UnitTest1
    {
        static bool SlowedTests = false;

        private static void SleepT(int time)
        {
            Thread.Sleep(time);
        }

        public static IEnumerable<object[]> GetTestData()
        {
            yield return new object[] { 20, 0.5m, 14, 5, 2, SlowedTests };
            yield return new object[] { 20, 0.5m, 15, 5, 2, SlowedTests };
            yield return new object[] { 10, 0.5m, 2, 5, 1, SlowedTests };
            yield return new object[] { 10, 0.5m, 2, 10, 1, SlowedTests };
            yield return new object[] { 14, 0.2m, 17, 5, 3, SlowedTests };
            yield return new object[] { 20, 0.5m, 10, 5, 0, SlowedTests };
            yield return new object[] { 16, 1.1m, 4, 2, 2, SlowedTests };
            yield return new object[] { 20, 0.8m, 15, 104, 2, SlowedTests };
            yield return new object[] { 18, 0.0m, 2, 5, 1, SlowedTests };
            yield return new object[] { 18, 1.0m, 12, 5, 1, SlowedTests };
            yield return new object[] { 10, 0.5m, 6, 100, 1, SlowedTests };
            yield return new object[] { 12, 0.65m, 12, 5, 1, SlowedTests };
            yield return new object[] { 18, 0.65m, 5, 5, 3, SlowedTests };
            yield return new object[] { 85, 0.4m, 10, 5, 2, SlowedTests };
            yield return new object[] { 20, 0.32m, 0, 5, 1, SlowedTests };
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void ConductTestWith(int n, decimal p, int r, int a, int testType, bool slowDown)
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

                Assert.NotNull(inputN);
                inputN.Enter(n.ToString());
                if (slowDown) SleepT(256);

                Assert.NotNull(inputP);
                inputP.Enter(p.ToString());
                if (slowDown) SleepT(256);

                Assert.NotNull(inputR);
                inputR.Enter(r.ToString());
                if (slowDown) SleepT(256);

                Assert.NotNull(inputA);
                inputA.Enter(a.ToString());
                if (slowDown) SleepT(256);

                if (testType == 1)
                {
                    Assert.NotNull(radioButton1);
                    radioButton1.Click();
                }
                if (testType == 2)
                {
                    Assert.NotNull(radioButton2);
                    radioButton2.Click();
                }
                if (testType == 3)
                {
                    Assert.NotNull(radioButton3);
                    radioButton3.Click();
                }

                if (slowDown) SleepT(512);
                Assert.NotNull(buttonCalc);
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
