using Microsoft.EntityFrameworkCore;

namespace Study.API.DataModel
{
    public class StudyDbContext:DbContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public StudyDbContext(DbContextOptions<StudyDbContext> options):base(options)
        {
       
        }

        public DbSet<AccountInfo> AccountInfo { get; set; }
    }
}
