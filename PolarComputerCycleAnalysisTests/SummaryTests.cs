using Microsoft.VisualStudio.TestTools.UnitTesting;
using PolarComputerCycleAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarComputerCycleAnalysis.Tests
{
    /// <summary>
    /// testing whether this method finds the maximum value from arraylist or not
    /// </summary>
    [TestClass()]
    public class SummaryTests
    {
        [TestMethod()]
        public void FindMaxTest()
        {
            int maxValue = Summary.FindMax(new List<string> { "15", "10", "4", "18", "16" });
            Assert.AreEqual(18, maxValue);
        }

        /// <summary>
        /// testing whether this method finds the manimum value from arraylist or not
        /// </summary>
        [TestMethod()]
        public void FindMinTest()
        {
            int val = Summary.FindMin(new List<string> { "15", "10", "4", "18", "16" });
            Assert.AreEqual(4, val);
        }
        /// <summary>
        /// testing whether this method finds the average value from arraylist or not
        /// </summary>
        [TestMethod()]
        public void FindAverageTest()
        {
            double val = Summary.FindAverage(new List<string> { "15", "10", "4", "18", "16" });
            Assert.AreEqual(12, val);
        }
        /// <summary>
        /// testing whether this method returns the total sum value from arraylist or not
        /// </summary>
        [TestMethod()]
        public void FindSumTest()
        {
            double val = Summary.FindSum(new List<string> { "15", "10", "4", "18", "16" });
            Assert.AreEqual(63, val);
        }
        /// <summary>
        /// testing whether this method converts string value of date into correct format of date or not
        /// </summary>
        [TestMethod()]
        public void ConvertToDate()
        {
            string val = Summary.ConvertToDate("20120102");
            Assert.AreEqual("2012-01-02", val);
        }
    }
}