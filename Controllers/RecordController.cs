using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using workshop.Models;
using workshop.Data;
using workshop.Domain;

namespace workshop.Controllers
{
    public class RecordController: Controller
    {
        // 数据库上下文
        private readonly RecordDbContext _context;

        public RecordController(RecordDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Query()
        {
            // 设定查询时间界限
            DateTime begin = DateTime.Today,
                     end   = DateTime.Today.AddDays(1);
            // 从上下文中获取记录
            var records = _context.Records
                .Where(record   => record.SignInTime >= begin && record.SignInTime <= end)
                .Select(record  => new
                {
                    StudentId   = record.StudentId,
                    Name        = record.Name,
                    SignInTime  = record.SignInTime,
                    SignOutTime = record.SignOutTime
                }).ToList();
            // 返回JSON序列
            return Json(records);
        }

        [HttpPost]
        public IActionResult SignIn([FromBody]SignInModel model)
        {
            // 根据模型数据创建记录
            Record record = new Record
            {
                StudentId  = model.StudentId,
                Name       = model.Name,
                SignInTime = DateTime.Now
            };
            // 向上下文中添加记录，并保存
            _context.Add(record);
            _context.SaveChanges();

            return Json(new 
            {
                Result  = true,
                Message = ""
            });
        }

        [HttpPost]
        public IActionResult SignOut([FromBody]SignOutModel model)
        {
            // 从上下文中检索是否有签到记录
            Record record = (from r in _context.Records
                             where r.StudentId == model.StudentId
                             && r.SignOutTime == null
                             select r).FirstOrDefault();
            // 若无签到记录，则返回错误信息
            if(record is null)
            {
                return Ok(new
                {
                    Result  = false,
                    Message = "暂无签到信息"
                });
            }

            // 记录签到时间并保存
            record.SignOutTime = DateTime.Now;
            _context.Update(record);
            _context.SaveChanges();

            return Ok(new
            {
                Result  = true,
                Message = ""
            });
        }
    }
}