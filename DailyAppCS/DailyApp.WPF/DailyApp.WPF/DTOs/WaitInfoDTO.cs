using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyApp.WPF.DTOs
{
    /// <summary>
    /// 待办事项DTO
    /// </summary>
    internal class WaitInfoDTO
    {
        /// <summary>
        /// 待办事项ID
        /// </summary>
        public int WaitId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 状态 0-待办  1-已完成 
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 背景颜色值
        /// </summary>
        public string BackColor
        {
            get
            {
                return Status == 0 ? "#1E90FF" : "#3CB371";
            }
        }
    }
}
