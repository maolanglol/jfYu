using jfYu.Core.jfYuRequest;
using System.IO;
using Xunit;

namespace xUnitTestCore.Request
{
    public class TestjfYuRequestCore
    {

        [Fact]

        public void TestHtml()
        {
            jfYuRequest jfYu = new jfYuRequest("https://b2b.10086.cn/b2b/main/listVendorNotice.html?noticeType=2")
            {
                Method = jfYuRequestMethod.Get
            };
            jfYu.RequestHeader.Host = "https://b2b.10086.cn";
            var x = jfYu.GetHtml();
            var y = jfYu.GetHtmlAsync().Result;
            Assert.True(x.Length > 100);
            Assert.Equal(x.Substring(x.Length - 100, 99), y.Substring(y.Length - 100, 99));


            var jfYu1 = new jfYuHttpClient("https://b2b.10086.cn/b2b/main/listVendorNotice.html?noticeType=2")
            {
                Method = jfYuRequestMethod.Get
            };
            jfYu.RequestHeader.Host = "https://b2b.10086.cn";
            var x1 = jfYu.GetHtml();
            var y1 = jfYu1.GetHtmlAsync().Result;
            Assert.True(x.Length > 100);
            Assert.Equal(x.Substring(x.Length - 100, 99), y.Substring(y.Length - 100, 99));
        }
        [Fact]
        public void TestFile()
        {
            jfYuRequest jfYu = new jfYuRequest("https://img.nga.178.com/attachments/mon_201904/11/-7da9Q5-dgq4ZgT3cSzk-qo.jpg");

            jfYu.GetFile("d:/2.jpg");
            jfYu.GetFile("d:/3.jpg");
            Assert.True(File.Exists("d:/2.jpg"));
            Assert.True(File.Exists("d:/3.jpg"));
            Assert.Equal(File.ReadAllText("d:/2.jpg"), File.ReadAllText("d:/3.jpg"));
            var jfYu1 = new jfYuHttpClient("https://img.nga.178.com/attachments/mon_201904/11/-7da9Q5-dgq4ZgT3cSzk-qo.jpg");
            jfYu1.GetFile("d:/4.jpg");
            jfYu1.GetFile("d:/5.jpg");
            Assert.True(File.Exists("d:/4.jpg"));
            Assert.True(File.Exists("d:/5.jpg"));
            Assert.Equal(File.ReadAllText("d:/4.jpg"), File.ReadAllText("d:/5.jpg"));
            File.Delete("d:/2.jpg");
            File.Delete("d:/3.jpg");
            File.Delete("d:/4.jpg");
            File.Delete("d:/5.jpg");
        }


    }
}
