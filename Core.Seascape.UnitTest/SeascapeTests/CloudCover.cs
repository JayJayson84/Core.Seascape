using Core.Seascape.BLL.Models;
using Core.Seascape.BLL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tests.Seascape
{
    [TestClass]
    public class CloudCover
    {

        [TestMethod]
        public void LowerRange()
        {
            var sunsetService = new SeascapeService();
            var sunsetModel = new SeascapeModel(1920, 1080);
            var sunsetOptions = new SeascapeOptions();
            var validRange = new List<int>();

            for (var cloudCover = 10; cloudCover < 20; cloudCover++)
            {
                sunsetOptions.CloudCover = cloudCover;

                sunsetModel.Options = sunsetOptions;

                if (sunsetService.GenerateBase64Data(sunsetModel, false) != null)
                {
                    validRange.Add(cloudCover);
                }
            }

            if (validRange.Count == 0)
            {
                Assert.Fail("No valid values found.");
                return;
            }

            Trace.WriteLine($"Valid range: {string.Join(", ", validRange)}");

            Assert.AreEqual(validRange.Count - 1, validRange.Last() - validRange.First(), "Range mismatch");
        }

        [TestMethod]
        public void UpperRange()
        {
            var sunsetService = new SeascapeService();
            var sunsetModel = new SeascapeModel(1920, 1080);
            var sunsetOptions = new SeascapeOptions();
            var validRange = new List<int>();

            for (var cloudCover = 999991; cloudCover <= 1000000; cloudCover++)
            {
                sunsetOptions.CloudCover = cloudCover;

                sunsetModel.Options = sunsetOptions;

                if (sunsetService.GenerateBase64Data(sunsetModel, false) != null)
                {
                    validRange.Add(cloudCover);
                }
            }

            if (validRange.Count == 0)
            {
                Assert.Fail("No valid values found.");
                return;
            }

            Trace.WriteLine($"Valid range: {string.Join(", ", validRange)}");

            Assert.AreEqual(validRange.Count - 1, validRange.Last() - validRange.First(), "Range mismatch");
        }

    }
}
