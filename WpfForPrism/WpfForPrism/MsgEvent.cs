using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfForPrism
{
    /// <summary>
    /// 发布 订阅 消息事件类
    /// </summary>
    internal class MsgEvent:PubSubEvent<string>
    {
    }
}
