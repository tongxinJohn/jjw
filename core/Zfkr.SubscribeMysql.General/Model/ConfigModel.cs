using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zfkr.SubscribeMysql.General.Model
{
    public class Config
    {
        public CanalServerConfigModel CanalServer { get; set; }

        public RedisConfigModel Redis { get; set; }
    }
    /// <summary>
    /// Canal 服务配置
    /// </summary>
    public class CanalServerConfigModel
    {
        /// <summary>
        /// 服务地址
        /// </summary>
        public string CanalServerAddress { get; set; }
        /// <summary>
        /// 服务端口
        /// </summary>
        public int CanalServerPort { get; set; }
        /// <summary>
        /// canal 配置的 destination，默认为 example
        /// </summary>
        public string Destination { get; set; }
        /// <summary>
        /// canal 帐号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// canal 密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 批量获取数据条数
        /// </summary>
        public int BatchSize { get; set; }
        /// <summary>
        /// 线程等待毫秒
        /// </summary>
        public int ThreadWait { get; set; }
        /// <summary>
        /// 过虑器 [.*\\..*  | 库名\\..* |  库名.表名,库名.表名]
        /// </summary>
        public string Filter { get; set; }
        /// <summary>
        /// 同步类型
        /// </summary>
        public IDictionary<string, List<string>> SyncType { get; set; }
    }
    /// <summary>
    ///Redis配置信息
    /// </summary>
    public class RedisConfigModel {
        /// <summary>
        /// 哨兵地址
        /// </summary>
        public string Sentinel { get; set; }
        /// <summary>
        /// 主库名称
        /// </summary>
        public string MasterName { get; set; }
        /// <summary>
        /// 库索引
        /// </summary>
        public int DBNum { get; set; }

    }
}
