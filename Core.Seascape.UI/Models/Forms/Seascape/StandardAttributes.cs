using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Seascape.UI.Models.Forms.Seascape
{
    public class StandardAttributes
    {
        [UIHint("IntegerTemplate")]
        [Range(10, 1000000)]
        [Display(Name = "Cloud Cover")]
        public int CloudCover { get; set; } = 100000;

        [UIHint("IntegerTemplate")]
        [Range(10, 1070)]
        [Display(Name = "Sun Size")]
        public int SunSize { get; set; } = 50;

        [UIHint("IntegerTemplate")]
        [Range(10, 1070)]
        [Display(Name = "Water Ripple")]
        public int WaterRipple { get; set; } = 48;

        [UIHint("IntegerTemplate")]
        [Range(10, 1070)]
        [Display(Name = "Hue")]
        public int Hue { get; set; } = 500;

        [UIHint("IntegerTemplate")]
        [Range(540, 540)]
        [Display(Name = "Y Divisor")]
        public int YDivisor { get; set; } = 540;
    }
}
