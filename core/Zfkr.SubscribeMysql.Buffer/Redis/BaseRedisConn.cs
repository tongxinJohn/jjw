using Jijia.Infrastructure.MiddleWare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zfkr.SubscribeMysql.Connector;
using Zfkr.SubscribeMysql.General;

namespace Zfkr.SubscribeMysql.Buffer.Redis
{
    public class BaseRedisConn
    {
        public RedisCache redis;
        /// <summary>
        /// 获取Redis实例对象
        /// </summary>
        public RedisCache Redis
        {
            get
            {
                if (redis == null)
                {
                    redis = new RedisCache(ConfigOperation.Config.Redis.MasterName, ConfigOperation.Config.Redis.Sentinel, ConfigOperation.Config.Redis.DBNum);
                }
                return redis;
            }
            set => redis = value;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public virtual void StringSet(string key, string value)
        {
            Redis.StringSetFromRedis<string>(key, value, false);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        public virtual void StringDel(string key)
        {
            Redis.DelSingleKeyFromRedis(key);
        }
        /// <summary>
        /// 添加|更新hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hashID"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public virtual void HashSet<T>(string hashID, string key, T value)
        {
            Redis.HashSet<T>(hashID, key, value);
        }
        /// <summary>
        /// 批量更新hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hashID"></param>
        /// <param name="subscribeDataModels"></param>
        public virtual void HashSet<T>(string hashID, List<SubscribeDataModel> subscribeDataModels)
        {
            if (subscribeDataModels != null)
            {
                List<StackExchange.Redis.HashEntry> hashEntries = new List<StackExchange.Redis.HashEntry>();
                foreach (SubscribeDataModel item in subscribeDataModels)
                {
                    hashEntries.Add(new StackExchange.Redis.HashEntry(item.Key, item.Value));
                }
                Redis.HashSet<string>(hashID, hashEntries);
            }
        }
        /// <summary>
        /// 删除hash
        /// </summary>
        /// <param name="hashID"></param>
        /// <param name="key"></param>
        public virtual void HashDel(string hashID, string key)
        {
            Redis.HashRemove(hashID, key);
        }
        /// <summary>
        /// 删除整个Hash
        /// </summary>
        /// <param name="hashID"></param>
        public virtual void HashDel(string hashID)
        {
            Redis.HashRemove(hashID);
        }
    }
}
