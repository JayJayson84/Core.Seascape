using Core.Seascape.UI.Models.Partials;

namespace Core.Seascape.UI.Models.Pages
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            AnimatedSeascapeImage = new AnimatedSeascapeImageViewModel();
        }

        public AnimatedSeascapeImageViewModel AnimatedSeascapeImage { get; set; }
    }
}
