

### word����

```
Install-Package jfYu.Core.Word
```

����������word�ļ���Ҳ���Ը���ģ������word�ļ����ɼ���ͼƬ�ļ���ģ����Ϊ${name}
```
var ContainerBuilder = new ContainerBuilder();
ContainerBuilder.AddJfYuWord();
var c = ContainerBuilder.Build();
var ms = c.Resolve<jfYuWord>();   
var x = new System.Collections.Generic.Dictionary<string, object>
{
    { "x", "����Ŷ" }
};

FileStream fs = new FileStream("d:/1.jpg", FileMode.Open);
x.Add("y", fs);
ms.GenerateWordByTemplate("d:/1.docx", x, "d:/2.docx");

```

