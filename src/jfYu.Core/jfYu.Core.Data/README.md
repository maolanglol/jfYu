

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
�����Լ���DbContext

```
    public class User : BaseEntity
    {
        /// <summary>
        /// ��¼��
        /// </summary>
        [DisplayName("��¼��"), Required, MaxLength(100)]
        public string UserName { get; set; }

        /// <summary>
        /// �ǳ�
        /// </summary>
        [DisplayName("�ǳ�"), Required, MaxLength(100)]
        public string NickName { get; set; }       

        /// <summary>
        /// �������ű��
        /// </summary>
        [DisplayName("��������")]
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public virtual Department Department { get; set; }

    }

    public class Department : BaseEntity
    {        
        /// <summary>
        /// ����
        /// </summary>
        [DisplayName("����"), Required]
        public string Name { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        [DisplayName("���"), Required]
        public string SubName { get; set; }

        /// <summary>
        /// �ϼ����ű��
        /// </summary>
        [DisplayName("�ϼ�����")]
        public int? SuperiorId { get; set; }

        /// <summary>
        /// �ϼ�����
        /// </summary>
        [DisplayName("�ϼ�����")]
        public virtual Department Superior { get; set; }

        /// <summary>
        /// ������Ա
        /// </summary>
        [DisplayName("������Ա")]
        public virtual List<User> Users { get; set; }
      
    }

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }

    }

  //���ʱ����
    class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("EFCORETOOLSDB");
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("The connection string was not set in the 'EFCORETOOLSDB' environment variable.");
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new DataContext(optionsBuilder.Options);
        }
    }
```
Ǩ������,�ڿ���̨����cmd��������ִ��
```
//����Ǩ�����ݿ������ַ���
$env:EFCORETOOLSDB="Data Source = xxx; database = test; User Id = sa; Password = xxx;";
//�½�Ǩ��
dotnet ef migrations add init
//Ӧ��Ǩ��(Ҳ���ڴ����н���Ǩ��)
dotnet ef migrations database update

```


DataContext Ϊ�Լ�������DbContext����

```
var containerBuilder = new ContainerBuilder();
var builder = new ConfigurationBuilder().AddConfigurationFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Build();
containerBuilder.AddDbContextService<DataContext>();
var container = containerBuilder.Build();
var db=container.Resolve<IDbContextService<DataContext>>();
db.Master.Database.Migrate(); //����Ӧ��Ǩ��

```
ʹ��

```
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
