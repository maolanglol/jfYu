using Autofac;
using Microsoft.EntityFrameworkCore;
using System;

namespace jfYu.Core.Data
{
    public static class DbContextServiceExtensions
    {
        /// <summary>
        /// IOC注册
        /// </summary>  
        public static void AddDbContextService<T>(this ContainerBuilder services, Func<DbContextOptions<T>, T> _DbContext) where T : DbContext
        {
            services.Register(c => new DbContextService<T>(_DbContext)).AsSelf().InstancePerLifetimeScope();
        }

        /// <summary>
        /// IOC注册
        /// </summary>   
        public static void AddDbContextService<T>(this ContainerBuilder services, DatabaseConfiguration databaseConfiguration, Func<DbContextOptions<T>, T> _DbContext) where T : DbContext
        {
            services.Register(c => new DbContextService<T>(databaseConfiguration, _DbContext)).AsSelf().InstancePerLifetimeScope();
        }
    }
}
