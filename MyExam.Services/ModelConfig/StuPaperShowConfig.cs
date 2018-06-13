using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyExam.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyExam.Services.ModelConfig
{
    class StuPaperShowConfig : IEntityTypeConfiguration<StuPaperShowEntity>
    {
        public void Configure(EntityTypeBuilder<StuPaperShowEntity> builder)
        {
            builder.ToTable("T_StuPaperShows");
        }
    }
}
