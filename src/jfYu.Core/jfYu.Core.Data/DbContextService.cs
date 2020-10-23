using jfYu.Core.Common.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace jfYu.Core.Data
{

    public class DbContextService<T> : IDbContextService<T> where T : DbContext
    {

        /// <summary>
        /// 主从数据库配置
        /// </summary>
        public readonly DatabaseConfiguration Config = new DatabaseConfiguration();

        /// <summary>
        /// 主数据库
        /// </summary>
        public T Master { get; }

        /// <summary>
        /// 丛数据库
        /// </summary>
        public T Slave { get; }

        /// <summary>
        /// 所有从数据库
        /// </summary>
        public List<T> Slaves { get; }

        public DbContextService()
        {
            try
            {
                Config = AppConfig.GetSection("ConnectionStrings").GetBindData<DatabaseConfiguration>();

            }
            catch (Exception ex)
            {
                throw new Exception($"连接字符串配置错误:{ex.Message} - {ex.StackTrace}");
            }
            try
            {
                //主数据库
                Master = GetMaster();
                //从数据库
                Slave = GetSlave();

                Slaves = GetSlaves();
            }
            catch (Exception ex)
            {
                throw new Exception($"配置错误无法实例化主从连接:{ex.Message} - {ex.StackTrace}");
            }

        }

        public DbContextService(DatabaseConfiguration _config)
        {
            Config = _config;
            try
            {
                //主数据库
                this.Master = GetMaster();
                //从数据库
                this.Slave = GetSlave();

                this.Slaves = GetSlaves();
            }
            catch (Exception ex)
            {
                throw new Exception($"配置错误无法实例化主从连接:{ex.Message} - {ex.StackTrace}");
            }
        }


        /// <summary>
        /// 设置主数据库
        /// </summary>
        /// <param name="_DbContext">要获取的dbcontext</param>
        /// <returns></returns>
        private T GetMaster()
        {
            var optionsBuilder = new DbContextOptionsBuilder<T>();
            if (Config.DatabaseType.Equals(DatabaseType.SqlServer))
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(Config.MasterConnectionString);
            else if (Config.DatabaseType.Equals(DatabaseType.Mysql))
                optionsBuilder.UseLazyLoadingProxies().UseMySQL(Config.MasterConnectionString);
            return (T)Activator.CreateInstance(typeof(T), optionsBuilder.Options);
        }

        /// <summary>
        /// 设置从数据库
        /// </summary>
        /// <param name="_DbContext">要获取的dbcontext</param>
        /// <returns></returns>
        private T GetSlave()
        {
            var optionsBuilder = new DbContextOptionsBuilder<T>();
            Random r = new Random();
            int slaveCount = Config.SlaveConnectionStrings.Count;
            if (slaveCount < 1) //无从数据库 返回主数据库
            {
                return GetMaster();
            }
            else
            {
                int i = r.Next(0, slaveCount);//随机一个从数据库
                string SlaveConnectionString = Config.SlaveConnectionStrings[i].ConnectionString;
                if (Config.DatabaseType.Equals(DatabaseType.SqlServer))
                    optionsBuilder.UseLazyLoadingProxies().UseSqlServer(SlaveConnectionString);
                else if (Config.DatabaseType.Equals(DatabaseType.Mysql))
                    optionsBuilder.UseLazyLoadingProxies().UseMySQL(SlaveConnectionString);
            }
            return (T)Activator.CreateInstance(typeof(T), optionsBuilder.Options);
        }

        /// <summary>
        /// 设置所有从数据库
        /// </summary>
        /// <param name="_DbContext">要获取的dbcontext</param>
        /// <returns></returns>
        private List<T> GetSlaves()
        {
            var salves = new List<T>();
            var optionsBuilder = new DbContextOptionsBuilder<T>();

            int slaveCount = Config.SlaveConnectionStrings.Count;
            if (slaveCount < 1) //无从数据库 返回主数据库
            {
                salves.Add(GetMaster());
            }
            else
            {
                for (int i = 0; i < slaveCount; i++)
                {
                    string SlaveConnectionString = Config.SlaveConnectionStrings[i].ConnectionString;
                    if (Config.DatabaseType.Equals(DatabaseType.SqlServer))
                        optionsBuilder.UseLazyLoadingProxies().UseSqlServer(SlaveConnectionString);
                    else if (Config.DatabaseType.Equals(DatabaseType.Mysql))
                        optionsBuilder.UseLazyLoadingProxies().UseMySQL(SlaveConnectionString);
                }

                salves.Add((T)Activator.CreateInstance(typeof(T), optionsBuilder.Options));
            }
            return salves;
        }
    }
}
