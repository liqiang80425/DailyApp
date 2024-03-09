using AutoMapper;
using DailyApp.API.ApiReponses;
using DailyApp.API.DataModel;
using DailyApp.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyApp.API.Controllers
{
    /// <summary>
    /// 待办事项接口 
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WaitController : ControllerBase
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
        public WaitController(DailyDbContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }

        /// <summary>
        /// 统计待办数据
        /// </summary>
        /// <returns>1:统计成功,-99:异常</returns>
        [HttpGet]
        public IActionResult StatWait()
        {
            ApiResponse res = new ApiResponse();

            try
            {
                var list = db.WaitInfo.ToList();//所有记录
                var finishList = list.Where(t => t.Status == 1).ToList();

                StatWaitDTO statDto = new StatWaitDTO { TotalCount = list.Count, FinishCount = finishList.Count };

                res.ResultCode = 1;//统计成功
                res.Msg = "统计待办事项成功";
                res.ResultData = statDto;
            }
            catch (Exception)
            {
                res.ResultCode = -99;
                res.Msg = "服务器忙,请稍后...";
            }

            return Ok(res);
        }

        /// <summary>
        /// 添加待办事项
        /// </summary>
        /// <param name="waitDTO">待办事项信息</param>
        /// <returns>1:添加成功，-1:添加失败，-99异常</returns>
        [HttpPost]
        public IActionResult AddWait(WaitDTO waitDTO)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                //DTO->Info
                WaitInfo accountInfo = mapper.Map<WaitInfo>(waitDTO);
                db.WaitInfo.Add(accountInfo);
                int result = db.SaveChanges();//受影响的行数
                if (result == 1)
                {
                    response.ResultCode = 1;
                    response.Msg = "添加待办事项成功";
                }
                else
                {
                    response.ResultCode = -1;
                    response.Msg = "添加待办事项失败";
                }
            }
            catch (Exception)
            {
                response.ResultCode = -99;
                response.Msg = "服务器忙,请稍后....";
            }

            return Ok(response);
        }

        /// <summary>
        /// 获取待办状态的所有待办事项
        /// </summary>
        /// <returns>1：获取成功 -99：异常</returns>
        [HttpGet]
        public IActionResult GetWaiting()
        {
            ApiResponse res = new ApiResponse();

            try
            {
                //linq语法
                var list = from A in db.WaitInfo
                           where A.Status == 0
                           select new WaitDTO
                           {
                               WaitId = A.WaitId,
                               Title = A.Title,
                               Content = A.Content,
                               Status = A.Status,
                           };

                res.ResultCode = 1;
                res.Msg = "获取成功";
                res.ResultData = list;
            }
            catch (Exception)
            {
                res.ResultCode = -99;
                res.Msg = "服务器忙,请稍后...";
            }
            return Ok(res);
        }

        /// <summary>
        /// 修改待办事项状态
        /// </summary>
        /// <param name="newStatusDto">新状态的待办事项</param>
        /// <returns>1:修改成功，-99异常，-1:状态id错误</returns>
        [HttpPut]
        public IActionResult UpdateStatus(WaitDTO newStatusDto)
        {
            ApiResponse res = new ApiResponse();

            try
            {
                var dbInfo = db.WaitInfo.Find(newStatusDto.WaitId);
                if (dbInfo != null)
                {
                    dbInfo.Status = newStatusDto.Status;//部分修改
                    int result = db.SaveChanges();
                    if (result == 1)
                    {
                        res.ResultCode = 1;
                        res.Msg = (newStatusDto.Status == 0 ? "状态成功设置为待办" : "状态成功设置为已完成 ");
                    }
                    else
                    {
                        res.ResultCode = -1;
                        res.Msg = "状态设置失败";
                    }
                }
                else
                {
                    res.ResultCode = -1;
                    res.Msg = "请确认待办事项ID是否正确";
                }
            }
            catch (Exception)
            {
                res.ResultCode = -99;
                res.Msg = "服务器忙,请稍后...";
            }

            return Ok(res);
        }

        /// <summary>
        /// 修改待办事项
        /// </summary>
        /// <param name="newDto">新待办事项</param>
        /// <returns>1:修改成功，-99异常，-1:状态id错误</returns>
        [HttpPut]
        public IActionResult EditWait(WaitDTO newDto)
        {
            ApiResponse res = new ApiResponse();

            try
            {
                var dbInfo = db.WaitInfo.Find(newDto.WaitId);
                if (dbInfo != null)
                {
                    dbInfo.Status = newDto.Status;
                    dbInfo.Title = newDto.Title;
                    dbInfo.Content = newDto.Content;
                    int result = db.SaveChanges();
                    if (result == 1)
                    {
                        res.ResultCode = 1;
                        res.Msg = "编辑成功";
                    }
                    else
                    {
                        res.ResultCode = -1;
                        res.Msg = "编辑失败";
                    }
                }
                else
                {
                    res.ResultCode = -1;
                    res.Msg = "请确认待办事项ID是否正确";
                }
            }
            catch (Exception)
            {
                res.ResultCode = -99;
                res.Msg = "服务器忙,请稍后...";
            }

            return Ok(res);
        }

        /// <summary>
        /// 查询待办事项
        /// </summary>
        /// <param name="title">标题(模糊查询)</param>
        /// <param name="status">状态(等值查询)</param>
        /// <returns>1:查询成功</returns>
        [HttpGet]
        public IActionResult QueryWait(string? title, int? status)
        {
            ApiResponse res = new ApiResponse();

            try
            {
                var query = from A in db.WaitInfo
                            select new WaitDTO
                            {
                                WaitId = A.WaitId,
                                Title = A.Title,
                                Content = A.Content,
                                Status = A.Status
                            };

                if (!string.IsNullOrEmpty(title))//查询条件
                {
                    query = query.Where(t => t.Title.Contains(title));
                }
                if (status != null)
                {
                    query = query.Where(t => t.Status==status);
                }

                res.ResultCode = 1;
                res.Msg = "查询成功";
                res.ResultData = query;
            }
            catch (Exception)
            {
                res.ResultCode = -99;
                res.Msg = "服务器忙,请稍后...";
            }
            return Ok(res);
        }

        /// <summary>
        /// 删除待办事项
        /// </summary>
        /// <param name="waitId">待办事项ID</param>
        /// <returns>1:删除成功 -2:ID传错了 -1:删除失败 -99:异常</returns>
        [HttpDelete]
        public IActionResult DelWait(int waitId)
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                var dbInfo = db.WaitInfo.Find(waitId);

                if (dbInfo == null)
                {
                    apiResponse.ResultCode = -2;
                    apiResponse.Msg = "请正确传待办事项ID";

                    return Ok(apiResponse);
                }

                db.WaitInfo.Remove(dbInfo);
                int result = db.SaveChanges();
                if (result == 1)
                {
                    apiResponse.ResultCode = 1;
                    apiResponse.Msg = "待办事项删除成功";
                }
                else
                {
                    apiResponse.ResultCode = -1;
                    apiResponse.Msg = "待办事项删除失败";
                }
            }
            catch (Exception)
            {
                apiResponse.ResultCode = -99;
                apiResponse.Msg = "服务器忙,请稍后...";
            }

            return Ok(apiResponse);
        }
    }
}

