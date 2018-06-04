using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MyExam.Services
{
    public static class EntityFrameworkCoreExtensions
    {
        //    private static bool IsIEntityTypeConfigurationType(Type typeIntf)
        //    {
        //        return typeIntf.IsInterface && typeIntf.IsGenericType && typeIntf.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>);
        //    }

        //    public static void ApplyConfigurationsFromAssembly(this ModelBuilder modelBuilder, Assembly assembly)
        //    {
        //        //筛选出继承自IEntityTypeConfiguration的类型
        //        IEnumerable<Type> types = assembly.GetTypes().Where(t => !t.IsAbstract && t.GetInterfaces().Any(it => IsIEntityTypeConfigurationType(it)));
        //        Type typeModelBuilder = modelBuilder.GetType();
        //        MethodInfo methodNonGenericApplyConfiguration = typeModelBuilder.GetMethod(nameof(ModelBuilder.ApplyConfiguration));
        //        foreach (var type in types)
        //        {
        //            object entityTypeConfig = Activator.CreateInstance(type);
        //            //获取实体的类型
        //            Type typeEntity = type.GetInterfaces().First(t => IsIEntityTypeConfigurationType(t)).GenericTypeArguments[0];
        //            //通过MakeGenericMethod转换为泛型方法
        //            MethodInfo methodApplyConfiguration = methodNonGenericApplyConfiguration.MakeGenericMethod(typeEntity);
        //            methodApplyConfiguration.Invoke(modelBuilder, new[] { entityTypeConfig });
        //        }
        //    }
        //}

        private static bool IsIEntityTypeConfigurationType(Type typeIntf)
        {
            return typeIntf.IsInterface && typeIntf.IsGenericType && typeIntf.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>);
        }

        public static void ApplyConfigurationsFromAssembly(this ModelBuilder modelBuilder, Assembly assembly)
        {
            //筛选出继承自IEntityTypeConfiguration的类型
            IEnumerable<Type> types = assembly.GetTypes().Where(t => !t.IsAbstract && t.GetInterfaces().Any(it => IsIEntityTypeConfigurationType(it)));
            Type typeModelBuilder = modelBuilder.GetType();
            MethodInfo methodNonGenericApplyConfiguration = typeModelBuilder.GetMethods()
                .Where(m => m.IsGenericMethod && m.Name == nameof(ModelBuilder.ApplyConfiguration) && m.GetParameters().Any(s => s.ParameterType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))).First();
            foreach (var type in types)
            {
                object entityTypeConfig = Activator.CreateInstance(type);
                //获取实体的类型
                Type typeEntity = type.GetInterfaces().First(t => IsIEntityTypeConfigurationType(t)).GenericTypeArguments[0];
                //通过MakeGenericMethod转换为泛型方法
                MethodInfo methodApplyConfiguration = methodNonGenericApplyConfiguration.MakeGenericMethod(typeEntity);
                methodApplyConfiguration.Invoke(modelBuilder, new[] { entityTypeConfig });
            }
        }
    }
}
