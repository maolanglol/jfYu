
### <a href="#Excel">Excel</a>

```
Install-Package jfYu.Core.Excel
```

```

 var cb = new ContainerBuilder();
cb.AddJfYuExcel();    
var excel = cb.Build().Resolve<JfYuExcel>();

var dt = new DataTable();
dt.Columns.Add("id");
dt.Columns.Add("name");
dt.Columns.Add("sex");
dt.Columns.Add("age");
dt.Rows.Add("1", "王", "男", "12");
dt.Rows.Add("2", "wang", "男", "1200000");
dt.Rows.Add("3", "大的洼地", "女", "12");
dt.Rows.Add("4", "dwadwadwad", "1", "12");
dt.Rows.Add("5", "13213213", "0", "1465452");
var dir = new Dictionary<string, string>
{
    { "id", "编号" },
    { "name", "名称" },
    { "sex", "性别" },
    { "age", "年龄" }
};
//导出excel
excel.ToExcel(dt, "d:/1.xlsx");

excel.ToExcel(sqlDataReader, fileName, q =>
    {
        Console.CursorLeft = 0;
        Console.Write(q);
    });

 excel.ToExcel<T>(sqlDataReader, fileName, q =>
    {
        Console.CursorLeft = 0;
        Console.Write(q);
    });
    excel.ToExcel<T>(sqlstring, fileName, q =>
    {
        Console.CursorLeft = 0;
        Console.Write(q);
    });

 //导出Csv
 excel.ToCsv(dt, "d:/1.xlsx");

//excel导入
var dt1 = excel.ToDataTable("d:/1.xlsx");


```