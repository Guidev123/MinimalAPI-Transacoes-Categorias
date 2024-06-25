using Microsoft.EntityFrameworkCore;
using MyFinance.Infra.Context;
using MyFinance.Shared.Commands.Transactions;
using MyFinance.Shared.Entities;
using MyFinance.Shared.Handlers;
using MyFinance.Shared.Responses;
using MyFinance.Shared.Extensions;
using System;

namespace MyFinance.API.Handlers
{
    public class TransactionHandler(MyFinanceDbContext _context) : ITransactionHandler
    {
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionCommand command)
        {
            if (command is { TransactionType: MyFinance.Shared.Enums.ETransactionType.Withdraw, Amount: >= 0 })
                command.Amount *= -1;

            try
            {
                var transaction = new Transaction
                {
                    UserId = command.UserId,
                    CategoryId = command.CategoryId,
                    CreatedAt = DateTime.Now,
                    Amount = command.Amount,
                    PaidOrReceivedAt = command.PaidOrReceivedAt,
                    Title = command.Title,
                    TransactionType = command.TransactionType
                };

                await _context.Transactions.AddAsync(transaction);
                await _context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 201, "Transação criada com sucesso!");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possível criar sua transação");
            }
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionCommand command)
        {
            if (command is { TransactionType: MyFinance.Shared.Enums.ETransactionType.Withdraw, Amount: >= 0 })
                command.Amount *= -1;

            try
            {
                var transaction = await _context
                    .Transactions
                    .FirstOrDefaultAsync(x => x.Id == command.Id && x.UserId == command.UserId);

                if (transaction is null)
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");

                transaction.CategoryId = command.CategoryId;
                transaction.Amount = command.Amount;
                transaction.Title = command.Title;
                transaction.TransactionType = command.TransactionType;
                transaction.PaidOrReceivedAt = command.PaidOrReceivedAt;

                _context.Transactions.Update(transaction);
                await _context.SaveChangesAsync();

                return new Response<Transaction?>(transaction);
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possível recuperar sua transação");
            }
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionCommand command)
        {
            try
            {
                var transaction = await _context
                    .Transactions
                    .FirstOrDefaultAsync(x => x.Id == command.Id && x.UserId == command.UserId);

                if (transaction is null)
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");

                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();

                return new Response<Transaction?>(transaction);
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possível recuperar sua transação");
            }
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdCommand command)
        {
            try
            {
                var transaction = await _context
                    .Transactions
                    .FirstOrDefaultAsync(x => x.Id == command.Id && x.UserId == command.UserId);

                return transaction is null
                    ? new Response<Transaction?>(null, 404, "Transação não encontrada")
                    : new Response<Transaction?>(transaction);
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possível recuperar sua transação");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodCommand command)
        {
            try
            {
                command.StartDate ??= DateTime.Now.GetFirstDayOfMonth();
                command.EndDate ??= DateTime.Now.GetLastDayOfMonth();
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500,
                    "Não foi possível determinar a data de início ou término");
            }

            try
            {
                var query = _context
                    .Transactions
                    .AsNoTracking()
                    .Where(x =>
                        x.PaidOrReceivedAt >= command.StartDate &&
                        x.PaidOrReceivedAt <= command.EndDate &&
                        x.UserId == command.UserId)
                    .OrderBy(x => x.PaidOrReceivedAt);

                var transactions = await query
                    .Skip((command.PageNumber - 1) * command.PageSize)
                    .Take(command.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(
                    transactions,
                    count,
                    command.PageNumber,
                    command.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possível obter as transações");
            }
        }
    }
}
