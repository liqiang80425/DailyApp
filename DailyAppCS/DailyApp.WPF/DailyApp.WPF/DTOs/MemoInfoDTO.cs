using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyApp.WPF.DTOs
{
    /// <summary>
    /// 备忘录DTO
    /// </summary>
    internal class MemoInfoDTO
    {
        /// <summary>
        /// 备忘录ID
        /// </summary>
        public int MemoId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}
