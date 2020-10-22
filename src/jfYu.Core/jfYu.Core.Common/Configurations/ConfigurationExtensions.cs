using jfYu.Core.Common.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace jfYu.Core.Common.Configurations
{
    /// <summary>
    /// 配置文件扩展类
    /// </summary>
    public static class ConfigurationExtensions
    {
        public static IConfigurationBuilder AddConfigurationFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
        {
            return AddConfigurationFile(builder, provider: null, path: path, optional: optional, reloadOnChange: reloadOnChange);
        }

        public static IConfigurationBuilder AddConfigurationFile(this IConfigurationBuilder builder, IFileProvider provider, string path, bool optional, bool reloadOnChange)
        {
            path = EnvironmentHelper.GetEnvironmentVariable(path);
            if (File.Exists(path))
            {
                if (provider == null && Path.IsPathRooted(path))
                {
                    provider = new PhysicalFileProvider(Path.GetDirectoryName(path));
                    path = Path.GetFileName(path);
                }
                builder.AddJsonFile(path);
                AppConfig.Configuration = builder.Build();
            }
            return builder;
        }
    }

}
