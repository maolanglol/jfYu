
### ��֤��

```
Install-Package jfYu.Core.Captcha
```
IOC����ע���ȡ����

```
var containerBuilder = new ContainerBuilder();
var builder = new ConfigurationBuilder().AddConfigurationFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Build();
containerBuilder.AddCaptcha(); //ʹ��Ĭ������
CaptchaConfig captchaConfig = new CaptchaConfig();
captchaConfig.Length = 6;
captchaConfig.Width = 360;
captchaConfig.Height = 120;
containerBuilder.AddCaptcha(new CaptchaConfig()); //�Զ�������
```


ʹ��

```
//ͨ��IOC����ֱ��new������
 var Captcha = New Captcha();
 var result=Captcha.GetCaptcha();
//����ͼƬ������ ���� ���ڴ������ʾ
Stream s = new MemoryStream(result.CaptchaByteData);
byte[] srcBuf = new Byte[s.Length];
s.Read(srcBuf, 0, srcBuf.Length);
s.Seek(0, SeekOrigin.Begin);
FileStream fs = new FileStream($"d:/{captchaCode}.png", FileMode.Create, FileAccess.Write);
fs.Write(srcBuf, 0, srcBuf.Length);
fs.Close();
```
