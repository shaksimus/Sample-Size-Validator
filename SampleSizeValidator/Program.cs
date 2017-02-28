using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SampleSizeValidator.Interface;
using SampleSizeValidator.Model;

namespace SampleSizeValidator
{
    class Program
    {



        private static void DisplayResults(string fileName, List<PowerReading> displayList)
        {
            if (!displayList.Any())
            {
                Console.WriteLine($"Results: No Abnormalities found in {fileName}");
                return;
            }
            Console.WriteLine("Results:  ");

            foreach (var item in displayList)
            {
                Console.WriteLine($"{fileName} {item.ReadingDateTime} {item.DataValue} {item.MedianValue}");
            }
        }

        public static void Main(string[] args)
        {
            Bootstrap.Register();
            var run = true;
            var validationService = TinyIoC.TinyIoCContainer.Current.Resolve<IValidatorService>();

            while (run)
            {

                Console.WriteLine("Welcome to Sample File Validator ");
                Console.WriteLine($"Sample Size Validator v. {typeof(Program).Assembly.GetName().Version}");
                Console.WriteLine("Enter 'q' and press enter to exit the application anytime....");
                Console.WriteLine("Please enter the directory name of the files to validate:");

                var command = Console.ReadLine();

                if (command == "q")
                {
                    run = false;
                }
                else
                {
                    var csvFiles = new DirectoryInfo(command).EnumerateFiles("*.csv").Select(f=>f.Name).ToList();

                    csvFiles.ForEach(file =>
                    {
                        var list = validationService.GetReadingsFromFiles(file);
                        var result = validationService.GetReadingsOutsideMedianLevel(list, 20);
                        DisplayResults(file, result);
                    });


                }
            }

        }
    }
}
