using Microsoft.AspNetCore.Mvc;
using MyFinance.API.Application.Api;
using MyFinance.Shared.Entities;
using MyFinance.Shared.Handlers;
using MyFinance.Shared.Requests.Categories;
using MyFinance.Shared.Responses;

namespace MyFinance.API.Endpoints.Categories
{
    public class CreateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/", HandleAsync)
                .WithName("Categories: Create")
                .WithSummary("Criar uma categoria nova")
                .WithOrder(1)
                .Produces<Response<Category>>();
        }

        private static async Task<IResult> HandleAsync(ICategoryHandler handler, CreateCategoryCommand command)
        {
            command.UserId = ApiConfiguration.UserId;
            var response = await handler.CreateAsync(command);
            if (response.IsSucces) return TypedResults.Created($"v1/categories/{response.Data?.Id}", response);
            return TypedResults.BadRequest(response);
        }
    }
}
