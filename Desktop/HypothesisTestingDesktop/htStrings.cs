using System;

namespace HypothesisTestingDesktop
{
    /// <summary>
    /// Implementayion for the string building class.
    /// </summary>
    public class htStrings
    {
        /// <summary>
        /// Get the conclusion text.
        /// </summary>
        /// <param name="specific">if set to <c>true</c> [specific].</param>
        /// <param name="successTrial">The success trial.</param>
        /// <param name="testType">Type of the test.</param>
        /// <param name="AltAccepted">if set to <c>true</c> [alt accepted].</param>
        /// <param name="n">The n.</param>
        /// <param name="p">The p.</param>
        /// <returns>The conclusion string.</returns>
        public static string TestConclusion(bool specific, string successTrial, int testType, bool AltAccepted, Int64 n, decimal p)
        {
            string comparative = "";
            // Relationship between null and alternative probabilities
            if (testType == 1) comparative = "less than";
            if (testType == 2) comparative = "greater than";
            if (testType == 3) comparative = "different to";
            if (testType >= 4 | testType <= 0) return "Error";

            decimal expectedX = n * p;
            // Expected number of successes from sample size according to null

            if (!specific) successTrial = " ";
            else successTrial = " (" + successTrial + ") ";

            // Null rejected hypothesis test conclusion
            if (AltAccepted) return "There is enough evidence to suggest that the number of successful trials"
                    + successTrial + "in the population may be "
                    + comparative
                    + " expected ("
                    + expectedX.ToString("0.00")
                    + ").";
            // Null kept hypothesis test conclusion
            else return "There is not enough evidence to suggest that the number of successful trials"
                    + successTrial
                    + "in the population is "
                    + comparative
                    + " expected ("
                    + expectedX.ToString("0.00")
                    + ").";
        }

        /// <summary>
        /// Gets the Critical comparison string.
        /// </summary>
        /// <param name="TestType">Type of the test.</param>
        /// <param name="n">The n.</param>
        /// <param name="p">The p.</param>
        /// <param name="r">The r.</param>
        /// <param name="a">a.</param>
        /// <returns></returns>
        public static string CriticalComparison(int TestType, Int64 n, decimal p, Int64 r, decimal a)
        {
            // Test types:
            // 1 - H0gtH1
            // 2 - H0ltH1
            // 3 - H0neH1
            //------------
            // gt: greater than
            // lt: less than
            // ne: not equal to

            decimal aDec = a / 100; // From % to x.x (percent to decimal)

            // < > ≥ ≤ H₀ H₁

            if (TestType == 1) // Test H0 > H1
            {
                if (htMaths.BinomialProb_XltetR(n, p, r) == aDec) return "P(X ≤ " + r.ToString() + ") ≤ " + a.ToString() + "%";

                if (htMaths.BinomialProb_XltetR(n, p, r) < aDec) return "P(X ≤ " + r.ToString() + ") < " + a.ToString() + "%";
                else return "P(X ≤ " + r.ToString() + ") > " + a.ToString() + "%";
            }

            if (TestType == 2) // Test H0 < H1
            {
                if (htMaths.BinomialProb_XgtetR(n, p, r) == aDec) return "P(X ≥ " + r.ToString() + ") ≤ " + a.ToString() + "%";

                if (htMaths.BinomialProb_XgtetR(n, p, r) < aDec) return "P(X ≥ " + r.ToString() + ") < " + a.ToString() + "%";
                else return "P(X ≥ " + r.ToString() + ") > " + a.ToString() + "%";
            }

            if (TestType == 3) // Test H0 != H1
            {
                if (htMaths.BinomialProb_XgtetR(n, p, r) < (aDec / 2)) return "P(X ≥ " + r.ToString() + ") < " + (a / 2).ToString() + "%";
                if (htMaths.BinomialProb_XltetR(n, p, r) < (aDec / 2)) return "P(X ≤ " + r.ToString() + ") < " + (a / 2).ToString() + "%";

                if (htMaths.BinomialProb_XgtetR(n, p, r) >= (aDec / 2)) return "P(X ≥ " + r.ToString() + ") > " + (a / 2).ToString() + "%";
                if (htMaths.BinomialProb_XltetR(n, p, r) >= (aDec / 2)) return "P(X ≤ " + r.ToString() + ") > " + (a / 2).ToString() + "%";
            }

            return "";
        }

        /// <summary>
        /// Gets the tests text.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <param name="p">The p.</param>
        /// <param name="r">The r.</param>
        /// <param name="a">a.</param>
        /// <param name="TestType">Type of the test.</param>
        /// <param name="AltAccepted">if set to <c>true</c> [alt accepted].</param>
        /// <param name="TestConclusion">The test conclusion.</param>
        /// <returns>The test text.</returns>
        public static string TestText(Int64 n, decimal p, Int64 r, decimal a, int TestType, bool AltAccepted, string TestConclusion)
        {
            // H₀ H₁ ≤ ≥ ≠

            if (TestType > 3 | TestType < 1) return "Please set up an appropriate test before trying to obtain results.";

            string outstring = "";

            outstring += "Hypothesis Testing Calculator test output " + GetTimeDate() + "\n\n";

            outstring += "Each test consists of: (e.g. 'Coin toss')\n";
            outstring += "A success is getting: (e.g. 'Heads')\n";
            outstring += "Number of tests: " + n.ToString() + "\n\n";

            outstring += "p is the probability that (e.g. 'a coin toss') from the population gives (e.g. 'heads').\n";
            outstring += "X is the number of (e.g. 'heads') in a sample size of " + n.ToString() + "\n\n";

            string TestOp = ""; // Decides on operator for type of test
            if (TestType == 1) TestOp = ">";
            if (TestType == 2) TestOp = "<";
            if (TestType == 3) TestOp = "≠";

            outstring += "H₀: p = " + p.ToString() + "\n";
            outstring += "H₁: p " + TestOp + " " + p.ToString() + "\n\n";

            outstring += "Significance level = " + a.ToString() + "%.\n\n";

            outstring += "Assume H₀ is true:\n";
            outstring += "X ~ B(" + n.ToString() + ", " + p.ToString() + ")\n";
            outstring += "X is distributed binomially with n = " + n.ToString() + ", p = " + p.ToString() + ".\n\n";

            outstring += "Number of successes, r = " + r.ToString() + "\n\n";

            string Comparison = htStrings.CriticalComparison(TestType, n, p, r, a);
            if (Comparison != "") outstring += Comparison + "\n\n";

            string WhatHappenedToTheNull = "";
            if (AltAccepted) WhatHappenedToTheNull = "Reject";
            else WhatHappenedToTheNull = "Accept";
            WhatHappenedToTheNull += " null (H₀)";
            outstring += WhatHappenedToTheNull + "\n\n";

            outstring += htStrings.TestConclusion(false, "", TestType, AltAccepted, n, p);

            return outstring + "\n\n";
        }


        /// <summary>
        /// Gets the current date and time.
        /// </summary>
        /// <returns>The current date/time.</returns>
        public static string GetTimeDate()
        {
            // Return time and date as a string
            string DateTime = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            return DateTime;
        }
    }
}
