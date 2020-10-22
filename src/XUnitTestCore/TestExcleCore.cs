using Autofac;
using jfYu.Core.Excel;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Xunit;

namespace xUnitTestCore.Excel
{
    public class TestExcleCore
    {


        [Fact]
        public void TestExcle()
        {
            var cb = new ContainerBuilder();
            cb.AddJfYuExcel();    
            var excel = cb.Build().Resolve<JfYuExcel>();
            Assert.NotNull(excel);
            var dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("name");
            dt.Columns.Add("sex");
            dt.Columns.Add("age");
            dt.Rows.Add("1", "��", "��", "12");
            dt.Rows.Add("2", "wang", "��", "1200000");
            dt.Rows.Add("3", "����ݵ�", "Ů", "12");
            dt.Rows.Add("4", "dwadwadwad", "1", "12");
            dt.Rows.Add("5", "13213213", "0", "1465452");
            var dir = new Dictionary<string, string>
            {
                { "id", "���" },
                { "name", "����" },
                { "sex", "�Ա�" },
                { "age", "����" }
            };
            excel.ToExcel(dt, "d:/1.xlsx");

            Assert.True(File.Exists("d:/1.xlsx"));
            var dt1 = excel.ToDataTable("d:/1.xlsx");
            Assert.True(dt1.Rows.Count == 5);

           File.Delete("d:/1.xlsx");
        }
    }
}
