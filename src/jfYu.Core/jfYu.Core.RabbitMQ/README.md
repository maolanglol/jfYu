
### RabbitMQ

```
Install-Package jfYu.Core.RabbitMQ
```
��װ��RabbitMQ���ߣ����У����ݳ־û���֧�ַ��ͺͽ���������֧�ֳ��õ�5��ģʽ

����rabbit����
```
 "RabbitMQ": {
    "HostName": "127.0.0.1",
    "UserName": "jfwang",
    "Password": "123456",
    "VirtualHost": "/",
    "HeartBeat": "60",
    "Port": "9808"
  }
```

iocע��
```
var builder = new ConfigurationBuilder()
        .AddConfigurationFile("appsettings.json", optional: true, reloadOnChange: true);
var Configuration = builder.Build();
var con = new ContainerBuilder();
con.AddRabbitMQService();
```

����
```
var icon = con.Build();
SendEventMessage(icon.Resolve<IRabbitMQService>());
private static void SendEventMessage(IRabbitMQService rabbitMQServer)
        {
            for (var i = 1; i < 100; i++)
            {
                var originObject = new testclass()
                {
                    id = i,
                    name = "����" + i

                };

                rabbitMQServer.Send("changeNametest3","direct", originObject);

                Console.WriteLine($"����{i}");
                Thread.Sleep(2000);
            }
        }
```

����
```
var mq = icon.Resolve<IRabbitMQService>();
mq.Receive("queName", "changeNametest3", "direct", q =>
{
   Thread.Sleep(4000);
   Console.WriteLine(q);
});
```
