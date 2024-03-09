namespace DailyApp.API.DTOs
{
    /// <summary>
    /// 备忘录DTO（添加备忘录接收、显示备忘录）
    /// </summary>
    public class MemoDTO
    {
        /// <summary>
        /// 备忘录ID
        /// </summary>
        public int MemoId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string? Content { get; set; }
    }
}
