
### ��ȡ�����Լ�һЩ��������

##### ��ȡ�����ļ�
```
Install-Package jfYu.Core.Common
```

��ƽ̨�������ݻ�ȡ���ɰ󶨵��࣬Ҳ����ֱ�ӻ�ȡֵ��

```
//����json�����ļ�

var builder = new ConfigurationBuilder()
.AddConfigurationFile($"appsettings.{ env.EnvironmentName}.json", optional: true, reloadOnChange: true)
.AddConfigurationFile($"appsettings.json", optional: true, reloadOnChange: true);
var Configuration = builder.Build();

//�󶨵���
EmailConfiguration Config = new EmailConfiguration();
Config=AppConfig.GetSection("Email").GetBindData<EmailConfiguration>();

//ֱ�ӻ�ȡֵ
var address=AppConfig.GetSection("Email").GetSection("Address").Value;

```

##### �ӽ���
 using jfYu.Core.Common.Utilities


```
//Aes�ӽ���
"test".AesEncrypt().AesEncrypt();
//Des�ӽ���
"test".DesEncrypt().DesEncrypt();
//Ras�ӽ���
Rsa.Encrypt("test", "d://pems/RSA.Pub")
Rsa.Decrypt("testEncryptString", "d://pems/RSA.Pub");

//SHA
//MD5
"test".SHAmd5Encrypt();
//SHA1
"test".SHA1Encrypt();
//SHA256
"test".SHA256Encrypt();
//SHA384
"test".SHA384Encrypt();
//SHA512
"test".SHA384Encrypt();

```

##### DataTableתList

```
var dt=new DataTable();
var list=dt.ToModels<T>();

var dr=dt.Rows[0];
T model=dt.ToModel<T>();

```

##### ���л�

```
var res=Serializer.Deserialize<T>(json)
var res=Serializer.Serialize(object)

```

##### unixʱ���

```
UnixTime.GetUnixTime();//��ȡ��ǰʱ���

UnixTime.GetUnixTime(DateTime.Now);//��ȡָ��ʱ���

UnixTime.GetDateTime(unixtime);//ʱ���תDateTime

```

##### д���ļ���־

```
//���ֶ�IOCע�����ֱ��new
//log�ļ������Ŀ¼Logsÿ��һ��
 WriteFileLog log=new WriteFileLog();
 log.WriteLog("xxxx");//д��log
 log.WriteLog("xxxx","logxxx");//д��log����Ӧ���Ƶ�log�ļ���

```

##### NLog��־

׼��NLog�����ļ�
```
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
      xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
      autoReload = "true"
      internalLogFile="logs/SysNlog.log"
  >
  <targets>
    <target xsi:type="File" name="ErrorLog" fileName="logs\${shortdate}-error.log"
            layout="${longdate}|${uppercase:${level}}|${message}|${event-context:item=type}|${event-context:item=message}|${event-context:item=ip}|${event-context:item=url}| ${exception:format=tostring}" />
    <target xsi:type="File" name="TraceLog" fileName="logs\${shortdate}-trace.log"
          layout="${longdate}|${uppercase:${level}}|${message}|${event-context:item=type}|${event-context:item=message}|${event-context:item=ip}|${event-context:item=url}| ${exception:format=tostring}" />
    <target name="Console" xsi:type="Console" layout="${longdate}|${message}"/>
    <target name="asyncdb" xsi:type="AsyncWrapper" queueLimit="5000" overflowAction="Discard">
      <target xsi:type="Database"  name="db" connectionString="${var:NLOG_CONNECTION_STRING}">
        <commandText>
          INSERT INTO [dbo].[Logs]([State],[CreateTime],[UpdateTime],[Type],[Message],[Content],[UserId],[TrueName],[IP],[Url])
          VALUES(1,@CreateDate,@CreateDate,@Type,@Message,@Content,@UserId,@TrueName,@IP,@Url);
        </commandText>
        <parameter name="@CreateDate" layout="${longdate}"/>
        <parameter name="@Type" layout="${event-context:item=type}"/>
        <parameter name="@Message" layout="${message}"/>
        <parameter name="@Content" layout="${event-context:item=content}"/>
        <parameter name="@UserId" layout="${event-context:item=userid}"/>
        <parameter name="@TrueName" layout="${event-context:item=truename}"/>
        <parameter name="@IP" layout="${event-context:item=ip}"/>
        <parameter name="@Url" layout="${event-context:item=url}"/>

      </target>
    </target>
  </targets>
  <rules>
    <logger name="*" level="Debug" writeTo="Console" />
    <logger name="*" level="Trace" writeTo="TraceLog" />
    <logger name="*" level="Info" writeTo="db" />
    <logger name="*" minlevel="Warn" writeTo="ErrorLog,db" />

  </rules>
</nlog>

```
IOCע��

```
  var config = new ConfigurationBuilder().AddConfigurationFile("SqlServer.json", true, true);
  containerBuilder.AddNLog("nLog.config");
  containerBuilder1.AddNLog("nLog.config", AppConfig.GetSection("ConnectionStrings:MasterConnectionString").Value);\\���������ݿ����־

```

ʹ��

```
   var logger = _container.Resolve<ILogger>();

   //��ͨд��
   logger.Trace("x2");
   logger.Log(new LogEventInfo(logLevel, logType, message));

   //��չ����д��
   logger.ALog(LogLevel.Info, "����", "�쳣");

```

##### AutoMapper


```
//ע��
var containerBuilder = new ContainerBuilder();
containerBuilder.AddAutoMapper(cfg =>
{
    cfg.CreateMap<User, UserProfileViewModel>().ForMember(q => q.AllName, opt => opt.MapFrom(q => q.NickName + q.UserName));
});

var c = containerBuilder.Build();
var am = c.Resolve<IMapper>();

//ʹ��
  var u = new User() { UserName = "x", NickName = "y" };
  var vu = am.Map<UserProfileViewModel>(u);
```