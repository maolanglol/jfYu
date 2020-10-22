
### mongodb


```
Install-Package jfYu.Core.MongoDB
```
�����ļ�

```
 "MongoDB": {
    "MongoUrl": "mongodb+srv://jfwang:xxx@xxx.net/admin",
    "DbName": "nba"
  }

```

```
//IOCע��
var builder = new ConfigurationBuilder()
    .AddConfigurationFile("MongoDB.json", optional: true, reloadOnChange: true);
var cb = new ContainerBuilder();
cb.AddMongoDB();
var con = cb.Build();
var mongoDBService = con.Resolve<MongoDBService>();
Data d1 = new Data() { name = "����1", age = "18", sex = "��" };
Data d2 = new Data() { name = "����2", age = "19", sex = "��" };
Data d3 = new Data() { name = "����3", age = "20", sex = "Ů" };
//���
d1 = mongoDBService.Insert(d1);
//�������
mongoDBService.InsertBatch(new List<Data>() { d3, d4 });
//ɾ��
mongoDBService.Delete<Data>(d11.Id.ToString());
mongoDBService.SoftDelete<Data>(d11.Id.ToString());
//�޸�
 mongoDBService.Modify<Data>(d11.Id.ToString(), "name", "����1�ĺ�");
 mongoDBService.Update(d11);
//��ȡ����
var d111=ongoDBService.QueryOne<Data>(q => q.Id == d11.Id);
//�б�
var list=mongoDBService.QueryCollection<Data>(q => q.sex == "Ů")
//��ҳ
 var lc = mongoDBService.QueryCollection<Data>().ToPaging(new QueryModel() { PageIndex = 1, PageSize = 2 });

  ```
