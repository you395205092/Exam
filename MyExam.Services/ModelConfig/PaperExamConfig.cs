using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyExam.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyExam.Services.ModelConfig
{
    class PaperExamConfig : IEntityTypeConfiguration<PaperExamEntity>
    {
        public void Configure(EntityTypeBuilder<PaperExamEntity> builder)
        {
            builder.ToTable("T_PaperExams");
            builder.Property(u => u.Name).IsRequired().HasMaxLength(250);
            builder.Property(u => u.PCount).IsRequired().HasMaxLength(200);
            builder.Property(u => u.PScore).IsRequired().HasMaxLength(200);
            builder.Property(u => u.BTime).IsRequired();
            builder.Property(u => u.ETime).IsRequired();
        }
    }
}
