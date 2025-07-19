namespace TriviaRoyaleGame.Api.Extensions.Use;
public static class UseJwt
{
    public static void UseJWT(this IApplicationBuilder self)
    {
        self.UseAuthentication();
    }
}
