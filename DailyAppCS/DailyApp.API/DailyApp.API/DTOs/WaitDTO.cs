using System.ComponentModel.DataAnnotations;

namespace DailyApp.API.DTOs
{
    /// <summary>
    /// 待办事项DTO(接收添加待办事项、返回查询/显示数据)
    /// </summary>
    public class WaitDTO
    {
        /// <summary>
        /// 待办事项ID
        /// </summary>
        public int WaitId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// 状态 0-待办  1-已完成 
        /// </summary>
        public int Status { get; set; }
    }
}
