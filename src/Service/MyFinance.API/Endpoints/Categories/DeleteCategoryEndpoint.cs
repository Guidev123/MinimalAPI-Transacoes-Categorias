using Microsoft.Extensions.Configuration;
using MyFinance.API.Application.Api;
using MyFinance.Shared.Entities;
using MyFinance.Shared.Handlers;
using MyFinance.Shared.Requests.Categories;
using MyFinance.Shared.Responses;

namespace MyFinance.API.Endpoints.Categories
{
    public class DeleteCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapDelete("/{id}", HandleAsync)
                .WithName("Categories: Delete")
                .WithSummary("Excluir uma categoria")
                .WithOrder(3)
                .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync(
        ICategoryHandler handler,
            long id)
        {
            var command = new DeleteCategoryCommand
            {
                UserId = ApiConfiguration.UserId,
                Id = id
            };

            var result = await handler.DeleteAsync(command);
            if (result.IsSucces) return TypedResults.Ok(result);
            return TypedResults.BadRequest(result);
        }
    }
}
