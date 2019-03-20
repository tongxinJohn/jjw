using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zfkr.SubscribeMysql.Connector;

namespace Zfkr.SubscribeMysql.BLL.Inter.Table
{
    interface ITableFactory
    {
        void DataAnalysis(MySqlResponseData data);
    }
}
