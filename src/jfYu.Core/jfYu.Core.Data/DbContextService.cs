using jfYu.Core.Common.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace jfYu.Core.Data
{

    public class DbContextService<T> : IDbContextService<T> where T : DbContext
    {

        /// <summary>
        /// 主数据库
        /// </summary>
        public T Master { get; }

        /// <summary>
        /// 丛数据库
        /// </summary>
        public T Slave { get; }

        public List<T> Slaves { get; }

        public DbContextService(T MasterContext, T SalveContext)
        {
            this.Master = MasterContext;

            this.Slave = SalveContext;
        }
    }
}
