using MyFinance.Shared.Enums;
using MyFinance.Shared.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Shared.Commands.Transactions
{
    public class UpdateTransactionCommand : Command
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Título em formato inválido")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tipo inválido")]
        public ETransactionType TransactionType { get; set; }
        [Required(ErrorMessage = "Valor inválido")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Categoria inválida")]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Data inválida")]
        public DateTime PaidOrReceivedAt { get; set; }
    }
}
