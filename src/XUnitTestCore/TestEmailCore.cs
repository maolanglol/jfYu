using Autofac;
using jfYu.Core.Common.Configurations;
using jfYu.Core.EMail;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace xUnitTestCore.Email
{
    public class TestEmailCore
    {
        [Fact]
        public void SendEmail1()
        {

            var builder = new ConfigurationBuilder()
             .AddConfigurationFile("Email.json", optional: true, reloadOnChange: true);
            builder.Build();
            var cb = new ContainerBuilder();
            cb.AddEmail();
            IEmail mail = cb.Build().Resolve<IEmail>();
            Assert.NotNull(mail);
            mail.SendMail("475760135@qq.com", "��TO��CC", "�����ʼ�");
            mail.SendMail("475760135@qq.com", "617110271@qq.com", "��TO��CC", "�����ʼ�");
            mail.SendMail("475760135@qq.com", "617110271@qq.com;wjf@sunbirddata.com.cn", "��TO��CC", "�����ʼ�");
            mail.SendMail("475760135@qq.com;617110271@qq.com", "��TO��CC", "�����ʼ�");
            mail.SendMail("475760135@qq.com;617110271@qq.com", "617110271@qq.com", "��TO��CC", "�����ʼ�");
            mail.SendMail("475760135@qq.com;617110271@qq.com", "617110271@qq.com;wjf@sunbirddata.com.cn", "��TO��CC", "�����ʼ�");


        }
        [Fact]
        public void SendEmail2()
        {
            var builder = new ConfigurationBuilder()
               .AddConfigurationFile("Email.json", optional: true, reloadOnChange: true);
            builder.Build();
            var cb = new ContainerBuilder();
            cb.AddEmail();
            IEmail mail = cb.Build().Resolve<IEmail>();
            Assert.NotNull(mail);
            mail.SendMailAsync("475760135@qq.com", "��TO��CC", "�����ʼ�").Wait();
            mail.SendMailAsync("475760135@qq.com", "617110271@qq.com", "��TO��CC", "�����ʼ�").Wait();
            mail.SendMailAsync("475760135@qq.com", "617110271@qq.com;wjf@sunbirddata.com.cn", "��TO��CC", "�����ʼ�").Wait();
            mail.SendMailAsync("475760135@qq.com;617110271@qq.com", "��TO��CC", "�����ʼ�").Wait();
            mail.SendMailAsync("475760135@qq.com;617110271@qq.com", "617110271@qq.com", "��TO��CC", "�����ʼ�").Wait();
            mail.SendMailAsync("475760135@qq.com;617110271@qq.com", "617110271@qq.com;wjf@sunbirddata.com.cn", "��TO��CC", "�����ʼ�").Wait();
        }


    }
}
