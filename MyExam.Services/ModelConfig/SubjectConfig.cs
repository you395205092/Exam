using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyExam.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyExam.Services.ModelConfig
{
    class SubjectConfig : IEntityTypeConfiguration<SubjectEntity>
    {
        public void Configure(EntityTypeBuilder<SubjectEntity> builder)
        {
            builder.ToTable("T_Subjects");
            builder.Property(u => u.Name).IsRequired().HasMaxLength(250);
            builder.Property(u => u.ItemA).IsRequired().HasMaxLength(250);
            builder.Property(u => u.ItemB).IsRequired().HasMaxLength(250);
            builder.Property(u => u.Answer).IsRequired().HasMaxLength(10);
            builder.Property(u => u.TypeId).IsRequired();
            builder.HasQueryFilter(u => u.IsDeleted == false);
        }
    }
}
