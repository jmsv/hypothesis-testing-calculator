using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HypothesisTestingAndroid
{
	public class htMaths
	{

		/// <summary>
		/// Gets the factorial of integer 'n'
		/// </summary>
		/// <param name="number">Number to find factorial of. The value returned is equal to number!.</param>
		/// <returns>The factorial of the specified number.</returns>
		public static long GetFactorial(int number)
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
		public static double PToTheR(double p, int r)
		{
			double tempP = p;
			double pToTheRReturn;

			if (r != 0)
			{
				for (int i = 1; i < r; i++)
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
		public static long Get_nCr(int n, int r)
		{
			long nCrValue = 0;

			if (n > r)
			{
				int nMinusR = n - r;
				long nFactorial;
				long rFactorial;
				long nMinusRFactorial;

				nFactorial = htMaths.GetFactorial(n);
				rFactorial = htMaths.GetFactorial(r);
				nMinusRFactorial = htMaths.GetFactorial(nMinusR);

				nCrValue = nFactorial / (rFactorial * nMinusRFactorial);
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
		/// <param name="p">'p' value. (double)</param>
		/// <param name="r">'r' value. (int)</param>
		/// <returns></returns>
		public static double OneMinusPToTheNMinusR(int n, double p, int r)
		{
			p = 1 - p;
			double tempP = p;

			int nMinusR = n - r;

			for (int i = 1; i < nMinusR; i++)
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
		public static double BinomialProb_XetR(int n, double p, int r)
		{
			double Result = 1;
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
		public static double BinomialProb_XltORetR(int n, double p, int r)
		{
			double res = 0;

			for (int i = 0; i <= r; i++)
			{
				res = res + htMaths.BinomialProb_XetR(n, p, i);
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



		public static bool isAlternativeAccepted(int n, double p, int r, double a)
		{
			bool altAccept = false;
			double siglevel = a / 100; // assignment of significance level value



			return altAccept;

		}
	}
}