using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Seascape.UI.Models.Forms.Seascape
{
    public class AdvancedAttributes
    {
        [UIHint("IntegerTemplate")]
        [Range(1200, 1900)]
        [Display(Name = "KXL")]
        public int KXL { get; set; } = 1700;

        [UIHint("IntegerTemplate")]
        [Range(600, 1900)]
        [Display(Name = "KXR")]
        public int KXR { get; set; } = 900;

        [UIHint("IntegerTemplate")]
        [Range(520, 540)]
        [Display(Name = "K1L")]
        public int K1L { get; set; } = 539;

        [UIHint("IntegerTemplate")]
        [Range(540, 560)]
        [Display(Name = "K1R")]
        public int K1R { get; set; } = 540;

        [UIHint("IntegerTemplate")]
        [Range(600, 1900)]
        [Display(Name = "K1C")]
        public int K1C { get; set; } = 960;

        [UIHint("IntegerTemplate")]
        [Range(10, 539)]
        [Display(Name = "K1F")]
        public int K1F { get; set; } = 539;
    }
}
