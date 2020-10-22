using Autofac;
using jfYu.Core.Data;
using System.Linq;
using System.Reflection;

namespace jfYu.Core.CPlatform
{
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// IOC注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddCPlatform(this ContainerBuilder services)
        {
            //获取所有Service并使用属性注入
            var baseServiceType = typeof(IServiceKey);

            services.RegisterAssemblyTypes(baseServiceType.Assembly)
                .Where(t => baseServiceType.IsAssignableFrom(t) && t != baseServiceType)
                .AsImplementedInterfaces()
                .PropertiesAutowired();

            services.RegisterAssemblyTypes(baseServiceType.Assembly).PropertiesAutowired();

            //获取所有控制器类型并使用属性注入
            var dataAccess = Assembly.GetExecutingAssembly();

            var controllerBaseType = typeof(IBaseController);
            services.RegisterAssemblyTypes(dataAccess)
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired();
        }
    }
}
