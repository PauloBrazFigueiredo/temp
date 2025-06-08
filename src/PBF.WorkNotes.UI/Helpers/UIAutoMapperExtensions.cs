namespace PBF.WorkNotes.UI.Helpers;

public static class UIAutoMapperExtensions
{
    public static IServiceCollection AddUIAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UIMappingProfile));
        //var mappingConfig = new MapperConfiguration(config =>
        //{
        //    config.AddProfile(new MappingProfile());
        //});
        //IMapper mapper = mappingConfig.CreateMapper();
        //services.AddSingleton(mapper);

        return services;
    }
}