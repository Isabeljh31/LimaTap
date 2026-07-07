using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TransitSystem.Frontend;
using TransitSystem.Frontend.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);

var apiBaseAddress = builder.Configuration["ApiBaseAddress"];
if (string.IsNullOrWhiteSpace(apiBaseAddress))
{
    apiBaseAddress = "https://localhost:7122";
}

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseAddress) });
builder.Services.AddSingleton<TransitSystem.Frontend.Services.TransactionService>();

// Registro de nuestro servicio proxy para poder usarlo en las pantallas
builder.Services.AddScoped<TransitApiService>();

await builder.Build().RunAsync();
