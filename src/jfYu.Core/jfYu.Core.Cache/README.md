

### ����

```
Install-Package jfYu.Core.Cache
```

֧���ڴ桢redis����,�������Ϊredis,��������redis������

```
{
  "Cache": {
    "Type": "Redis", //��������Memory,Redis
    "KeySuffix": "" //����keyǰ׺
  },
   "Redis": {
    "EndPoints": [
      {
        "Host": "jfwang.tpddns.cn", //��ַ
        "Port": "31002" //�˿�

      },
      {
        "Host": "jfwang.tpddns.cn", //��ַ
        "Port": "31003" //�˿�     
      }
    ],
    "Password": "Ncscd1111", //����
    "KeySuffix": "", //redis keyǰ׺��������񲻻���ô�ǰ׺��
    "Timeout": "5000", //��ʱʱ�䣨���룩 Ĭ��5��
    "DbIndex": "15" //���ݿ� Ĭ��Ϊ0
  }
}
```
ͨ��IOC���������ļ�ֱ�ӷ��ض�Ӧ�Ļ����������

```
var ContainerBuilder = new ContainerBuilder();
var builder = new ConfigurationBuilder().AddConfigurationFile("appsettings.json", optional: true, reloadOnChange: true); //ע�������ļ�
var Configuration = builder.Build();
ContainerBuilder.AddCache(); //iocע��
ContainerBuilder.AddCacheAsProperties(); //����ע��
var CacheService = icon.Resolve<ICache>(); //����
//��ӻ���
CacheService.Add("testkey1", "testvalue1");
//��ӻ�����Ϲ���ʱ��
CacheService.Add("testkey1", "testvalue1", 3)
CacheService.Add("testkey1", "testvalue1", TimeSpan.FromSeconds(3))
//�ж��Ƿ���ڻ���
CacheService.Has("testkey1")
//��ȡ����
CacheService.GetString("testkey2")
CacheService.GetInt("testkey6")
CacheService.Get<TestModel>("testkey8")
CacheService.Get("testkey1").ToString()
//������ȡ����
string[] list = { "testkey1", "testkey2" };
var listvalue = CacheService.GetRange(list);
//�޸Ļ���
CacheService.Replace("testkey1", "testvalue11")
CacheService.Replace("testkey2", "testvalue22", 3)
CacheService.Replace("testkey2", "testvalue22",TimeSpan.FromSeconds(3))
//ɾ������
CacheService.Remove("testkey6")
//����ɾ������
string[] list = { "testkey1", "testkey2" };
var rlc = CacheService.RemoveRange(list);//���ز���ʧ�ܵ�key

```
