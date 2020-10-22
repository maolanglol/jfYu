

###  代码轻量级读写分离功能

支持MySql和SqlServer,如无从数据库可不配置，自动使用主数据库

```
Install-Package jfYu.Core.Data
```

配置文件

```
 "ConnectionStrings": {
    "DatabaseType": "SqlServer", //MySql  
    "MasterConnectionString": "Data Source = 127.0.0.1,9004; database = dbtest; User Id = sa; Password = 123456;",
    "SlaveConnectionStrings": [
      {
        "ConnectionString": "Data Source = 127.0.0.1,9005; database = dbtest; User Id = sa; Password = 123456;",
        "Weight": 2
      },
      {
        "ConnectionString": "Data Source = 127.0.0.1,9006; database = dbtest; User Id = sa; Password = 123456;",
        "Weight": 8
      }
    ]

    //"DatabaseType": "MySql", 
    //"MasterConnectionString": "server=127.0.0.1;userid=root;pwd=123456;port=9001;database=dbtest;",
    //"SlaveConnectionStrings": [
    //  {
    //    "ConnectionString": "server=127.0.0.1;userid=root;pwd=123456;port=9002;database=dbtest;",
    //    "Weight": 2
    //  },
    //  {
    //    "ConnectionString": "server=127.0.0.1;userid=root;pwd=123456;port=9003;database=dbtest;",
    //    "Weight": 8
    //  }
    //]
  }
```
创建自己的DbContext然后通过Autofac进行依赖注入

DataContext 为自己创建的DbContext对象

```
var containerBuilder = new ContainerBuilder();
var builder = new ConfigurationBuilder().AddConfigurationFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Build();
containerBuilder.AddDbContextService<DataContext>(q => { return new DataContext(q); });

```
使用

```
  var db = container.Resolve<DbContextService<DataContext>>();
////master
if (db.Master.Database.GetPendingMigrations().Any())
    db.Master.Database.Migrate();

//slave
if (db.Slave.Database.GetPendingMigrations().Any())
    db.Slave.Database.Migrate();

//写
db.Master.Users.Add(new User() { Id = 1, Name = 2 });
db.Master.SaveChanges();
//读
db.Slave.Users.ToList();
db.Slave.Users.Count();
```
