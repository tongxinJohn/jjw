using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zfkr.SubscribeMysql.BLL.Inter.Table;
using Zfkr.SubscribeMysql.Buffer.Redis.JiJiaWorkFlow;
using Zfkr.SubscribeMysql.Connector;
using Zfkr.SubscribeMysql.General.Enums;

namespace Zfkr.SubscribeMysql.BLL.Factory.DataBaseFactory.JiJiaWorkFlow.Table
{
    public class ApplyRelationBLL : ITableFactory
    {
        public void DataAnalysis(MySqlResponseData data)
        {
            EnumTable eventType = (EnumTable)Enum.Parse(typeof(EnumTable), data.EventType);

            switch (eventType)
            {
                case EnumTable.Insert:
                case EnumTable.Update:
                    EditAdd(data, eventType);
                    break;
                case EnumTable.Delete:
                    break;
                default:
                    break;
            }
        }

        #region 新增 修改更新缓存
        /// <summary>
        /// 新增|修改
        /// </summary>
        /// <param name="data"></param>
        /// <param name="eventType"></param>
        private void EditAdd(MySqlResponseData data, EnumTable eventType)
        {
            if (data.SubscribeData != null)
            {
                SubscribeDataModel formHtml = null;
                SubscribeDataModel instanceID = data.SubscribeData.Where(c => c.Key.Equals("InstanceID")).FirstOrDefault();
                SubscribeDataModel type = data.SubscribeData.Where(c => c.Key.Equals("Type")).FirstOrDefault();
                SubscribeDataModel typeCode = data.SubscribeData.Where(c => c.Key.Equals("TypeCode")).FirstOrDefault();
                switch (eventType)
                {
                    case EnumTable.Insert:
                        formHtml = data.SubscribeData.Where(c => c.Key.Equals("Html")).FirstOrDefault();
                        break;
                    case EnumTable.Update:
                        formHtml = data.SubscribeData.Where(c => c.Key.Equals("Html") && c.Updated == true).FirstOrDefault();
                        break;
                    case EnumTable.Delete:
                        break;
                    default:
                        break;
                }
                if (formHtml != null && instanceID != null && type != null && typeCode != null)
                {
                    new JiJiaWorkFlowRedis().HashSet<string>(RedisKey.ApplyFormKey(instanceID.Value), type.Value + "-" + typeCode.Value, formHtml.Value);
                }
            }
        }
        #endregion
    }
}
