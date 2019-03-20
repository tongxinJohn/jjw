using System;
using System.Collections.Generic;
using System.Text;

namespace Zfkr.SubscribeMysql.Connector
{
    public class MySqlResponseData
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DataBaseName { get; set; }
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }
        public string EventType { get; set; }

        public List<SubscribeDataModel> SubscribeData { get; set; }
    }
    /// <summary>
    /// 订阅数据模型
    /// </summary>
    public class SubscribeDataModel
    {
        /// <summary>
        /// 字段
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 是否更新
        /// </summary>
        public bool Updated { get; set; }

    }
}
