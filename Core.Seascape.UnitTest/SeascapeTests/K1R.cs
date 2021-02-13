using Core.Seascape.BLL.Models;
using Core.Seascape.BLL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tests.Seascape
{
    [TestClass]
    public class K1R
    {

        [TestMethod]
        public void LowerRange()
        {
            var seascapeService = new SeascapeService();
            var seascapeModel = new SeascapeModel(1920, 1080);
            var seascapeOptions = new SeascapeOptions();
            var validRange = new List<int>();

            for (var k1r = 540; k1r < 550; k1r++)
            {
                seascapeOptions.K1R = k1r;

                seascapeModel.Options = seascapeOptions;

                if (seascapeService.GenerateBase64Data(seascapeModel, false) != null)
                {
                    validRange.Add(k1r);
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
            var seascapeService = new SeascapeService();
            var seascapeModel = new SeascapeModel(1920, 1080);
            var seascapeOptions = new SeascapeOptions();
            var validRange = new List<int>();

            for (var k1r = 551; k1r <= 560; k1r++)
            {
                seascapeOptions.K1R = k1r;

                seascapeModel.Options = seascapeOptions;

                if (seascapeService.GenerateBase64Data(seascapeModel, false) != null)
                {
                    validRange.Add(k1r);
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
