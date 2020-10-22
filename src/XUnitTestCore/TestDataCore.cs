using Autofac;
using jfYu.Core.Common.Configurations;
using jfYu.Core.CPlatform;
using jfYu.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NPOI.HSSF.Record.Chart;
using System.Linq;
using Xunit;

namespace xUnitTestCore
{

    /// <summary>
    /// �����������migrations add��ʱ��ͨ���÷���ʵ����datacontext
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            var builder = new ConfigurationBuilder().AddConfigurationFile("SqlServer.json", optional: true, reloadOnChange: true);
            builder.Build();
            containerBuilder.AddDbContextService<DataContext>(q => { return new DataContext(q); });
            var container = containerBuilder.Build();
            return container.Resolve<DbContextService<DataContext>>().Master;
        }
    }
    public class User
    {
        public int Id { get; set; }
        public int Name { get; set; }
    }
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }


        public DbSet<User> Users { get; set; }

        public DbSet<Company> Companys { get; set; }

    }
    public class TestDataCore
    {
        [Fact]
        public void TestData()
        {
            var containerBuilder = new ContainerBuilder();
            var builder = new ConfigurationBuilder().AddConfigurationFile("SqlServer.json", optional: true, reloadOnChange: true);
            builder.Build();
            containerBuilder.AddDbContextService<DataContext>(q => { return new DataContext(q); });
            var container = containerBuilder.Build();
            var db = container.Resolve<DbContextService<DataContext>>();

            ////master
            if (db.Master.Database.GetPendingMigrations().Any())
                db.Master.Database.Migrate();

            //slave
            if (db.Slave.Database.GetPendingMigrations().Any())
                db.Slave.Database.Migrate();

            //д

            db.Master.Users.Add(new User() { Id = 1, Name = 2 });
            db.Master.SaveChanges();
            db.Slave.Users.Add(new User() { Id = 11, Name = 22 });
            db.Slave.Users.Add(new User() { Id = 12, Name = 33 });
            db.Slave.SaveChanges();
            Assert.Equal(3, db.Master.Users.Count());
            Assert.Equal(3, db.Slave.Users.Count());

            db.Master.Companys.Add(new Company() { Id = 1, Name = "master" });
            db.Master.SaveChanges();
            db.Slave.Companys.Add(new Company() { Id = 11, Name = "slavetstdwad" });
            db.Slave.Companys.Add(new Company() { Id = 12, Name = "slavetst����" });
            db.Slave.SaveChanges();
            Assert.Equal(3, db.Master.Companys.Count());
            Assert.Equal(3, db.Slave.Companys.Count());
        }
    }
}
