using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Study.API.DTO;
using Study.API.Reponses;

namespace Study.API.Controllers
{
    /// <summary>
    /// 学习的API接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudyController : ControllerBase
    {
        /// <summary>
        /// 功能一
        /// </summary>
        /// <param name="name">姓名</param>
        /// <returns>返回的姓名</returns>
        [HttpGet]
        public IActionResult Fun1(string name)
        {
            return Ok(name);
        }

        /// <summary>
        /// 功能二
        /// </summary>
        /// <returns>字符串</returns>
        [HttpGet]
        public IActionResult Fun2(string account,string pwd)
        {
            //假设添加
            return Ok(new ApiResponse<AccountDTO> {  ErrorCode=1, Message="登录成功", Result= new AccountDTO { AccountId = 1, Account = "longma", Name = "龙马" } } );
        }
    }
}
