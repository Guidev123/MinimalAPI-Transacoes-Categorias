using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyFinance.API;
using MyFinance.API.Application.Api;
using MyFinance.Shared;
using MyFinance.Shared.Entities;
using MyFinance.Shared.Handlers;
using MyFinance.Shared.Requests.Categories;
using MyFinance.Shared.Responses;

namespace MyFinance.API.Endpoints.Categories
{
    public class GetAllCategoriesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
                .WithName("Categories: Get All")
                .WithSummary("Recuperar todas as categorias")
                .WithOrder(5)
                .Produces<PagedResponse<List<Category>?>>();

        private static async Task<IResult> HandleAsync(
            ICategoryHandler handler,
            [FromQuery] int pageNumber = Config.DefaultPageNumer,
            [FromQuery] int pageSize = Config.DefaultPageSize)
        {
            var command = new GetAllCategoryCommand
            {
                UserId = ApiConfiguration.UserId,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            var result = await handler.GetAllAsync(command);
            if (result.IsSucces) return TypedResults.Ok(result);
            return TypedResults.BadRequest(result);
        }
    }
}
