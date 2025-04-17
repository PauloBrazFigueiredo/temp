namespace PBF.WorkNotes.Gateways.SQLiteGateway.Extensions;

public static class AutoMapperExtensions
{
    public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.AddProfile(new MappingProfile());
        });
        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}