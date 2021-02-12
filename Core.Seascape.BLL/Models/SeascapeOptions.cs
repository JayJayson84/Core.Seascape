namespace Core.Seascape.BLL.Models
{
    public class SeascapeOptions
    {
        public float CloudCover { get; set; } = 100000.0F;
        public float SunSize { get; set; } = 50.0F;
        public float WaterRipple { get; set; } = 48F;
        public float Hue { get; set; } = 500.0F;
        public int YDivisor { get; set; } = 540;
        public int StratosphereColour { get; set; } = 0x906050;
        public int StratosphereCloudColour { get; set; } = 0xD0D0D0;
        public int TroposphereColour { get; set; } = 0x908001;
        public int TroposphereCloudColour { get; set; } = 0x807080;
        public int SunColour { get; set; } = 0xFFFFFF;
        public int SunRaysColour { get; set; } = 0xFFFFFF;
        public int WaterColour { get; set; } = 0x352520;
        public float KXL { get; set; } = 1728F;
        public float KXR { get; set; } = 972F;
        public int K1L { get; set; } = 539;
        public int K1R { get; set; } = 540;
        public int K1C { get; set; } = 960;
        public int K1F { get; set; } = 539;
    }
}
