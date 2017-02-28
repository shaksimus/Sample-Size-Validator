using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleSizeValidator.Interface;
using SampleSizeValidator.Service;
using TinyIoC;

namespace SampleSizeValidator
{
    public static class Bootstrap
    {
        public static void Register()
        {
            //registration of containers
            TinyIoCContainer.Current.Register<IValidatorService>(new ValidatorService());
        }
    }
}
