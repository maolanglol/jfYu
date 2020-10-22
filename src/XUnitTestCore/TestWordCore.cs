using jfYu.Core.Word;
using System.IO;
using Xunit;
using Autofac;

namespace xUnitTestCore
{
    public class TestWordCore
    {

        [Fact]
        public void CreateWord()
        {
            var ContainerBuilder = new ContainerBuilder();
            ContainerBuilder.AddJfYuWord();
            var c = ContainerBuilder.Build();
            var ms = c.Resolve<jfYuWord>();          
            var x = new System.Collections.Generic.Dictionary<string, object>
            {
                { "x", "²âÊÔÅ¶" }
            };
            FileStream fs = new FileStream("d:/1.jpg", FileMode.Open);
            x.Add("y", fs);
            ms.GenerateWordByTemplate("d:/1.docx", x, "d:/2.docx");
            Assert.True(File.Exists("d:/2.docx"));
            var fst = File.Open("d:/2.docx", FileMode.Open);
            Assert.True(fst.Length > 0);
        }

    }
}
