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
        public static void AddDbContextService<T>(this ContainerBuilder services) where T : DbContext
        {
            services.Register(q => new DbContextService<T>()).AsImplementedInterfaces().InstancePerLifetimeScope();          
        }

        /// <summary>
        /// IOC注册
        /// </summary>   
        public static void AddDbContextService<T>(this ContainerBuilder services, DatabaseConfiguration databaseConfiguration) where T : DbContext
        {
            services.Register(q => new DbContextService<T>(databaseConfiguration)).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
