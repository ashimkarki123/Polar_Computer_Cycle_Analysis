using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolarComputerCycleAnalysis
{
    public class FileConvertor
    {
        /// <summary>
        /// Splitting text by header data
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string[] SplitString(string text) => text.Split(GetParams(), StringSplitOptions.RemoveEmptyEntries);

        /// <summary>
        /// Detect line break from hrm data and return value in the form of array
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string[] SplitStringByEnter(string text) => text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

        /// <summary>
        /// Detect space from data and return value in the form of array
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string[] SplitStringBySpace(string text) => string.Join(" ", text.Split().Where(x => x != "")).Split(' ');

        /// <summary>
        /// returns header data
        /// </summary>
        /// <returns></returns>
        public string[] GetParams()
        {
            return new string[] { "[Params]", "[Note]", "[IntTimes]", "[IntNotes]",
                "[ExtraData]", "[LapNames]", "[Summary-123]",
                "[Summary-TH]", "[HRZones]", "[SwapTimes]", "[Trip]", "[HRData]"};
        }


    }
}
