using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyApp.WPF.MsgEvents
{
    /// <summary>
    /// 发布订阅 信息model
    /// </summary>
    internal class MsgEvent:PubSubEvent<string>
    {
    }
}
