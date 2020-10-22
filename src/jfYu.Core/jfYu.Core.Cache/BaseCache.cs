using jfYu.Core.Common.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace jfYu.Core.Cache
{
    public abstract class BaseCache
    {
        public CacheType CacheType { get; protected set; }
        private readonly CacheConfig cacheConfig;
        public BaseCache(CacheConfig cacheConfig)
        {
            this.cacheConfig = cacheConfig;
        }
        protected string GetKey(string key)
        {
            if (string.IsNullOrEmpty(cacheConfig.KeySuffix))
                return $"{CacheType}Cache_{key}";
            else
                return $"{cacheConfig.KeySuffix}_{key}";
        }
    }
}
