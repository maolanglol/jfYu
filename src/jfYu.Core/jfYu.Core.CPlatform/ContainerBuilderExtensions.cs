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
            //获取执行方法的方法Assembly
            var EntryAssembly = Assembly.GetEntryAssembly();
            var ServiceKeyType = typeof(IServiceKey);
            var BaseControllerType = typeof(IBaseController);

            services.RegisterAssemblyTypes(ServiceKeyType.Assembly).PropertiesAutowired();
            //获取所有Service并使用属性注入       
            services.RegisterAssemblyTypes(EntryAssembly.GetReferencedAssemblies().Select(q => Assembly.Load(q)).ToArray())
                .Where(t => t.GetInterfaces().Contains(ServiceKeyType))
                .AsImplementedInterfaces()
                .PropertiesAutowired();
            services.RegisterAssemblyTypes(EntryAssembly)
              .Where(t => t.GetInterfaces().Contains(ServiceKeyType))
              .AsImplementedInterfaces()
              .PropertiesAutowired();

            //注册controller
            services.RegisterAssemblyTypes(EntryAssembly)
                .Where(q => q.GetInterfaces().Contains(typeof(IBaseController)))
                .PropertiesAutowired();
            services.RegisterAssemblyTypes(EntryAssembly.GetReferencedAssemblies().Select(q => Assembly.Load(q)).ToArray())
                .Where(t => t.GetInterfaces().Contains(typeof(IBaseController)))
                .PropertiesAutowired();
        }
    }
}
