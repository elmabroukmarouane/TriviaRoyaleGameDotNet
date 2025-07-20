using AutoMapper;
using System.Reflection;

namespace TriviaRoyaleGame.Api.Extensions.Add;

public static class AddAutoMapper
{
    public static void AddAUTOMAPPER( this IServiceCollection self, Action<IMapperConfigurationExpression>? config = null, params Assembly[] assemblies)
    {
        if (assemblies == null || assemblies.Length == 0)
        {
            assemblies = [Assembly.GetExecutingAssembly()];
        }

        self.AddAutoMapper(cfg =>
        {
            config?.Invoke(cfg);
        }, assemblies);
    }
}
