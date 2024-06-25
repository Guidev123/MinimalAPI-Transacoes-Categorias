using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyFinance.API;
using MyFinance.API.Application.Api;
using MyFinance.Shared;
using MyFinance.Shared.Commands.Transactions;
using MyFinance.Shared.Entities;
using MyFinance.Shared.Handlers;
using MyFinance.Shared.Responses;

namespace MyFinance.API.Endpoints.Transactions
{
    public class GetTransactionsByPeriodEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
                .WithName("Transactions: Get All")
                .WithSummary("Recuperar todas as transações")
                .WithOrder(5)
                .Produces<PagedResponse<List<Transaction>?>>();

        private static async Task<IResult> HandleAsync(
            ITransactionHandler handler,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int pageNumber = Config.DefaultPageNumer,
            [FromQuery] int pageSize = Config.DefaultPageSize)
        {
            var command = new GetTransactionsByPeriodCommand
            {
                UserId = ApiConfiguration.UserId,
                PageNumber = pageNumber,
                PageSize = pageSize,
                StartDate = startDate,
                EndDate = endDate
            };

            var result = await handler.GetByPeriodAsync(command);
            if (result.IsSucces) return TypedResults.Ok(result);
            return TypedResults.BadRequest(result);
        }
    }
}
