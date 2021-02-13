using Core.Seascape.BLL.Models;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Seascape.BLL.Services
{
    public class StyleService : IStyleService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageData"></param>
        /// <param name="stylesheetPath"></param>
        /// <returns></returns>
        public bool CreateBase64AnimatedCss(SeascapeImageData imageData, string stylesheetPath)
        {
            if (string.IsNullOrWhiteSpace(imageData.FullSizeData) || string.IsNullOrWhiteSpace(imageData.CropData) || string.IsNullOrWhiteSpace(stylesheetPath)) return false;

            var css = $".seascape-image-container img{{background-image:url(data:image/png;base64,{imageData.FullSizeData});background-size:cover}}.seascape-image-container .water{{background-image:url(data:image/png;base64,{imageData.CropData})}}";

            try
            {
                File.WriteAllText(stylesheetPath, css);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wwwPath"></param>
        /// <param name="guid"></param>
        public void ScheduleCleanup(string wwwPath, string guid)
        {
            if (string.IsNullOrWhiteSpace(wwwPath) || string.IsNullOrWhiteSpace(guid)) return;

            Task.Run(() =>
            {
                Thread.Sleep(5000);
                var stylesheetPath = Path.Combine(wwwPath, "css", "generated", $"{guid}.min.css");

                if (File.Exists(stylesheetPath)) try { File.Delete(stylesheetPath); } catch { }
            });
        }

    }
}
