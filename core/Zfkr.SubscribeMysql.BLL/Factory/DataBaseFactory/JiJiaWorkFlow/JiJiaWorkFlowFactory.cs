using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zfkr.SubscribeMysql.BLL.Factory.DataBaseFactory.JiJiaWorkFlow.Table;
using Zfkr.SubscribeMysql.BLL.Inter.DataBase;
using Zfkr.SubscribeMysql.BLL.Inter.Table;
using Zfkr.SubscribeMysql.Connector;
using Zfkr.SubscribeMysql.General;
using Newtonsoft.Json;

namespace Zfkr.SubscribeMysql.BLL.Factory.DataBaseFactory.JiJiaWorkFlow
{
    public class JiJiaWorkFlowFactory : IDataBaseFactory
    {
        public void GetTableData(MySqlResponseData data)
        {
            ITableFactory table = null;
            switch (data.TableName.ToLower())
            {
                case "wf_workflowtask":
                    table = new WorkFlowTaskBLL();
                    break;
                case "b_applyrelation":
                    table = new ApplyRelationBLL();
                    break;
                case "b_applyinstance":
                    table = new ApplyInstanceBLL();
                    break;
                default:
                    break;
            }
            if (table != null)
                table.DataAnalysis(data);
        }
    }
}
