using Microsoft.EntityFrameworkCore;

namespace DailyApp.API.DataModel
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class DailyDbContext : DbContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public DailyDbContext(DbContextOptions<DailyDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// 账号
        /// </summary>
        public virtual DbSet<AccountInfo> AccountInfo { get; set; }

        /// <summary>
        /// 待办事项
        /// </summary>
        public virtual DbSet<WaitInfo> WaitInfo { get; set; }

        /// <summary>
        /// 备忘录
        /// </summary>
        public virtual DbSet<MemoInfo> MemoInfo { get; set; }
    }
}
