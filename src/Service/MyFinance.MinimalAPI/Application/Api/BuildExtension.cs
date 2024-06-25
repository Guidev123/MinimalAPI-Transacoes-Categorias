using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyFinance.API.Handlers;
using MyFinance.Infra.Context;
using MyFinance.Shared;
using MyFinance.Shared.Handlers;
using System;

namespace MyFinance.API.Application.Api
{
    public static class BuildExtension
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            Config.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
            Config.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
            Config.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
        }

        public static void AddDocumentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.CustomSchemaIds(n => n.FullName);
            });
        }

        public static void AddDataContexts(this WebApplicationBuilder builder)
        {
            builder
            .Services
                .AddDbContext<MyFinanceDbContext>(
                    x =>
                    {
                        x.UseSqlServer(Config.ConnectionString);
                    });
        }

        public static void AddCrossOrigin(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(
                options => options.AddPolicy(
                    ApiConfiguration.CorsPolicyName,
                    policy => policy
                        .WithOrigins([
                            Config.BackendUrl,
                        Config.FrontendUrl
                        ])
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                ));
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder
                .Services
                .AddTransient<ICategoryHandler, CategoryHandler>();

            builder
                .Services
                .AddTransient<ITransactionHandler, TransactionHandler>();
        }
    }
}
