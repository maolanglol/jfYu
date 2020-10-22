using Autofac;
using jfYu.Core.Captcha;
using jfYu.Core.Common.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.IO;

namespace UnitTest4._7.Common
{


    [TestClass]
    public class TestCommon
    {
        [TestMethod]
        public void TestJson()
        {
            var builder = new ConfigurationBuilder()
            .AddConfigurationFile("CacheRedis.json", optional: true, reloadOnChange: true)
            .AddConfigurationFile("Captcha.json", optional: true, reloadOnChange: true);
            _ = builder.Build();
            Assert.AreEqual("2", AppConfig.GetSection("Captcha:Length").Value);
            Assert.AreEqual("Redis", AppConfig.GetSection("Cache:Type").Value);
        }

    }




}
