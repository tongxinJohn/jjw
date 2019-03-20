using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zfkr.SubscribeMysql.BLL.Factory.DataBaseFactory.JiJiaWorkFlow;
using Zfkr.SubscribeMysql.BLL.Inter.DataBase;
using Zfkr.SubscribeMysql.Connector;

namespace Zfkr.SubscribeMysql.BLL.Factory
{
    public class LogSubscribe
    {
        /// <summary>
        /// 订阅数据库日志
        /// </summary>
        public void SubscribeLog()
        {
            SubscribeMysqlConnector.SubscribeMySqlLog += SubscribeConn_SubscribeMySqlLog;
            SubscribeMysqlConnector.StartLinster();
        }

        private void SubscribeConn_SubscribeMySqlLog(MySqlResponseData data)
        {
            if (data != null)
            {
                IDataBaseFactory dataBaseFactory = null;
                switch (data.DataBaseName.ToLower())
                {
                    case "jijiaworkflow":
                        dataBaseFactory = new JiJiaWorkFlowFactory();
                        break;
                    default:
                        break;
                }
                if (dataBaseFactory != null)
                    dataBaseFactory.GetTableData(data);
            }
        }
    }
}
