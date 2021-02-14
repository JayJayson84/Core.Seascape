using Core.Seascape.BLL.Models;
using Core.Seascape.UI.Models.Forms.Seascape;
using Core.Seascape.UI.Types;
using AutoMapper;

namespace Core.Seascape.UI.Config
{
    public class SeascapeProfile : Profile
    {
        public SeascapeProfile()
        {
            CreateMap<SeascapeForm, SeascapeOptions>()
                .ForMember(dest => dest.CloudCover, opt => opt.MapFrom(src => (float)src.Attributes.CloudCover))
                .ForMember(dest => dest.SunSize, opt => opt.MapFrom(src => (float)src.Attributes.SunSize))
                .ForMember(dest => dest.WaterRipple, opt => opt.MapFrom(src => (float)src.Attributes.WaterRipple))
                .ForMember(dest => dest.Hue, opt => opt.MapFrom(src => (float)src.Attributes.Hue))
                .ForMember(dest => dest.YDivisor, opt => opt.MapFrom(src => src.Attributes.YDivisor))
                .ForMember(dest => dest.StratosphereColour, opt => opt.MapFrom(src => (HexColour)src.Colourscheme.StratosphereColour))
                .ForMember(dest => dest.StratosphereCloudColour, opt => opt.MapFrom(src => (HexColour)src.Colourscheme.StratosphereCloudColour))
                .ForMember(dest => dest.TroposphereColour, opt => opt.MapFrom(src => (HexColour)src.Colourscheme.TroposphereColour))
                .ForMember(dest => dest.TroposphereCloudColour, opt => opt.MapFrom(src => (HexColour)src.Colourscheme.TroposphereCloudColour))
                .ForMember(dest => dest.SunColour, opt => opt.MapFrom(src => (HexColour)src.Colourscheme.SunColour))
                .ForMember(dest => dest.SunRaysColour, opt => opt.MapFrom(src => (HexColour)src.Colourscheme.SunRaysColour))
                .ForMember(dest => dest.WaterColour, opt => opt.MapFrom(src => (HexColour)src.Colourscheme.WaterColour))
                .ForMember(dest => dest.KXL, opt => opt.MapFrom(src => (float)src.Advanced.KXL))
                .ForMember(dest => dest.KXR, opt => opt.MapFrom(src => (float)src.Advanced.KXR))
                .ForMember(dest => dest.K1L, opt => opt.MapFrom(src => src.Advanced.K1L))
                .ForMember(dest => dest.K1R, opt => opt.MapFrom(src => src.Advanced.K1R))
                .ForMember(dest => dest.K1C, opt => opt.MapFrom(src => src.Advanced.K1C))
                .ForMember(dest => dest.K1F, opt => opt.MapFrom(src => src.Advanced.K1F));
        }
    }
}
