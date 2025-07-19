namespace TriviaRoyaleGame.Api.Extensions.Add;
public static class AddCors
{
    public static void AddCORS(this IServiceCollection self, IConfiguration configuration)
    {
        self.AddCors(option =>
        {
            option.AddPolicy(configuration.GetSection("CorsName").Value ?? "",
                builder => builder.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader()
                                  .SetIsOriginAllowed(origin => true) // Allow all origins
                                  .Build()
                            );
        });
    }
}
