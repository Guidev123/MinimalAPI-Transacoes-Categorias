using Microsoft.Extensions.Configuration;
using MyFinance.API;
using MyFinance.API.Application.Api;
using MyFinance.Shared.Commands.Transactions;
using MyFinance.Shared.Entities;
using MyFinance.Shared.Handlers;
using MyFinance.Shared.Responses;

namespace MyFinance.API.Endpoints.Transactions
{
    public class UpdateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/{id}", HandleAsync)
                .WithName("Transactions: Update")
                .WithSummary("Atualizar uma transação")
                .WithOrder(2)
                .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandleAsync(
        ITransactionHandler handler,
            UpdateTransactionCommand command,
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
