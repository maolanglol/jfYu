
### ��ҳ���ݻ�ȡ

��װ

```
Install-Package jfYu.Core.jfYuRequest 

```

Ϊ����󻯵����ܣ�.net frameworkӦ��ʹ��httpwebrequest(��ӦjfYuRequest) .net coreʹ��HttpClient(��ӦjfYuHttpClient)��

```
var jfYu = new jfYuRequest("https://b2b.10086.cn/b2b/main/listVendorNotice.html?noticeType=2")
{
    Method = jfYuRequestMethod.Get
};
jfYu.RequestHeader.Host = "https://b2b.10086.cn";
var x = jfYu.GetHtml();
var y = await jfYu.GetHtmlAsync();

var jfYu1 = new jfYuHttpClient("https://b2b.10086.cn/b2b/main/listVendorNotice.html?noticeType=2")
{
    Method = jfYuRequestMethod.Get
};
jfYu.RequestHeader.Host = "https://b2b.10086.cn";
var x1 = jfYu.GetHtml();
var y1 = await jfYu.GetHtmlAsync();
//�����ļ�
jfYuRequest jfYu = new jfYuRequest("https://img.nga.178.com/attachments/mon_201904/11/-7da9Q5-dgq4ZgT3cSzk-qo.jpg");
jfYu.GetFile("d:/2.jpg",(q,w,e)=> { });
jfYuHttpClient jfYu1 = new jfYuHttpClient("https://img.nga.178.com/attachments/mon_201904/11/-7da9Q5-dgq4ZgT3cSzk-qo.jpg");
jfYu1.GetFile("d:/4.jpg");

```
