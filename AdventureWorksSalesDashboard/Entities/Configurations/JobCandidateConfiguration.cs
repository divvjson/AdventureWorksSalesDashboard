﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using AdventureWorksSalesDashboard.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace AdventureWorksSalesDashboard.Entities.Configurations
{
    public partial class JobCandidateConfiguration : IEntityTypeConfiguration<JobCandidate>
    {
        public void Configure(EntityTypeBuilder<JobCandidate> entity)
        {
            entity.HasKey(e => e.JobCandidateId).HasName("PK_JobCandidate_JobCandidateID");

            entity.ToTable("JobCandidate", "HumanResources", tb => tb.HasComment("Résumés submitted to Human Resources by job applicants."));

            entity.HasIndex(e => e.BusinessEntityId, "IX_JobCandidate_BusinessEntityID");

            entity.Property(e => e.JobCandidateId)
                .HasComment("Primary key for JobCandidate records.")
                .HasColumnName("JobCandidateID");
            entity.Property(e => e.BusinessEntityId)
                .HasComment("Employee identification number if applicant was hired. Foreign key to Employee.BusinessEntityID.")
                .HasColumnName("BusinessEntityID");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.Resume)
                .HasComment("Résumé in XML format.")
                .HasColumnType("xml");

            entity.HasOne(d => d.BusinessEntity).WithMany(p => p.JobCandidates).HasForeignKey(d => d.BusinessEntityId);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<JobCandidate> entity);
    }
}
