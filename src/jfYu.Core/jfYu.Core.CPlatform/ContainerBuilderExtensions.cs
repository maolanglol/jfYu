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
            //获取所有控制器类型并使用属性注入
            var dataAccess = Assembly.GetEntryAssembly();

            //获取所有Service并使用属性注入       
            services.RegisterAssemblyTypes(dataAccess.GetReferencedAssemblies().Select(q => Assembly.Load(q)).ToArray())
                .Where(t => t.GetInterfaces().Contains(typeof(IServiceKey)))
                .AsImplementedInterfaces()
                .PropertiesAutowired();

            services.RegisterAssemblyTypes(typeof(IServiceKey).Assembly).PropertiesAutowired();

            services.RegisterAssemblyTypes(dataAccess.GetReferencedAssemblies().Select(q => Assembly.Load(q)).ToArray())
                .Where(t => t.GetInterfaces().Contains(typeof(IBaseController)))
                .PropertiesAutowired();
        }
    }
}
