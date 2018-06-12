using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;
using MyExam.Services.Entities;



namespace MyExam.Services
{
    public class MyDbContext:DbContext
    {

        public DbSet<AdminUserEntity> AdminUsers { get; set; }
        public DbSet<SubjectEntity> Subjects { get; set; }
        public DbSet<StudentEntity> Students { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var builder =
                new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())//SetBasePath设置配置文件所在路径
                .AddJsonFile("appsettings.json");
            var configRoot = builder.Build();
            var connString =
                configRoot.GetSection("db").GetSection("ConnectionString").Value;
            optionsBuilder.UseSqlServer(connString);


        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Assembly asmServices = Assembly.Load(new AssemblyName("MyExam.Services"));
            //modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfigurationsFromAssembly(asmServices);
        }
    }
}
