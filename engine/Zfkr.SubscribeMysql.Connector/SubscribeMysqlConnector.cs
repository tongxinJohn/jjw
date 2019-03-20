using Com.Alibaba.Otter.Canal.Protocol;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Zfkr.SubscribeMysql.Client;
using Zfkr.SubscribeMysql.Client.Impl;
using Zfkr.SubscribeMysql.General;

namespace Zfkr.SubscribeMysql.Connector
{
    public class SubscribeMysqlConnector
    {
        private static ICanalConnector canalConnector = null;

        public static ICanalConnector CanalConnector
        {
            get
            {
                if (canalConnector == null)
                {
                    //创建一个简单 CanalClient 连接对象（此对象不支持集群）传入参数分别为 canal 地址、端口、destination、用户名、密码
                    canalConnector = CanalConnectors.NewSingleConnector(
                        ConfigOperation.Config.CanalServer.CanalServerAddress,
                        ConfigOperation.Config.CanalServer.CanalServerPort,
                        ConfigOperation.Config.CanalServer.Destination,
                       ConfigOperation.Config.CanalServer.Account,
                        ConfigOperation.Config.CanalServer.Pwd);
                }
                return canalConnector;
            }
        }
        /// <summary>
        /// 订阅数据 回调
        /// </summary>
        /// <param name="data"></param>
        public delegate void SubscribeMySqlLogHandler(MySqlResponseData cmd);
        /// <summary>
        /// 事件响应
        /// </summary>
        public static event SubscribeMySqlLogHandler SubscribeMySqlLog;

        public static void StartLinster()
        {
            Thread thread = new Thread(Connector);     //执行的必须是无返回值的方法
            thread.Name = "子线程";
            thread.Start();
        }
        public static void Connector()
        {
            if (CanalConnector != null)
            {
                //连接 Canal
                CanalConnector.Connect();
                //订阅，同时传入 Filter。Filter是一种过滤规则，通过该规则的表数据变更才会传递过来
                //允许所有数据 .*\\..*
                //允许某个库数据 库名\\..*
                //允许某些表 库名.表名,库名.表名
                if (!string.IsNullOrWhiteSpace(ConfigOperation.Config.CanalServer.Filter))
                    CanalConnector.Subscribe(ConfigOperation.Config.CanalServer.Filter);


                while (true)
                {
                    // 每次读取 BatchSize 条 自动确认
                    var message = CanalConnector.Get(ConfigOperation.Config.CanalServer.BatchSize);
                    //批次id 可用于回滚
                    var batchId = message.Id;
                    if (batchId == -1 || message.Entries.Count <= 0)
                    {
                        Thread.Sleep(ConfigOperation.Config.CanalServer.ThreadWait);
                        continue;
                    }
                    PrintEntry(message.Entries);
                }
            }
        }
        /// <summary>
        /// 输出数据
        /// </summary>
        /// <param name="entrys">一个entry表示一个数据库变更</param>
        private static void PrintEntry(List<Entry> entrys)
        {
            foreach (var entry in entrys)
            {
                if (entry.EntryType == EntryType.Transactionbegin || entry.EntryType == EntryType.Transactionend)
                {
                    continue;
                }
                RowChange rowChange = null;
                try
                {
                    //获取行变更
                    rowChange = RowChange.Parser.ParseFrom(entry.StoreValue);
                }
                catch (Exception e)
                {
                }

                if (rowChange != null)
                {
                    //变更类型 insert/update/delete 等等
                    EventType eventType = rowChange.EventType;
                    //输出 insert/update/delete 变更类型列数据
                    foreach (var rowData in rowChange.RowDatas)
                    {
                        switch (eventType)
                        {
                            case EventType.Compatibleproto2:
                                break;
                            case EventType.Insert:
                                PrintColumn(eventType, entry, rowData.AfterColumns.ToList());
                                break;
                            case EventType.Update:
                                //PrintColumn(rowData.BeforeColumns.ToList());
                                PrintColumn(eventType, entry, rowData.AfterColumns.ToList());
                                break;
                            case EventType.Delete:
                                PrintColumn(eventType, entry, rowData.BeforeColumns.ToList());
                                break;
                            case EventType.Create:
                                break;
                            case EventType.Alter:
                                break;
                            case EventType.Erase:
                                break;
                            case EventType.Query:
                                break;
                            case EventType.Truncate:
                                break;
                            case EventType.Rename:
                                break;
                            case EventType.Cindex:
                                break;
                            case EventType.Dindex:
                                break;
                            case EventType.Gtid:
                                break;
                            case EventType.Xacommit:
                                break;
                            case EventType.Xarollback:
                                break;
                            case EventType.Mheartbeat:
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 输出每个列的详细数据
        /// </summary>
        /// <param name="columns"></param>
        private static void PrintColumn(EventType eventType, Entry entry, List<Column> columns)
        {
            if (SubscribeMySqlLog != null && columns != null)
            {
                MySqlResponseData data = new MySqlResponseData()
                {
                    DataBaseName = entry.Header.SchemaName,
                    TableName = entry.Header.TableName,
                    EventType = eventType.ToString(),
                    SubscribeData = columns.Select<Column, SubscribeDataModel>(c => new SubscribeDataModel() { Key = c.Name, Value = c.Value, Updated = c.Updated }).ToList()
                };
                //LogOperation.Logger.LogInformation($"================>输出每个列的详细数据:data[{JsonConvert.SerializeObject(data)}]");
                SubscribeMySqlLog(data);
            }
        }

    }
}
