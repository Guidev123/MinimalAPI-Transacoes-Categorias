using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinance.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Infra.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.TransactionType)
                .IsRequired()
                .HasColumnType("SMALLINT");

            builder.Property(x => x.Amount)
                .IsRequired()
                .HasColumnType("MONEY");

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.PaidOrReceivedAt)
                .IsRequired(false);

            builder.Property(x => x.UserId)
                .IsRequired(true)
                .HasColumnType("VARCHAR")
                .HasMaxLength(160);
        }
    }
}
