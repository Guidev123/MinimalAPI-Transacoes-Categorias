using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MyFinance.Shared;
using MyFinance.Shared.Handlers;
using MyFinance.Web;
using MyFinance.Web.Handlers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient(WebConfig.HttpClientName, opt =>
{
    opt.BaseAddress = new Uri(Config.BackendUrl);
});
builder.Services.AddMudServices();

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

await builder.Build().RunAsync();
