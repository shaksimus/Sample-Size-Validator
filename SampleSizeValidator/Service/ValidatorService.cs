using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SampleSizeValidator.Interface;
using SampleSizeValidator.Model;

namespace SampleSizeValidator.Service
{
    public class ValidatorService : IValidatorService
    {
        /// <summary>
        /// Returns a list of PowerReading from the csv file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public List<PowerReading> GetReadingsFromFiles(string file)
        {
            if (!File.Exists(file))
            {
                throw new FileNotFoundException();
            }
            var result = new List<PowerReading>();
            var lines = File.ReadAllLines(file).Skip(1);

            foreach (var line in lines)
            {
                var fields = line.Split(',');
                result.Add(new PowerReading()
                {
                    ReadingDateTime = DateTime.Parse(fields[3]),
                    DataValue = float.Parse(fields[5])
                });
            }
            return result;
        }

        /// <summary>
        /// Returns a list of non zero readings that are outside the tolerance level
        /// </summary>
        /// <param name="unorderdList"></param>
        /// <param name="toleranceLevel">Specified as Percentage</param>
        /// <returns></returns>
        public List<PowerReading> GetReadingsOutsideMedianLevel(List<PowerReading> unorderdList, float toleranceLevel)
        {
            var orderedList = unorderdList.OrderBy(p => p.DataValue).ToList();
            var index = orderedList.Count()/2;
            if (orderedList.Count()%2 == 0)
            {
                index++;
            }

            var upperToleranceValue = orderedList[index].DataValue * (1+ toleranceLevel/100);
            var lowerToleranceValue = orderedList[index].DataValue * (1- toleranceLevel/100);

            var result =
                unorderdList.Where(p => p.DataValue > 0 && (p.DataValue <= lowerToleranceValue || p.DataValue >= upperToleranceValue))
                    .Select(x => new PowerReading()
                    {
                        DataValue = x.DataValue,
                        ReadingDateTime = x.ReadingDateTime,
                        MedianValue = orderedList[index].DataValue
                    })
                    .ToList();
            return result;
        }
    }
}
