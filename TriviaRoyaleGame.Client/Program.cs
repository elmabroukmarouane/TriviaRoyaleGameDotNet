using Blazored.LocalStorage;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using TriviaRoyaleGame.Client;
using TriviaRoyaleGame.Client.Business.Providers.Classes;
using TriviaRoyaleGame.Client.Business.Providers.Interfaces;
using TriviaRoyaleGame.Client.Business.Services.AuthenticationService.Classe;
using TriviaRoyaleGame.Client.Business.Services.AuthenticationService.Interface;
using TriviaRoyaleGame.Client.Business.Services.GenericService.Class;
using TriviaRoyaleGame.Client.Business.Services.GenericService.Interface;
using TriviaRoyaleGame.Client.Domain.Models;
using TriviaRoyaleGame.Client.Domain.Models.Settings;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredModal();
builder.Services.AddSweetAlert2(options =>
{
    options.Theme = SweetAlertTheme.Dark;
    options.SetThemeForColorSchemePreference(ColorScheme.Light, SweetAlertTheme.Default);
    options.SetThemeForColorSchemePreference(ColorScheme.Dark, SweetAlertTheme.Dark);
});
builder.Services.AddRadzenComponents();
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

    var appSettingsJwt = builder.Configuration.GetSection("Jwt").Get<JwtAppSettings>() ?? new()
    {
        Key = string.Empty,
        Issuer = string.Empty,
        Audience = string.Empty,
        JwtTokenValidite = 0
    };
    builder.Services.AddSingleton(appSettingsJwt);
}
catch (Exception ex)
{
    throw new Exception(ex.Message, ex);
}

// Register a base path or URL for your application
builder.Services.AddSingleton<ISourceAppProvider>(new SourceAppProvider("Web"));

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<AuthenticationStateProvider, ClientAppAuthenticationStateProvider>();
builder.Services.AddTransient<IGenericService<ClientAppLogViewModel>, GenericService<ClientAppLogViewModel>>();
builder.Services.AddTransient<IGenericService<MemberViewModel>, GenericService<MemberViewModel>>();
builder.Services.AddTransient<IGenericService<QuestionViewModel>, GenericService<QuestionViewModel>>();
builder.Services.AddTransient<IGenericService<UserViewModel>, GenericService<UserViewModel>>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();

await builder.Build().RunAsync();
