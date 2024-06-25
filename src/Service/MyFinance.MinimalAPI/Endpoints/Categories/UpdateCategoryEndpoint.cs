using Microsoft.Extensions.Configuration;
using MyFinance.API;
using MyFinance.API.Application.Api;
using MyFinance.Shared.Entities;
using MyFinance.Shared.Handlers;
using MyFinance.Shared.Requests.Categories;
using MyFinance.Shared.Responses;

namespace MyFinance.API.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/{id}", HandleAsync)
                .WithName("Categories: Update")
                .WithSummary("Atualizar uma categoria")
                .WithOrder(2)
                .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync(
            ICategoryHandler handler,
            UpdateCategoryCommand command,
            long id)
        {
            command.UserId = ApiConfiguration.UserId;
            command.Id = id;

            var result = await handler.UpdateAsync(command);
            if (result.IsSucces) return TypedResults.Ok(result);
            return TypedResults.BadRequest(result);
        }
    }
}
