using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ITI.HMS.BlazorWASM;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient to call HMS API (different origin - will cause CORS error)
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7211") });

await builder.Build().RunAsync();
