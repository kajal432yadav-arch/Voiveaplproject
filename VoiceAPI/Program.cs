using VoiceAPI.Models;
using VoiceAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.Configure<SarvamOptions>(
    builder.Configuration.GetSection("Sarvam"));

builder.Services.AddHttpClient<ISarvamService, SarvamService>(
    (sp, client) =>
    {
        var config = sp.GetRequiredService<IConfiguration>();

        client.BaseAddress =
            new Uri(config["Sarvam:BaseUrl"]!);

        client.Timeout =
            TimeSpan.FromSeconds(60);
    });

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();