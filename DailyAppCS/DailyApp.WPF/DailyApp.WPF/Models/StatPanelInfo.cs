using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyApp.WPF.Models
{
    /// <summary>
    /// 首页统计面板信息
    /// </summary>
    internal class StatPanelInfo : BindableBase
    {
        /// <summary>
        /// 统计项图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 统计项名称
        /// </summary>
        public string ItemName { get; set; }

        private string _Result;

        /// <summary>
        /// 统计结果
        /// </summary>
        public string Result
        {
            get { return _Result; }
            set
            {
                _Result = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 背景颜色
        /// </summary>
        public string BackColor { get; set; }

        /// <summary>
        /// 点击 跳转到 界面名称
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// 面板里手形状
        /// </summary>
        public string Hand
        {
            get
            {
                if (ItemName == "完成比例")
                {
                    return "";
                }
                else
                {
                    return "Hand";
                }
            }
        }
    }
}
