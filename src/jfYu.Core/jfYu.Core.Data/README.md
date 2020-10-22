

###  ������������д���빦��

֧��MySql��SqlServer,���޴����ݿ�ɲ����ã��Զ�ʹ�������ݿ�

```
Install-Package jfYu.Core.Data
```

�����ļ�

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
�����Լ���DbContextȻ��ͨ��Autofac��������ע��

DataContext Ϊ�Լ�������DbContext����

```
var containerBuilder = new ContainerBuilder();
var builder = new ConfigurationBuilder().AddConfigurationFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Build();
containerBuilder.AddDbContextService<DataContext>(q => { return new DataContext(q); });

```
ʹ��

```
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
//��
db.Slave.Users.ToList();
db.Slave.Users.Count();
```
