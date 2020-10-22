
###  Email����

```
Install-Package jfYu.Core.Email
```
����email server
```
{
 "Email": {
    "MailServer": "smtp.aliyun.com",
    "MailServerUsername": "xxx@aliyun.com",
    "MailServerPassword": "xxx",
    "SenderEmail": "xxx@aliyun.com",
    "SenderName": "xxx",
    "AdminSenderEmail": "xxx",
    "Port": 465
  },
}

var builder = new ConfigurationBuilder().AddConfigurationFile("Email.json", optional: true, reloadOnChange: true);
 builder.Build();
var cb = new ContainerBuilder();
cb.AddEmail();           
cb.AddEmail((new EmailConfiguration() { MailServer = "smtp.aliyun.com", MailServerPassword = "", MailServerUsername = "", Port = 465, SenderName = "", SenderEmail = "" )); //�Զ���
IEmail mail = cb.Build().Resolve<IEmail>();
await mail.SendMailAsync("475760135@qq.com", "x1", "xx1");
mail.SendMail("475760135@qq.com", "617110271@qq.com;wjf@sunbirddata.com.cn", "��TO��CC", "�����ʼ�");
```
