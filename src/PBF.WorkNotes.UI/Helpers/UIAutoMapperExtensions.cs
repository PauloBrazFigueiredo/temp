namespace PBF.WorkNotes.UI.Helpers;

public static class UIAutoMapperExtensions
{
    public static IServiceCollection AddUIAutoMapper(this IServiceCollection services, ILoggerFactory loggerFactory)
    {
        var config = new MapperConfiguration(config =>
        {
            config.AllowNullDestinationValues = true;
            config.AllowNullCollections = true;
            config.AddProfile<UIMappingProfile>();
            config.AddProfile<SQLiteGatewayMappingProfile>();
        }, loggerFactory);
        services.AddSingleton<IMapper>(new Mapper(config));
        return services;
    }
}