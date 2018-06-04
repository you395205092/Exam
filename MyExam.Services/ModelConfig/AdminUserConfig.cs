using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyExam.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyExam.Services.ModelConfig
{
    class AdminUserConfig : IEntityTypeConfiguration<AdminUserEntity>
    {
        public void Configure(EntityTypeBuilder<AdminUserEntity> builder)
        {
            builder.ToTable("T_AdminUsers");
            builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(50);
            builder.Property(u => u.UserName).IsRequired().HasMaxLength(50);
            builder.HasQueryFilter(u => u.IsDeleted == false);
        }
    }
}
