using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zfkr.SubscribeMysql.Buffer.Redis.JiJiaWorkFlow
{
    public class RedisKey
    {
        /// <summary>
        /// 申请信息
        /// </summary>
        /// <param name="instanceID"></param>
        /// <returns></returns>
        public static string FlowApplyInfoKey(string instanceID)
        {
            return string.Format("Flow:Client:{0}:ApplyInstance", instanceID);
        }
        /// <summary>
        /// 流程表单
        /// </summary>
        /// <param name="instanceID">申请关系ID</param>
        /// <param name="type">关系类型</param>
        /// <returns></returns>
        public static string ApplyFormKey(string instanceID)
        {
            return string.Format("Flow:Client:{0}:ApplyRelation", instanceID);
        }

    }
}
