using AutoMapper;
using DailyApp.API.ApiReponses;
using DailyApp.API.DataModel;
using DailyApp.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyApp.API.Controllers
{
    /// <summary>
    /// 账户接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        private readonly DailyDbContext db;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_db"></param>
        public AccountController(DailyDbContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="accountInfoDTO">注册信息 </param>
        /// <returns>-1:账号已经存在 1:注册成功 -99:位置错误</returns>
        [HttpPost]
        public IActionResult Reg(AccountInfoDTO accountInfoDTO)
        {
            ApiResponse res = new ApiResponse();//响应的数据
            //业务
            try
            {
                //1、账号是否存在(没有考虑高并发)
                var dbAccount = db.AccountInfo.Where(t => t.Account == accountInfoDTO.Account).FirstOrDefault();
                if (dbAccount != null)
                {
                    res.ResultCode = -1;//账号已存在
                    res.Msg = "对不起,账号被注册";

                    return Ok(res);
                }

                //2、如果不存在则添加账号
                //DTO->AccountInfo
                AccountInfo accountInfo = mapper.Map<AccountInfo>(accountInfoDTO);
               
                
                db.AccountInfo.Add(accountInfo);
                int result = db.SaveChanges();//保存 受影响的行数
                if (result == 1)
                {
                    res.ResultCode = 1;//账号注册成功
                    res.Msg = "账号注册成功";
                }
                else
                {
                    res.ResultCode = -99;//失败
                    res.Msg = "服务器,请稍等...";
                }
            }
            catch (Exception)
            {
                res.ResultCode = -99;//失败
                res.Msg = "服务器,请稍等...";
            }

            return Ok(res);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="pwd">密码(MD5值)</param>
        /// <returns>登录信息 1-登录成功。-1:账号错误。-2：密码错误 -99:未知错误</returns>
        [HttpGet]
        public IActionResult Login(string account, string pwd)
        {
            ApiResponse res = new ApiResponse();

            try
            {
                var dbAccountInfo = db.AccountInfo.Where(t => t.Account == account).FirstOrDefault();
                if (dbAccountInfo == null)
                {
                    res.ResultCode = -1;//账号错误
                    res.Msg = "账号错误";

                    return Ok(res);
                }

                if (dbAccountInfo.Pwd != pwd)
                {
                    res.ResultCode = -2;//密码错误
                    res.Msg = "密码错误";

                    return Ok(res);
                }

                res.ResultCode =1;//登录成功
                res.Msg = "登录成功";
                res.ResultData = dbAccountInfo;

                return Ok(res);
            }
            catch (Exception)
            {
                res.ResultCode = -99;//未知错误
                res.Msg = "服务器忙,请稍后...";
            }

            return Ok(res);
        }
    }
}
