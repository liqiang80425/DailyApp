using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyApp.API.DataModel
{
    /// <summary>
    /// 备忘录表
    /// </summary>
    [Table("MemoInfo")]
    public class MemoInfo
    {
        /// <summary>
        /// 备忘录ID
        /// </summary>
        [Key]
        public int MemoId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
