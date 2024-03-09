using AutoMapper;
using DailyApp.API.ApiReponses;
using DailyApp.API.DataModel;
using DailyApp.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyApp.API.Controllers
{
    /// <summary>
    /// 备忘录接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemoController : ControllerBase
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
        public MemoController(DailyDbContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }

        /// <summary>
        /// 统计备忘录总数据
        /// </summary>
        /// <returns>1:查询正确 -99:异常</returns>
        [HttpGet]
        public IActionResult StatMomo() 
        {
            ApiResponse res = new ApiResponse();

            try
            {
                int count = db.MemoInfo.Count();
                res.ResultCode = 1;
                res.Msg = "查询正确";
                res.ResultData = count;
            }
            catch (Exception)
            {
                res.ResultCode = -99;
                res.Msg = "服务器忙,请稍后...";
            }

            return Ok(res);
        }

        /// <summary>
        /// 添加备忘录
        /// </summary>
        /// <param name="memoDTO">备忘录信息</param>
        /// <returns>1:添加成功，-1:失败， -99:异常</returns>
        [HttpPost]
        public IActionResult AddMomo(MemoDTO memoDTO)
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                MemoInfo memoInfo= mapper.Map<MemoInfo>(memoDTO);

                db.MemoInfo.Add(memoInfo);

                int result = db.SaveChanges();
                if (result == 1)
                {
                    apiResponse.ResultCode = 1;
                    apiResponse.Msg = "备忘录添加成功";
                }
                else
                {
                    apiResponse.ResultCode = -1;
                    apiResponse.Msg = "备忘录添加失败";
                }
            }
            catch (Exception)
            {
                apiResponse.ResultCode = -99;
                apiResponse.Msg = "服务器忙,请稍后...";
            }

            return Ok(apiResponse);
        }

        /// <summary>
        /// 备忘录查询
        /// </summary>
        /// <param name="title">标题(模糊查询)</param>
        /// <returns>1:查询成功  -99:异常</returns>
        [HttpGet]
        public IActionResult QueryMemo(string? title)
        { 
            ApiResponse apiResponse=new ApiResponse();

            try
            {
                var query = from A in db.MemoInfo
                            select new MemoDTO {
                                MemoId = A.MemoId,
                                Title = A.Title,
                                 Content=A.Content
                            };
                if (!string.IsNullOrEmpty(title))
                {
                    query = query.Where(t => t.Title.Contains(title));
                }

                apiResponse.ResultCode = 1;
                apiResponse.Msg = "查询成功";
                apiResponse.ResultData = query;
            }
            catch (Exception)
            {
                apiResponse.ResultCode = -99;
                apiResponse.Msg = "服务器忙,请稍后...";
            }

            return Ok(apiResponse);
        }

        /// <summary>
        /// 编辑备忘录信息
        /// </summary>
        /// <param name="memoDTO">备忘录新信息</param>
        /// <returns>1::编辑成功, -1:失败,-2:ID传错了， -99:异常</returns>
        [HttpPut]
        public IActionResult EditMemo(MemoDTO memoDTO)
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                var dbInfo = db.MemoInfo.Find(memoDTO.MemoId);

                if (dbInfo == null)
                {
                    apiResponse.ResultCode = -2;
                    apiResponse.Msg = "请正确传备忘录ID";

                    return Ok(apiResponse);
                }

                dbInfo.Title = memoDTO.Title;
                dbInfo.Content = memoDTO.Content;

                int result = db.SaveChanges();
                if (result == 1)
                {
                    apiResponse.ResultCode = 1;
                    apiResponse.Msg = "备忘录编辑成功";
                }
                else
                {
                    apiResponse.ResultCode = -1;
                    apiResponse.Msg = "备忘录编辑失败";
                }
            }
            catch (Exception)
            {
                apiResponse.ResultCode = -99;
                apiResponse.Msg = "服务器忙,请稍后...";
            }

            return Ok(apiResponse);
        }

        /// <summary>
        /// 删除备忘录
        /// </summary>
        /// <param name="memoId">备忘录ID</param>
        /// <returns>1:删除成功 -2:ID传错了 -1:删除失败 -99:异常</returns>
        [HttpDelete]
        public IActionResult DelMemo(int memoId)
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                var dbInfo = db.MemoInfo.Find(memoId);

                if (dbInfo == null)
                {
                    apiResponse.ResultCode = -2;
                    apiResponse.Msg = "请正确传备忘录ID";

                    return Ok(apiResponse);
                }

                db.MemoInfo.Remove(dbInfo);
                int result = db.SaveChanges();
                if (result == 1)
                {
                    apiResponse.ResultCode = 1;
                    apiResponse.Msg = "备忘录删除成功";
                }
                else
                {
                    apiResponse.ResultCode = -1;
                    apiResponse.Msg = "备忘录删除失败";
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
