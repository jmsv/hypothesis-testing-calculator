using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HypothesisTestingDesktop
{
    public class htMaths
    {


        /// <summary>
        /// Gets the factorial of integer 'n'
        /// </summary>
        /// <param name="number">Number to find factorial of. The value returned is equal to number!.</param>
        /// <returns>The factorial of the specified number.</returns>
        public static Int64 GetFactorial(Int64 number)
        {
            if (number == 0)
            {
                return 1;
            }
            return number * GetFactorial(number - 1);
        }



        /// <summary>
        /// Calculates 'P' to the power of 'R'.
        /// </summary>
        /// <param name="p">Base number.</param>
        /// <param name="r">Power.</param>
        /// <returns></returns>
        public static decimal PToTheR(decimal p, Int64 r)
        {
            decimal tempP = p;
            decimal pToTheRReturn;

            if (r != 0)
            {
                for (Int64 i = 1; i < r; i++)
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
        public static Int64 Get_nCr(Int64 n, Int64 r)
        {
            Int64 nCrValue = 0;

            if (n > r)
            {
                Int64 nMinusR = n - r;
                Int64 nFactorial;
                Int64 rFactorial;
                Int64 nMinusRFactorial;

                nFactorial = htMaths.GetFactorial(n);
                rFactorial = htMaths.GetFactorial(r);
                nMinusRFactorial = htMaths.GetFactorial(nMinusR);

                try
                {
                    nCrValue = nFactorial / (rFactorial * nMinusRFactorial);
                }
                catch (Exception e)
                {
                    return -1;
                }
            }
            else
            {
                nCrValue = 0;
            }

            return nCrValue;
        }



        /// <summary>
        /// (1 - p)^(n - r)
        /// </summary>
        /// <param name="n">'n' value. (int)</param>
        /// <param name="p">'p' value. (decimal)</param>
        /// <param name="r">'r' value. (int)</param>
        /// <returns></returns>
        public static decimal OneMinusPToTheNMinusR(Int64 n, decimal p, Int64 r)
        {
            p = 1 - p;
            decimal tempP = p;

            Int64 nMinusR = n - r;

            for (Int64 i = 1; i < nMinusR; i++)
            {
                tempP = tempP * p;
            }

            decimal returnValue = tempP;
            return returnValue;
        }



        /// <summary>
        /// Calculates single binomial value, non-cumulative.
        /// </summary>
        /// <param name="n">'n' value</param>
        /// <param name="p">'p' value</param>
        /// <param name="r">'r' value</param>
        /// <returns></returns>
        public static decimal BinomialProb_XetR(Int64 n, decimal p, Int64 r)
        {
            decimal Result = 1;
            Result = Result * (htMaths.Get_nCr(n, r));
            Result = Result * (htMaths.PToTheR(p, r));
            Result = Result * (htMaths.OneMinusPToTheNMinusR(n, p, r));

            return Result;
        }



        /// <summary>
        /// Takes the binomial formula and calculates the cumulative value.
        /// From r=0 to r=x.
        /// </summary>
        /// <param name="n">'n' value</param>
        /// <param name="p">'p' value</param>
        /// <param name="r">'r' value</param>
        /// <returns></returns>
        public static decimal BinomialProb_XltetR(Int64 n, decimal p, Int64 r)
        {
            decimal res = 0;

            for (Int64 i = 0; i <= r; i++)
            {
                res = res + htMaths.BinomialProb_XetR(n, p, i);
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
        public static decimal BinomialProb_XltR(Int64 n, decimal p, Int64 r)
        {
            decimal res = 0;

            for (Int64 i = 0; i < r; i++)
            {
                res = res + htMaths.BinomialProb_XetR(n, p, i);
            }
            return res;
        }



        /// <summary>
        /// Takes the binomial formula and calculates the cumulative value.
        /// From r=r down to r<x.
        /// </summary>
        /// <param name="n">'n' value</param>
        /// <param name="p">'p' value</param>
        /// <param name="r">'r' value</param>
        /// <returns></returns>
        public static decimal BinomialProb_XgtR(Int64 n, decimal p, Int64 r)
        {
            decimal res = 0;

            res = 1 - htMaths.BinomialProb_XltetR(n, p, r);

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
        public static decimal BinomialProb_XgtetR(Int64 n, decimal p, Int64 r)
        {
            decimal res = 0;

            res = 1 - htMaths.BinomialProb_XltR(n, p, r);

            return res;
        }





        public static bool AltAccepted(int TestType, Int64 n, decimal p, Int64 r, decimal a)
        {
            // Test types:
            // 1 - H0gtH1
            // 2 - H0ltH1
            // 3 - H0neH1
            //------------
            // gt: greater than
            // lt: less than
            // ne: not equal to

            a = a / 100;


            if (TestType == 1) // H0 > H1
            {
                if (BinomialProb_XltetR(n, p, r) <= a) return true;
                else return false;
            }

            if (TestType == 2) // H0 < H1
            {
                if (BinomialProb_XgtetR(n, p, r) <= a) return true;
                else return false;
            }

            if (TestType == 3) // H0 != H1
            {
                int i = 0; // count

                bool foundXlower = false;
                int Xlower = 0;
                while (!foundXlower && i <= n)
                {
                    if (BinomialProb_XltetR(n, p, i) <= (a / 2))
                    {
                        Xlower = i;
                        i++;
                    }
                    else
                    {
                        foundXlower = true;
                    }
                }

                i = 0; //reset count

                bool foundXupper = false;
                int Xupper = 0;
                i++;
                while (!foundXupper && i <= n)
                {
                    if (BinomialProb_XltetR(n, p, i - 1) >= (1 - (a / 2)))
                    {
                        Xupper = i;
                        i++;
                    }
                    else
                    {
                        foundXupper = true;
                    }
                }


                // Now I have the lower and upper x values,
                // I can compare them to the given r value.

                if (r <= Xlower || r >= Xupper) return true;
            }

            return false;
        }



    }
}