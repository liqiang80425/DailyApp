using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyApp.WPF.DTOs
{
    /// <summary>
    /// 接收API待办事项统计的数据模型
    /// </summary>
    internal class StatWaitDTO
    {
        /// <summary>
        /// 待办事项总数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 已完成数量
        /// </summary>
        public int FinishCount { get; set; }

        /// <summary>
        /// 完成比例
        /// </summary>
        public string FinishPercent { get; set; }
    }
}
