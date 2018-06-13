using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyExam.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyExam.Services.ModelConfig
{
    class StuPaperConfig : IEntityTypeConfiguration<StuPaperEntity>
    {
        public void Configure(EntityTypeBuilder<StuPaperEntity> builder)
        {
            builder.ToTable("T_StuPapers");
            builder.Property(u => u.Question).IsRequired().HasMaxLength(500);
            builder.Property(u => u.Answer).IsRequired().HasMaxLength(500);
            builder.Property(u => u.Score).IsRequired().HasMaxLength(50);
        }
    }
}
