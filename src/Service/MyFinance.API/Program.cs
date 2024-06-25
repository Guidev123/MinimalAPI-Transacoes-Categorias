using Microsoft.Extensions.Configuration;
using MyFinance.API;
using MyFinance.API.Application.Api;
using MyFinance.API.Endpoints;
using MyFinance.API.Handlers;
using MyFinance.Shared.Handlers;

//========================================== Environment Configure ===============================================/
var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();
//================================================ End ========================================================/


builder.AddConfiguration();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();
app.UseCors(ApiConfiguration.CorsPolicyName);
app.MapEndpoints();

app.Run();