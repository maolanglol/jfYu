using Autofac;
using jfYu.Core.Common.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace jfYu.Core.Data
{
    public static class DbContextServiceExtensions
    {

        /// <summary>
        /// IOC注册
        /// </summary>  
        public static void AddDbContextService<T>(this ContainerBuilder services) where T : DbContext
        {
            // 主从数据库配置
            DatabaseConfiguration Config = new DatabaseConfiguration();
            try
            {
                Config = AppConfig.GetSection("ConnectionStrings").GetBindData<DatabaseConfiguration>();

            }
            catch (Exception ex)
            {
                throw new Exception($"连接字符串配置错误:{ex.Message} - {ex.StackTrace}");
            }
            //注册MasterDBContext
            var masterOptBuilder = new DbContextOptionsBuilder<T>();
            if (Config.DatabaseType.Equals(DatabaseType.SqlServer))
                masterOptBuilder.UseLazyLoadingProxies().UseSqlServer(Config.MasterConnectionString);
            else if (Config.DatabaseType.Equals(DatabaseType.Mysql))
                masterOptBuilder.UseLazyLoadingProxies().UseMySQL(Config.MasterConnectionString);
            services.RegisterType<T>().AsSelf().InstancePerLifetimeScope().WithParameter("options", masterOptBuilder.Options).Named<T>("MasterContext");

            //注册SalveDBContext
            int slaveCount = Config.SlaveConnectionStrings.Count;
            if (slaveCount == 1)
                services.RegisterType<T>().AsSelf().InstancePerLifetimeScope().WithParameter("options", masterOptBuilder.Options).Named<T>("SalveContext");
            else
            {
                Random r = new Random();
                int i = r.Next(0, slaveCount);//随机一个从数据库
                var SlaveOptBuilder = new DbContextOptionsBuilder<T>();
                string SlaveConnectionString = Config.SlaveConnectionStrings[i].ConnectionString;
                if (Config.DatabaseType.Equals(DatabaseType.SqlServer))
                    SlaveOptBuilder.UseLazyLoadingProxies().UseSqlServer(SlaveConnectionString);
                else if (Config.DatabaseType.Equals(DatabaseType.Mysql))
                    SlaveOptBuilder.UseLazyLoadingProxies().UseMySQL(SlaveConnectionString);
                services.RegisterType<T>().AsSelf().InstancePerLifetimeScope().WithParameter("options", SlaveOptBuilder.Options).Named<T>("SalveContext");

                //for (int i = 0; i < slaveCount; i++)
                //{
                //    var SlaveOptBuilder = new DbContextOptionsBuilder<T>();
                //    string SlaveConnectionString = Config.SlaveConnectionStrings[i].ConnectionString;
                //    if (Config.DatabaseType.Equals(DatabaseType.SqlServer))
                //        SlaveOptBuilder.UseLazyLoadingProxies().UseSqlServer(SlaveConnectionString);
                //    else if (Config.DatabaseType.Equals(DatabaseType.Mysql))
                //        SlaveOptBuilder.UseLazyLoadingProxies().UseMySQL(SlaveConnectionString);
                //    services.RegisterType<T>().AsSelf().InstancePerLifetimeScope().WithParameter("options", SlaveOptBuilder.Options).Named<DbContext>($"SlaveContext{i + 1}");
                //}
            }

            //注册读写分离模块
            //services.RegisterType<DbContextService<T>>()
            //    .WithParameter((p, c) => p.Name == "MasterContext", (p, c) => c.ResolveNamed<T>("MasterContext"))
            //    .WithParameter((p, c) => p.Name == "SalveContext", (p, c) => c.ResolveNamed<T>("SalveContext"))
            //    .AsImplementedInterfaces().InstancePerLifetimeScope();

            services.Register((c,p)=>new DbContextService<T>(c.ResolveNamed<T>("MasterContext"), c.ResolveNamed<T>("SalveContext")))
                //.WithParameter((p, c) => p.Name == "MasterContext", (p, c) => c.ResolveNamed<T>("MasterContext"))
                //.WithParameter((p, c) => p.Name == "SalveContext", (p, c) => c.ResolveNamed<T>("SalveContext"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
        }

        ///// <summary>
        ///// IOC注册
        ///// </summary>   
        //public static void AddDbContextService<T>(this ContainerBuilder services, DatabaseConfiguration databaseConfiguration) where T : DbContext
        //{
        //    services.Register(q => new DbContextService<T>(databaseConfiguration)).AsImplementedInterfaces().InstancePerLifetimeScope();
        //}
    }
}
