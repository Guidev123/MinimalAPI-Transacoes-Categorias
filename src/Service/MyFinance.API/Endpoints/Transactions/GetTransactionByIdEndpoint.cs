using Microsoft.Extensions.Configuration;
using MyFinance.API.Application.Api;
using MyFinance.Shared.Commands.Transactions;
using MyFinance.Shared.Entities;
using MyFinance.Shared.Handlers;
using MyFinance.Shared.Responses;

namespace MyFinance.API.Endpoints.Transactions
{
    public class GetTransactionByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{id}", HandleAsync)
                .WithName("Transactions: Get By Id")
                .WithSummary("Recuperar uma transação")
                .WithOrder(4)
                .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandleAsync(
            ITransactionHandler handler,
            long id)
        {
            var command = new GetTransactionByIdCommand
            {
                UserId = ApiConfiguration.UserId,
                Id = id
            };

            var result = await handler.GetByIdAsync(command);
            if (result.IsSucces) return TypedResults.Ok(result);
            return TypedResults.BadRequest(result);
        }
    }
}
