namespace DailyApp.API.DTOs
{
    /// <summary>
    /// 统计待办事项DTO
    /// </summary>
    public class StatWaitDTO
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
        public string FinishPercent
        {
            get
            {
                if (TotalCount == 0)//总数为0
                {
                    return "0.00%";
                }

                return (FinishCount * 100.00 / TotalCount).ToString("f2") + "%";
            }
        }
    }
}
