using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Zfkr.SubscribeMysql.BLL.Factory;
using Zfkr.SubscribeMysql.Buffer.Redis.JiJiaWorkFlow;

namespace Zfkr.SubscribeMysql.Services
{
    public partial class SubscribeMysqlService : ServiceBase
    {
        public SubscribeMysqlService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            new LogSubscribe().SubscribeLog();
        }

        protected override void OnStop()
        {
        }
    }
}
