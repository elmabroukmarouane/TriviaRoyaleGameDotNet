namespace TriviaRoyaleGame.Api.Extensions.Use;
public static class UseCors
{
    public static void UseCORS(this IApplicationBuilder self, IConfiguration configuration)
    {
        self.UseCors(configuration.GetSection("CorsName").Value ?? "");
    }
}
