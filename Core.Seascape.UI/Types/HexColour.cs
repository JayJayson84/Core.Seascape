using System;

namespace Core.Seascape.UI.Types
{
    public struct HexColour
    {
        private readonly int hexColour;

        public HexColour(string colour)
        {
            hexColour = Convert.ToInt32(colour.Replace("#", "0x"), 16);
        }

        public static implicit operator int(HexColour h) => h.hexColour;
        public static explicit operator HexColour(string c) => new HexColour(c);
    }
}
