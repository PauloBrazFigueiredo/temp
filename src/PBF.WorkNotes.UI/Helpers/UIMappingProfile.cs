namespace PBF.WorkNotes.UI.Helpers;

public class UIMappingProfile : Profile
{
    public UIMappingProfile()
    {
        CreateMap<Priority, Entities.Priority>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => ColorToBrushConverter.BrushToString(src.Color)))
            .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => src.IsDefault));
        CreateMap<Entities.Priority, Priority>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => ColorToBrushConverter.StringToBrush(src.Color)))
            .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => src.IsDefault));
        CreateMap<ToDoState, Entities.ToDoState>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => src.IsDefault));
        CreateMap<Entities.ToDoState, ToDoState>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(src => src.IsDefault));
    }
}
