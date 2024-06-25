using MyFinance.Shared.Commands.Transactions;
using MyFinance.Shared.Entities;
using MyFinance.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Shared.Handlers
{
    public interface ITransactionHandler
    {
        Task<Response<Transaction?>> CreateAsync(CreateTransactionCommand command);
        Task<Response<Transaction?>> UpdateAsync(UpdateTransactionCommand command);
        Task<Response<Transaction?>> DeleteAsync(DeleteTransactionCommand command);
        Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdCommand command);
        Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodCommand command);
    }
}
