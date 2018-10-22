using HypothesisTestingDesktop;
using System;
using System.Collections.Generic;
using Xunit;
using HypothesisTestingCommon;

namespace HypothesisTestingDesktop.Tests
{
    public class htMathsTests
    {
        // Papers used for AltAccepted tests were found at http://www.mei.org.uk/alevelpapers#S1

        public static IEnumerable<object[]> AltAcceptedTestData()
        {
            yield return new object[] { 3, 20, 0.35f, 10, 5.0f, false }; // MEI S1 Jan 2013 7)ii)
            yield return new object[] { 3, 200, 0.35f, 90, 5.0f, true }; // MEI S1 Jan 2013 7)iii)
            yield return new object[] { 1, 20, 0.85f, 13, 1.0f, false }; // MEI S1 June 2014 7)iii)
            yield return new object[] { 2, 20, 0.5f, 13, 5.0f, false }; // MEI S1 June 2013 5)iii)
        }

        [Theory]
        [MemberData(nameof(AltAcceptedTestData))]
        public void AltAcceptedTest(int testType, long n, double p, long r, double a, bool expected)
        {
            bool actual = Maths.IsAlternativeAccepted(testType, n, p, r, a);

            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> GetFactorialTestData()
        {
            //yield return new object[] { -1, 1 };
            yield return new object[] { 0, 1 };
            yield return new object[] { 1, 1 };
            yield return new object[] { 2, 2 };
            yield return new object[] { 3, 6 };
            yield return new object[] { 4, 24 };
            yield return new object[] { 5, 120 };
            yield return new object[] { 10, 3628800 };
            yield return new object[] { 11, 39916800 };
            yield return new object[] { 200, 0 };
        }

        [Theory]
        [MemberData(nameof(GetFactorialTestData))]
        public void GetFactorialTest(long number, ulong expected)
        {
            var actual = Maths.GetFactorial(number);
            Assert.Equal(expected, actual);
        }
    }
}
