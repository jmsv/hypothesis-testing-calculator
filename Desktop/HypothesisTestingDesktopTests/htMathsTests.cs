using HypothesisTestingDesktop;
using System;
using System.Collections.Generic;
using Xunit;

namespace HypothesisTestingDesktop.Tests
{
    public class htMathsTests
    {
        // Papers used for AltAccepted tests were found at http://www.mei.org.uk/alevelpapers#S1

        public static IEnumerable<object[]> AltAcceptedTestData()
        {
            yield return new object[] { 3, 20, 0.35m, 10, 5, false }; // MEI S1 Jan 2013 7)ii)
            yield return new object[] { 3, 200, 0.35m, 90, 5, true }; // MEI S1 Jan 2013 7)iii)
            yield return new object[] { 1, 20, 0.85m, 13, 1, false }; // MEI S1 June 2014 7)iii)
            yield return new object[] { 2, 20, 0.5m, 13, 5, false }; // MEI S1 June 2013 5)iii)
        }

        [Theory]
        [MemberData(nameof(AltAcceptedTestData))]
        public void AltAcceptedTest(int testType, Int64 n, decimal p, Int64 r, decimal a, bool expected)
        {
            bool actual = htMaths.AltAccepted(testType, n, p, r, a);

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
        }

        [Theory]
        [MemberData(nameof(GetFactorialTestData))]
        public void GetFactorialTest(Int64 number, Int64 expected)
        {
            var actual = htMaths.GetFactorial(number);
            Assert.Equal(expected, actual);
        }
    }
}
