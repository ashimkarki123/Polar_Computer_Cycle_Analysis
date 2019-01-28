using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarComputerCycleAnalysis
{
    class AdvancedMetricsCalculation
    {
        /// <summary>
        /// Normalized power is calculated
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public double CalculateNormalizedPower(Dictionary<string, object> list)
        {
            List<double> powerSumList = new List<double>();

            var powerList = list["watt"] as List<string>; //converting object into list
            int count = 0;
            double powerSum = 0;

            for (int i = 0; i < powerList.Count; i++)
            {
                count++;
                double power = Convert.ToDouble(powerList[i]);
                powerSum += power;

                if (count == 30)
                {
                    powerSumList.Add(NthRoot(powerSum, 4) * 0.5);
                    count = 0;
                }
            }

            var result = Summary.FindSum(powerSumList.Select(p => p.ToString()).ToList());

            return result;
        }

        /// <summary>
        /// Calculating functional threshold power
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public double CalculateFunctionalThresholdPower(Dictionary<string, object> list) => Summary.FindAverage((List<string>)list["watt"]) * 0.95;

        /// <summary>
        /// Calculating intesity factor
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public double CalculateIntensityFactor(Dictionary<string, object> list) => CalculateNormalizedPower(list) / CalculateFunctionalThresholdPower(list);

        /// <summary>
        /// Calculating training stress score
        /// </summary>
        /// <param name="ftp"></param>
        /// <param name="avgPower"></param>
        /// <returns></returns>
        public double CalculateTss(double ftp, double avgPower) => (avgPower / ftp) * 100;

        /// <summary>
        /// Calculating power balance
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public double CalculatePowerBalance(Dictionary<string, object> list) => Summary.FindAverage(list["speed"] as List<string>);

        /// <summary>
        /// Calculating root of nth value
        /// </summary>
        /// <param name="A"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        public static double NthRoot(double A, int N) => Math.Pow(A, 1.0 / N);
    }
}
