
using Microsoft.Extensions.Configuration;

namespace jfYu.Core.Common.Configurations
{
    public static class AppConfig
    {
        internal static IConfigurationRoot Configuration { get; set; }
        /// <summary>
        /// 获取配置节点
        /// </summary>
        /// <param name="name">节点key</param>
        /// <returns>节点</returns>
        public static IConfigurationSection GetSection(string name)
        {
            return Configuration?.GetSection(name);
        }

        public static T GetBindData<T>(this IConfigurationSection configurationSection) where T : class
        {
            return configurationSection.Get<T>();
            //configurationSection.Bind(instance);
            //return (T)instance;
        }
    }
}
