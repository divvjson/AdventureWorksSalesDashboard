﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using AdventureWorksSalesDashboard.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace AdventureWorksSalesDashboard.Entities.Configurations
{
    public partial class TransactionHistoryConfiguration : IEntityTypeConfiguration<TransactionHistory>
    {
        public void Configure(EntityTypeBuilder<TransactionHistory> entity)
        {
            entity.HasKey(e => e.TransactionId).HasName("PK_TransactionHistory_TransactionID");

            entity.ToTable("TransactionHistory", "Production", tb => tb.HasComment("Record of each purchase order, sales order, or work order transaction year to date."));

            entity.HasIndex(e => e.ProductId, "IX_TransactionHistory_ProductID");

            entity.HasIndex(e => new { e.ReferenceOrderId, e.ReferenceOrderLineId }, "IX_TransactionHistory_ReferenceOrderID_ReferenceOrderLineID");

            entity.Property(e => e.TransactionId)
                .HasComment("Primary key for TransactionHistory records.")
                .HasColumnName("TransactionID");
            entity.Property(e => e.ActualCost)
                .HasComment("Product cost.")
                .HasColumnType("money");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.ProductId)
                .HasComment("Product identification number. Foreign key to Product.ProductID.")
                .HasColumnName("ProductID");
            entity.Property(e => e.Quantity).HasComment("Product quantity.");
            entity.Property(e => e.ReferenceOrderId)
                .HasComment("Purchase order, sales order, or work order identification number.")
                .HasColumnName("ReferenceOrderID");
            entity.Property(e => e.ReferenceOrderLineId)
                .HasComment("Line number associated with the purchase order, sales order, or work order.")
                .HasColumnName("ReferenceOrderLineID");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time of the transaction.")
                .HasColumnType("datetime");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasComment("W = WorkOrder, S = SalesOrder, P = PurchaseOrder");

            entity.HasOne(d => d.Product).WithMany(p => p.TransactionHistories)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<TransactionHistory> entity);
    }
}
