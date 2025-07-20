using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TriviaRoyaleGame.Client;
using TriviaRoyaleGame.Client.Business.Services.Class;
using TriviaRoyaleGame.Client.Business.Services.Interface;
using TriviaRoyaleGame.Client.Domain.Models.Settings;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Fill App Setting from appsetting.json
try
{
    var appSettingsBaseSettingsApp = builder.Configuration.GetSection("BaseSettingsApp").Get<BaseSettingsApp>() ?? new()
    {
        BaseTitleApp = string.Empty,
        BaseUrlApiAndroidHttp = string.Empty,
        BaseUrlApiWebHttp = string.Empty,
        ChosenEnviroment = string.Empty
    };
    builder.Services.AddSingleton(appSettingsBaseSettingsApp);

    //var appSettingsJwt = builder.Configuration.GetSection("Jwt").Get<JwtAppSettings>() ?? new()
    //{
    //    Key = string.Empty,
    //    Issuer = string.Empty,
    //    Audience = string.Empty,
    //    JwtTokenValidite = 0
    //};
    //builder.Services.AddSingleton(appSettingsJwt);
}
catch (Exception ex)
{
    throw new Exception(ex.Message, ex);
}

// Register a base path or URL for your application
builder.Services.AddSingleton<ISourceAppProvider>(new SourceAppProvider("Web"));

await builder.Build().RunAsync();
