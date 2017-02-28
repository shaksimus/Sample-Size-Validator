using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleSizeValidator.Interface;

namespace SampleSizeValidator.UnitTests
{
    [TestClass]
    public class FileServiceTest
    {
        private IValidatorService _fileService;
        private string lpTestFile = "Test Data\\LpTest.csv";
        private string touTestFile = "Test Data\\TouTest.csv";

        [TestInitialize]
        public void Setup()
        {
            Bootstrap.Register();
            _fileService = TinyIoC.TinyIoCContainer.Current.Resolve<IValidatorService>();
        }



        [TestMethod]
        public void Get_Power_Reading_FromValid_LpFile_Then_Returns_List()
        {
            var lpResult = _fileService.GetReadingsFromFiles(lpTestFile);
            var touResult = _fileService.GetReadingsFromFiles(touTestFile);

            Assert.IsNotNull(lpResult);
            Assert.IsTrue(lpResult.Any());

            Assert.IsNotNull(touResult);
            Assert.IsTrue(touResult.Any());

        }

        [TestMethod]
        public void Get_Power_Reading_Outside_Tolerance_FromValid_Files_Then_Returns_List()
        {
            var touList = _fileService.GetReadingsFromFiles(touTestFile);
            var touResult = _fileService.GetReadingsOutsideMedianLevel(touList, 20);

            var lpList = _fileService.GetReadingsFromFiles(lpTestFile);
            var lpResult = _fileService.GetReadingsOutsideMedianLevel(lpList, 20);


            Assert.IsNotNull(touResult);
            Assert.IsTrue(touResult.Any());
            Assert.IsNotNull(lpResult);
            Assert.IsTrue(lpResult.Any());

        }


    }
}
