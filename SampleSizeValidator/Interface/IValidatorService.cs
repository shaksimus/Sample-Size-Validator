using System.Collections.Generic;
using SampleSizeValidator.Model;

namespace SampleSizeValidator.Interface
{
    public interface IValidatorService
    {
        List<PowerReading> GetReadingsFromFiles(string file);
        List<PowerReading> GetReadingsOutsideMedianLevel(List<PowerReading> unorderdList, float toleranceLevel);

    }
}
