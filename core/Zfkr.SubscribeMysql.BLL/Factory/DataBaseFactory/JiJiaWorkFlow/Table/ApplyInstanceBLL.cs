using System;
using Zfkr.SubscribeMysql.BLL.Inter.Table;
using Zfkr.SubscribeMysql.Connector;
using Zfkr.SubscribeMysql.General.Enums;
using System.Linq;
using Zfkr.SubscribeMysql.Buffer.Redis.JiJiaWorkFlow;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Collections.Generic;

namespace Zfkr.SubscribeMysql.BLL.Factory.DataBaseFactory.JiJiaWorkFlow.Table
{
    class ApplyInstanceBLL : ITableFactory
    {
        public void DataAnalysis(MySqlResponseData data)
        {
            EnumTable eventType = (EnumTable)Enum.Parse(typeof(EnumTable), data.EventType);
            SubscribeDataModel id = data.SubscribeData.Where(c => c.Key.Equals("ID")).FirstOrDefault();
            if (id != null)
            {
                switch (eventType)
                {
                    case EnumTable.Insert:
                        UpdateApplyCache(data.SubscribeData, id.Value);
                        break;
                    case EnumTable.Update:
                        //1流程结束，2流程退回,3流程提交失败
                        SubscribeDataModel updateStatus = data.SubscribeData.Where(c => c.Key.Equals("FlowApplyStatus") && c.Updated == true).FirstOrDefault();
                        if (updateStatus != null && id != null)
                        {
                            if (updateStatus != null)
                            {
                                int status = 0;
                                int.TryParse(updateStatus.Value, out status);
                                switch (status)
                                {
                                    case 1:
                                    case 2:
                                    case 3:
                                        DelCache(id.Value);
                                        break;
                                    default:
                                        UpdateApplyCache(data.SubscribeData, id.Value);
                                        break;
                                }
                            }
                        }
                        else
                        {
                            UpdateApplyCache(data.SubscribeData, id.Value);
                        }
                        break;
                    case EnumTable.Delete:
                        if (id != null)
                        {
                            DelCache(id.Value);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        #region 新增 修改更新缓存
        /// <summary>
        /// 更新申请参数信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="id"></param>
        private void UpdateApplyCache(List<SubscribeDataModel> data, string id)
        {
            new JiJiaWorkFlowRedis().HashSet<string>(RedisKey.FlowApplyInfoKey(id), data);
        }
        #endregion
        #region 删除缓存 来源 [删除数据,变更流程状态(1流程结束，2流程退回,3流程提交失败)]
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="id"></param>
        private void DelCache(string id)
        {
            JiJiaWorkFlowRedis redis = new JiJiaWorkFlowRedis();
            redis.HashDel(RedisKey.FlowApplyInfoKey(id));
            redis.HashDel(RedisKey.ApplyFormKey(id));
        }
        #endregion
    }
}
