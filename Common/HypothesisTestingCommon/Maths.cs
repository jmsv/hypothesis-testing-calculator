using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HypothesisTestingCommon
{
    public class Maths
    {
        /// <summary>
        /// Gets the factorial of integer 'n'
        /// </summary>
        /// <param name="number">Number to find factorial of. The value returned is equal to number!.</param>
        /// <returns>The factorial of the specified number.</returns>
        public static ulong GetFactorial(long number)
        {
            if (number == 0)
            {
                return 1;
            }
            return ((ulong)number) * GetFactorial(number - 1);
        }

        /// <summary>
        /// Calculates 'P' to the power of 'R'.
        /// </summary>
        /// <param name="p">Base number.</param>
        /// <param name="r">Power.</param>
        /// <returns></returns>
        public static double PToTheR(double p, long r)
        {
            double tempP = p;
            double pToTheRReturn;

            if (r != 0)
            {
                for (long i = 1; i < r; i++)
                {
                    tempP = tempP * p;
                }
                pToTheRReturn = tempP;
            }
            else
            {
                pToTheRReturn = 1;
            }

            return pToTheRReturn;
        }

        /// <summary>
        /// Takes N and R and uses GetFactorial to calculate nCr
        /// </summary>
        /// <param name="nValueIn">n value (nCr)</param>
        /// <param name="rValueIn">r value (nCr)</param>
        /// <returns></returns>
        public static long Get_nCr(long n, long r)
        {
            ulong nCrValue = 0;

            if (n > r)
            {
                long nMinusR = n - r;

                ulong nFactorial = Maths.GetFactorial(n);
                ulong rFactorial = Maths.GetFactorial(r);
                ulong nMinusRFactorial = Maths.GetFactorial(nMinusR);

                try
                {
                    nCrValue = nFactorial / (rFactorial * nMinusRFactorial);
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            else
            {
                nCrValue = 0;
            }

            return (long)nCrValue;
        }

        /// <summary>
        /// (1 - p)^(n - r)
        /// </summary>
        /// <param name="n">'n' value. (int)</param>
        /// <param name="p">'p' value. (double)</param>
        /// <param name="r">'r' value. (int)</param>
        /// <returns></returns>
        public static double OneMinusPToTheNMinusR(long n, double p, long r)
        {
            p = 1 - p;
            double tempP = p;

            long nMinusR = n - r;

            for (long i = 1; i < nMinusR; i++)
            {
                tempP = tempP * p;
            }

            double returnValue = tempP;
            return returnValue;
        }

        /// <summary>
        /// Calculates single binomial value, non-cumulative.
        /// </summary>
        /// <param name="n">'n' value</param>
        /// <param name="p">'p' value</param>
        /// <param name="r">'r' value</param>
        /// <returns></returns>
        public static double BinomialProb_XetR(long n, double p, long r)
        {
            double Result = 1;
            Result = Result * (Maths.Get_nCr(n, r));
            Result = Result * (Maths.PToTheR(p, r));
            Result = Result * (Maths.OneMinusPToTheNMinusR(n, p, r));

            return Result;
        }

        #region Android

        /// <summary>
        /// Takes the binomial formula and calculates the cumulative value.
        /// From r=0 to r=x.
        /// </summary>
        /// <param name="n">'n' value</param>
        /// <param name="p">'p' value</param>
        /// <param name="r">'r' value</param>
        /// <returns></returns>
        public static double BinomialProb_XltORetR(long n, double p, long r)
        {
            double res = 0;

            for (int i = 0; i <= r; i++)
            {
                res = res + Maths.BinomialProb_XetR(n, p, i);
            }
            return res;
        }

        public static string typeOfTest(string altSelect)
        {
            if (altSelect == "1")
            {
                return "1";
            }
            if (altSelect == "2")
            {
                return "2";
            }
            if (altSelect == "3")
            {
                return "3";
            }

            return ".";
        }

        public static bool IsAlternativeAccepted(long n, double p, long r, double a)
        {
            bool altAccept = false;
            double siglevel = a / 100; // assignment of significance level value

            return altAccept;
        }

        #endregion

        #region Desktop

        /// <summary>
        /// Takes the binomial formula and calculates the cumulative value.
        /// From r=0 to r=x.
        /// </summary>
        /// <param name="n">'n' value</param>
        /// <param name="p">'p' value</param>
        /// <param name="r">'r' value</param>
        /// <returns></returns>
        public static double BinomialProb_XltetR(long n, double p, long r)
        {
            double res = 0;

            for (long i = 0; i <= r; i++)
            {
                res = res + Maths.BinomialProb_XetR(n, p, i);
            }
            return res;
        }

        /// <summary>
        /// Takes the binomial formula and calculates the cumulative value.
        /// From r=0 to r>x.
        /// </summary>
        /// <param name="n">'n' value</param>
        /// <param name="p">'p' value</param>
        /// <param name="r">'r' value</param>
        /// <returns></returns>
        public static double BinomialProb_XltR(long n, double p, long r)
        {
            double res = 0;

            for (long i = 0; i < r; i++)
            {
                res = res + Maths.BinomialProb_XetR(n, p, i);
            }
            return res;
        }

        /// <summary>
        /// Takes the binomial formula and calculates the cumulative value.
        /// From r equals r down to r less than x.
        /// </summary>
        /// <param name="n">'n' value</param>
        /// <param name="p">'p' value</param>
        /// <param name="r">'r' value</param>
        /// <returns></returns>
        public static double BinomialProb_XgtR(long n, double p, long r)
        {
            double res = 0;
            res = 1 - Maths.BinomialProb_XltetR(n, p, r);
            return res;
        }

        /// <summary>
        /// Takes the binomial formula and calculates the cumulative value.
        /// From r=r down to r=x.
        /// </summary>
        /// <param name="n">'n' value</param>
        /// <param name="p">'p' value</param>
        /// <param name="r">'r' value</param>
        /// <returns></returns>
        public static double BinomialProb_XgtetR(long n, double p, long r)
        {
            double res = 0;

            res = 1 - Maths.BinomialProb_XltR(n, p, r);

            return res;
        }

        /// <summary>
        /// Uses maths methods listed above to detirmine whether
        /// the null/alternative hypotheis is accepted/rejected
        /// </summary>
        /// <param name="TestType">Type of test; (types 1, 2 or 3)</param>
        /// <param name="n">'n' value</param>
        /// <param name="p">'p' value</param>
        /// <param name="r">'r' value</param>
        /// <param name="a">'a' value</param>
        /// <returns>True if 'alt accepted' and 'null rejected'
        ///          False is 'alt rejected and 'null kept' </returns>
        public static bool IsAlternativeAccepted(int TestType, long n, double p, long r, double a)
        {
            // Test types:
            // 1 - H0gtH1
            // 2 - H0ltH1
            // 3 - H0neH1
            //------------
            // gt: greater than
            // lt: less than
            // ne: not equal to

            a = a / 100; // From % to x.x (percent to decimal)

            if (TestType == 1) // H0 > H1
            {
                if (BinomialProb_XltetR(n, p, r) <= a)
                    return true;
                else return false;
            }

            if (TestType == 2) // H0 < H1
            {
                if (BinomialProb_XgtetR(n, p, r) <= a)
                    return true;
                else return false;
            }

            if (TestType == 3) // H0 != H1
            {
                if (BinomialProb_XgtetR(n, p, r) < (a / 2)
                    | BinomialProb_XltetR(n, p, r) < (a / 2))
                    return true;
                else return false;
            }

            return false;
        }

        #endregion
    }
}
