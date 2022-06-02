using GpProject.Data;
using GpProject.Models;
using GpProject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GpProject.Controllers.Api
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ApplicationDbContext DbContext;
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        public ReportController(ApplicationDbContext applicationDbContext, UserManager<User> userMngr, SignInManager<User> signInMngr)
        {
            DbContext = applicationDbContext;
            userManager = userMngr;
            
            signInManager = signInMngr;
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteReport(string id)
        {
            using (var httpclient = new HttpClient())
            {
                var url = "http://localhost:5000/Delete/" + id;
                var result = httpclient.DeleteAsync(url).Result;
                
                if (result.IsSuccessStatusCode)
                {
                    var report = DbContext.Reports.Where(report => report.Id == Int32.Parse(id)).First();
                    DbContext.Remove(report);
                    DbContext.SaveChanges();
                    
                }
                else
                {
                    return BadRequest();
                }
            }
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetReportImage(string id)
        {
            
            var report = DbContext.Reports.Where(e => e.Id == Convert.ToInt32(id)).FirstOrDefault();
            if(report == null)
            {
                return BadRequest();
            }
            return Ok(report.Img);
        }


        [HttpPost] 
        public async Task<IActionResult> AddComment([FromBody] Comment comment)
        {
            comment.Report = DbContext.Reports.Where(x => x.Id == comment.ReportId).Include(x=>x.User).FirstOrDefault();
            if(comment.CommentData == "")
            {
                return BadRequest();
            }
            DbContext.Add(comment);

            DbContext.SaveChanges();
            var notification = new Notification() {
                Comment = comment,
                CommentID = comment.Id,
                Description = comment.Report.User.UserName + " commented on your report",
                User = comment.Report.User,
                UserId = comment.Report.User.Id,
                Reply = false,
                IsReaded = false
            };
            DbContext.Add(notification);
            DbContext.SaveChanges();
            comment.Report = null;
            return Ok(comment);
            
        }
        [HttpGet]
        [Route("GetComment")]
        public async Task<IActionResult> GetComment(int from,int to,int reportId)
        {
            var commentList = DbContext.Comments.Where(e=>e.Report.Id==reportId).OrderBy(c => c.Id).Skip(from - 1).Take(to - from).Include(e=>e.User).ToList();
            var count = DbContext.Comments.ToList().Count;
            if(to >= count)
            {
                var Comments = commentList.Select(c => new CommentDataVM { CommentData = c.CommentData, date = c.date, UserId = c.UserId, ReportId = c.ReportId, Id = c.Id, Img = c.User.Img, UserName = c.User.UserName,Full=true });
                return Ok(Comments);
            }
            else
            {
                var Comments = commentList.Select(c => new CommentDataVM { CommentData = c.CommentData, date = c.date, UserId = c.UserId, ReportId = c.ReportId, Id = c.Id, Img = c.User.Img, UserName = c.User.UserName,Full=false });

                return Ok(Comments);
            }
            
        }
        [Route("DeleteComment")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = DbContext.Comments.Where(x => x.Id == id).FirstOrDefault();
            DbContext.Comments.Remove(comment);
            DbContext.SaveChanges();
            return Ok();

        }
        [HttpPost]
        [Route("AddReply")]
        public async Task<IActionResult> AddReply([FromBody] CommentReply comment)
        {
            comment.Comment = DbContext.Comments.Where(x => x.Id == comment.CommentId).Include(x => x.User).FirstOrDefault();
            
            DbContext.Add(comment);
            
            DbContext.SaveChanges();
            comment.User = await userManager.GetUserAsync(this.User);
            var notification = new Notification()
            {
                Comment = comment.Comment,
                CommentID = comment.Id,
                Description = comment.User.UserName + " Replied to your comment",
                User = comment.Comment.User,
                UserId = comment.Comment.User.Id,
                Reply = true,
                IsReaded = false
            };
            DbContext.Add(notification);
            DbContext.SaveChanges();
            comment.User = null;
            comment.Comment = null;
            return Ok(comment);

        }





        [HttpPut]
        [Route("EditComment")]
        public async Task<IActionResult> EditComment([FromBody] Comment comment)
        {
            var LastComment = DbContext.Comments.Where(x => x.Id == comment.Id).FirstOrDefault();
            if (LastComment == null)
            {
                return BadRequest();
            }
            LastComment.CommentData = comment.CommentData;
            DbContext.SaveChanges();
            return Ok(comment);

        }


        [Route("DeleteReply")]
        public async Task<IActionResult> DeleteReply(int id)
        {
            var comment = DbContext.commentReplies.Where(x => x.Id == id).FirstOrDefault();
            
            DbContext.commentReplies.Remove(comment);
            DbContext.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("EditReply")]
        public async Task<IActionResult> EditReply([FromBody] CommentReply comment)
        {
            var LastComment = DbContext.commentReplies.Where(x => x.Id == comment.Id).FirstOrDefault();
            if (LastComment == null)
            {
                return BadRequest();
            }
            LastComment.CommentData = comment.CommentData;
            DbContext.SaveChanges();
            return Ok(comment);

        }



        [HttpGet]
        [Route("NotificationCount")]
        public async Task<IActionResult> NotificationCount(string Id)
        {
            var Notifications = DbContext.Notifications.Where(c => c.UserId == Id && c.IsReaded == false).ToList();
            return Ok(Notifications.Count);
        }

        [HttpGet]
        [Route("GetNotification")]
        public async Task<IActionResult> GetNotification(string Id)
        {
            var Notifications = DbContext.Notifications.Where(c => c.UserId == Id).ToList();
            foreach(var notification in Notifications)
            {
                notification.IsReaded = true;
            }
            DbContext.SaveChanges();
            return Ok(Notifications);
        }

        
        
        [HttpGet]
        [Route("GetReply")]
        public async Task<IActionResult> GetReply(int Id)
        {
            var comment = DbContext.Comments.Where(x=>x.Id==Id).FirstOrDefault();
            var userId = this.userManager.GetUserId(this.User);
            if(comment.UserId != userId)
            {
                var replys = DbContext.commentReplies.Where(c => c.CommentId == Id && c.UserId == userId).Include(x => x.User).ToList();
                var data = new List<ReplyCommentDataVM>();
                foreach (var reply in replys)
                {
                    var d = new ReplyCommentDataVM() { CommentId = reply.CommentId, CommentData = reply.CommentData, date = reply.date, Id = reply.Id, Img = reply.User.Img, UserId = reply.UserId, UserName = reply.User.UserName };
                    data.Add(d);

                }
                return Ok(data);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetReplyByUser")]
        public async Task<IActionResult> GetReplyByUser(string UserId, int Id)
        {
            var replys = DbContext.commentReplies.Where(c => c.CommentId == Id && c.UserId == UserId).Include(x => x.User).ToList();
            var data = new List<ReplyCommentDataVM>();
            foreach (var reply in replys)
            {
                var d = new ReplyCommentDataVM() { CommentId = reply.CommentId, CommentData = reply.CommentData, date = reply.date, Id = reply.Id, Img = reply.User.Img, UserId = reply.UserId, UserName = reply.User.UserName };
                data.Add(d);

            }
            return Ok(data);
        }


        [HttpGet]
        [Route("GetUserByReply")]
        public async Task<IActionResult> GetUserByReply(int Id)
        {
            var CommentReply = DbContext.commentReplies.Where(x => x.CommentId == Id).Include(x=>x.User).ToList();
            var Comments = new List<ReplyCommentDataVM>();
            foreach(var c in CommentReply)
            {
                if(Comments.Any(x=>x.UserId==c.UserId))
                {
                    continue;
                }
                var comment = new ReplyCommentDataVM {CommentData=c.CommentData,CommentId=c.CommentId,date=c.date,Id=c.Id,Img=c.User.Img,UserId=c.UserId,UserName=c.User.UserName };
                Comments.Add(comment);
            }
            return Ok(Comments);
        }

    }

    
}
