﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using AdventureWorksSalesDashboard.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace AdventureWorksSalesDashboard.Entities.Configurations
{
    public partial class ProductInventoryConfiguration : IEntityTypeConfiguration<ProductInventory>
    {
        public void Configure(EntityTypeBuilder<ProductInventory> entity)
        {
            entity.HasKey(e => new { e.ProductId, e.LocationId }).HasName("PK_ProductInventory_ProductID_LocationID");

            entity.ToTable("ProductInventory", "Production", tb => tb.HasComment("Product inventory information."));

            entity.Property(e => e.ProductId)
                .HasComment("Product identification number. Foreign key to Product.ProductID.")
                .HasColumnName("ProductID");
            entity.Property(e => e.LocationId)
                .HasComment("Inventory location identification number. Foreign key to Location.LocationID. ")
                .HasColumnName("LocationID");
            entity.Property(e => e.Bin).HasComment("Storage container on a shelf in an inventory location.");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasComment("Quantity of products in the inventory location.");
            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                .HasColumnName("rowguid");
            entity.Property(e => e.Shelf)
                .HasMaxLength(10)
                .HasComment("Storage compartment within an inventory location.");

            entity.HasOne(d => d.Location).WithMany(p => p.ProductInventories)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductInventories)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<ProductInventory> entity);
    }
}
