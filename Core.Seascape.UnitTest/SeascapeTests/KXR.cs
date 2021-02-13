using Core.Seascape.BLL.Models;
using Core.Seascape.BLL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tests.Seascape
{
    [TestClass]
    public class KXR
    {

        [TestMethod]
        public void LowerRange()
        {
            var seascapeService = new SeascapeService();
            var seascapeModel = new SeascapeModel(1920, 1080);
            var seascapeOptions = new SeascapeOptions();
            var validRange = new List<int>();

            for (var kxr = 600; kxr < 610; kxr++)
            {
                seascapeOptions.KXR = kxr;

                seascapeModel.Options = seascapeOptions;

                if (seascapeService.GenerateBase64Data(seascapeModel, false) != null)
                {
                    validRange.Add(kxr);
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

            for (var kxr = 1891; kxr <= 1900; kxr++)
            {
                seascapeOptions.KXR = kxr;

                seascapeModel.Options = seascapeOptions;

                if (seascapeService.GenerateBase64Data(seascapeModel, false) != null)
                {
                    validRange.Add(kxr);
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
