using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyApp.WPF.DTOs
{
    /// <summary>
    /// 账户DTO
    /// </summary>
    internal class AccountInfoDTO
    {
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

        /// <summary>
        /// 再次输入的密码
        /// </summary>
        public string ConfirmPwd { get; set; }
    }
}
