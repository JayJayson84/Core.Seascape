using Core.Seascape.BLL.Models;
using Core.Seascape.BLL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tests.Seascape
{
    [TestClass]
    public class WaterRipple
    {

        [TestMethod]
        public void LowerRange()
        {
            var sunsetService = new SeascapeService();
            var sunsetModel = new SeascapeModel(1920, 1080);
            var sunsetOptions = new SeascapeOptions();
            var validRange = new List<int>();

            for (var waterRipple = 10; waterRipple < 20; waterRipple++)
            {
                sunsetOptions.WaterRipple = waterRipple;

                sunsetModel.Options = sunsetOptions;

                if (sunsetService.GenerateBase64Data(sunsetModel, false) != null)
                {
                    validRange.Add(waterRipple);
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

            for (var waterRipple = 1061; waterRipple <= 1070; waterRipple++)
            {
                sunsetOptions.WaterRipple = waterRipple;

                sunsetModel.Options = sunsetOptions;

                if (sunsetService.GenerateBase64Data(sunsetModel, false) != null)
                {
                    validRange.Add(waterRipple);
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
