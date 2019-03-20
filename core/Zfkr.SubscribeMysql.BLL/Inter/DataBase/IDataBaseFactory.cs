using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zfkr.SubscribeMysql.Connector;

namespace Zfkr.SubscribeMysql.BLL.Inter.DataBase
{
    interface IDataBaseFactory
    {
        void GetTableData(MySqlResponseData data);
    }
}
