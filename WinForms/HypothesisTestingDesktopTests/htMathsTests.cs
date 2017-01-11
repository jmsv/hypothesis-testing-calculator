using Microsoft.VisualStudio.TestTools.UnitTesting;
using HypothesisTestingDesktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HypothesisTestingDesktop.Tests
{
    [TestClass()]
    public class htMathsTests
    {
        // Papers used for AltAccepted tests were found at http://www.mei.org.uk/alevelpapers#S1

        [TestMethod()]
        public void AltAcceptedTest1() // MEI S1 Jan 2013 7)ii)
        {
            //Arrange
            bool expected = false;
            //Act
            bool actual = htMaths.AltAccepted(3, 20, 0.35m, 10, 5);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AltAcceptedTest2() // MEI S1 Jan 2013 7)iii)
        {
            //Arrange
            bool expected = true;
            //Act
            bool actual = htMaths.AltAccepted(3, 200, 0.35m, 90, 5);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AltAcceptedTest3() // MEI S1 June 2014 7)iii)
        {
            //Arrange
            bool expected = false;
            //Act
            bool actual = htMaths.AltAccepted(1, 20, 0.85m, 13, 1);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AltAcceptedTest4() // MEI S1 June 2013 5)iii)
        {
            //Arrange
            bool expected = false;
            //Act
            bool actual = htMaths.AltAccepted(2, 20, 0.5m, 13, 5);
            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}