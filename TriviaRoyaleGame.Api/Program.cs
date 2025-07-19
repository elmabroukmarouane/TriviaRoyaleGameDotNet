using TriviaRoyaleGame.Api.Extensions.Add;
using TriviaRoyaleGame.Api.Extensions.Use;
using TriviaRoyaleGame.Api.RealTime.Class;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });

builder.Services.AddMemoryCache(options =>
{
    options.SizeLimit = 1024 * 1024 * 1000;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSWAGGER();
builder.Services.AddConnection(builder.Configuration, builder.Environment);
builder.Services.AddEmailSmtpConfigurationExtension(builder.Configuration);
builder.Services.AddSERVICES(builder.Configuration, builder.Environment);
builder.Services.AddJWT(builder.Configuration);
builder.Services.AddAutoMapper(cfg => {}, AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCORS(builder.Configuration);
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCORS(app.Configuration);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseJWT();


app.MapControllers();
app.MapHub<RealTimeHub>("/realTimeHub");

await app.RunAsync();
