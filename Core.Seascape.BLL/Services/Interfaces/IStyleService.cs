using Core.Seascape.BLL.Models;

namespace Core.Seascape.BLL.Services
{
    public interface IStyleService
    {
        bool CreateBase64AnimatedCss(SeascapeImageData imageData, string stylesheetPath);
        void ScheduleCleanup(string wwwPath, string guid);
    }
}
