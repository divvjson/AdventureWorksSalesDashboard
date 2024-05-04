﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using AdventureWorksSalesDashboard.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace AdventureWorksSalesDashboard.Entities.Configurations
{
    public partial class SalesPersonQuotaHistoryConfiguration : IEntityTypeConfiguration<SalesPersonQuotaHistory>
    {
        public void Configure(EntityTypeBuilder<SalesPersonQuotaHistory> entity)
        {
            entity.HasKey(e => new { e.BusinessEntityId, e.QuotaDate }).HasName("PK_SalesPersonQuotaHistory_BusinessEntityID_QuotaDate");

            entity.ToTable("SalesPersonQuotaHistory", "Sales", tb => tb.HasComment("Sales performance tracking."));

            entity.HasIndex(e => e.Rowguid, "AK_SalesPersonQuotaHistory_rowguid").IsUnique();

            entity.Property(e => e.BusinessEntityId)
                .HasComment("Sales person identification number. Foreign key to SalesPerson.BusinessEntityID.")
                .HasColumnName("BusinessEntityID");
            entity.Property(e => e.QuotaDate)
                .HasComment("Sales quota date.")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                .HasColumnName("rowguid");
            entity.Property(e => e.SalesQuota)
                .HasComment("Sales quota amount.")
                .HasColumnType("money");

            entity.HasOne(d => d.BusinessEntity).WithMany(p => p.SalesPersonQuotaHistories)
                .HasForeignKey(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<SalesPersonQuotaHistory> entity);
    }
}