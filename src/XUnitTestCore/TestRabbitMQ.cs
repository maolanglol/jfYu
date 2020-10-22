using Autofac;
using jfYu.Core.Common.Configurations;
using jfYu.Core.Common.Utilities;
using jfYu.Core.RabbitMQ;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Text;
using Xunit;

namespace xUnitTestCore
{
    class testclass
    {
        public int id;
        public string name;
    }
    public class TestRabbitMQ
    {
        [Fact]
        public void RabbitMQ()
        {
            var builder = new ConfigurationBuilder()
             .AddConfigurationFile("appsettings.json", optional: true, reloadOnChange: true);
            var Configuration = builder.Build();

            var mq = new RabbitMQService(AppConfig.GetSection("RabbitMQConnectionString")?.Value);
            var con = new ContainerBuilder();
            con.AddRabbitMQService();
            var icon = con.Build();

            var mq1 = icon.Resolve<IRabbitMQService>();
            //����
            var originObject = new testclass()
            {
                id = 1,
                name = "����1"

            };

            mq.Send("changeNametest3", "direct", originObject);


            //����
            var mq2 = icon.Resolve<IRabbitMQService>();
            mq2.Receive("aaa", "changeNametest3", "direct", q =>
            {
                Assert.Equal(q, Serializer.Serialize(originObject));
            });




        }

    }

}

