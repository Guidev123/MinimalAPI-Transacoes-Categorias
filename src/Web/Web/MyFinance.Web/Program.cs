using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MyFinance.Shared;
using MyFinance.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient(WebConfig.HttpClientName, opt =>
{
    opt.BaseAddress = new Uri(Config.BackendUrl);
});
builder.Services.AddMudServices();

await builder.Build().RunAsync();
