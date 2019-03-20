using Jijia.Infrastructure.BasicOperations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zfkr.SubscribeMysql.General.Model;

namespace Zfkr.SubscribeMysql.General
{
    public class ConfigOperation
    {
        #region 缓存过期配置
        /// <summary>
        /// 缓存地址
        /// </summary>
        private static string ConfigPath
        {
            get
            {
                return ConfigurationManager.AppSettings["ConfigPath"] ?? "";
            }
        }
        /// <summary>
        /// 缓存Key
        /// </summary>
        private static string ConfigCacheKey
        {
            get
            {
                return ConfigurationManager.AppSettings["ConfigCacheKey"] ?? "";
            }
        }
        /// <summary>
        /// 配置文件默认过期时间
        /// </summary>
        private static double ConfigCacheExpire
        {
            get
            {
                return double.Parse(ConfigurationManager.AppSettings["ConfigCacheExpire"] ?? "60");
            }
        }

        #endregion

        public static Config Config
        {
            get
            {
                return BasicOperationJsonObject.GetJsonObject<Config>(ConfigCacheKey, ConfigPath, ConfigCacheExpire);
            }
        }
    }
}
