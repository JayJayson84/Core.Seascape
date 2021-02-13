using System.ComponentModel.DataAnnotations;

namespace Core.Seascape.UI.Models.Forms.Seascape
{
    public class ColourAttributes
    {
        [UIHint("ColourTemplate")]
        [Display(Name = "Stratosphere Colour")]
        public string StratosphereColour { get; set; } = "#906050";

        [UIHint("ColourTemplate")]
        [Display(Name = "Stratosphere Cloud Colour")]
        public string StratosphereCloudColour { get; set; } = "#D0D0D0";

        [UIHint("ColourTemplate")]
        [Display(Name = "Troposphere Colour")]
        public string TroposphereColour { get; set; } = "#908001";

        [UIHint("ColourTemplate")]
        [Display(Name = "Troposphere Cloud Colour")]
        public string TroposphereCloudColour { get; set; } = "#807080";

        [UIHint("ColourTemplate")]
        [Display(Name = "Sun Colour")]
        public string SunColour { get; set; } = "#FFFFFF";

        [UIHint("ColourTemplate")]
        [Display(Name = "Sun Rays Colour")]
        public string SunRaysColour { get; set; } = "#FFFFFF";

        [UIHint("ColourTemplate")]
        [Display(Name = "Water Colour")]
        public string WaterColour { get; set; } = "#352520";
    }
}
