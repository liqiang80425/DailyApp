using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyApp.API.DataModel
{
    /// <summary>
    /// 登录账号数据模型
    /// </summary>
    [Table("AccountInfo")]
    public class AccountInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Key]//主键 自增
        public int AccountId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }

    }
}
