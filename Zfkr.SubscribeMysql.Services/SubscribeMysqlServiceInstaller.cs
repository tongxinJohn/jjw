using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace Zfkr.SubscribeMysql.Services
{
    [RunInstaller(true)]
    public partial class SubscribeMysqlServiceInstaller : System.Configuration.Install.Installer
    {
        public SubscribeMysqlServiceInstaller()
        {
            InitializeComponent();
        }
    }
}
