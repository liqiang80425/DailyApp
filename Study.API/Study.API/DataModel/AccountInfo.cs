using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Study.API.DataModel
{
    /// <summary>
    /// 账号信息
    /// </summary>
    [Table("AccountInfo")]
    public class AccountInfo
    {
        [Key]
        public int AccountId { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }
    }
}
