using GpProject.Data;
using GpProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.IO;
using System.Net.Http;
using GpProject.ViewModels;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace GpProject.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext DbContext;
        private UserManager<User> userManager;
        private readonly INotyfService _notyfService;
        private SignInManager<User> signInManager;
        public UserController(ApplicationDbContext applicationDbContext, UserManager<User> userMngr, SignInManager<User> signInMngr, INotyfService notyf)
        {
            DbContext = applicationDbContext;
            userManager = userMngr;
            _notyfService = notyf;
            signInManager = signInMngr;
        }









        [HttpGet]
        public IActionResult Index()
        {
            var Reports = DbContext.Reports.Include(e=>e.User).ToList();
            foreach(var report in Reports)
            {
                report.CommentCount = DbContext.Comments.Where(e => e.ReportId == report.Id).ToList().Count;
            }
            return View(Reports);
        }







        [HttpGet]
        public IActionResult CreateReport()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateReport(Report report)
        {
            if (ModelState.IsValid)
            {
                var user = this.User;
                var User = await userManager.GetUserAsync(user);
                using (var ms = new MemoryStream())
                {
                    report.Image.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    report.Img = fileBytes;
                }
                report.User = User;
                var url = "http://localhost:5000/upload2";
                var ValidtionURl = "http://localhost:5000/CheckValidation";
                using (var httpclient = new HttpClient())
                {
                    var reportType = 0;
                    if (report.FoundPerson)
                    {
                        reportType = 1;
                    }
                    else
                    {
                        reportType = 0;
                    }
                    using(var content = new MultipartFormDataContent())
                    {
                        var fileContent = new ByteArrayContent(report.Img);
                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { Name = "image", FileName = report.Image.FileName };
                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                        content.Add(fileContent);
                        var result = httpclient.PostAsync(ValidtionURl, content).Result;
                        if (!result.IsSuccessStatusCode)
                        {
                            var error = await result.Content.ReadAsStringAsync();
                            ModelState.AddModelError("Image", error);
                            return View(report);
                        }
                        await DbContext.AddAsync(report);
                        await DbContext.SaveChangesAsync();
                    }
                    using(var content = new MultipartFormDataContent())
                    {
                        if (report.FoundPerson)
                        {
                            reportType = 1;
                        }
                        else
                        {
                            reportType = 0;
                        }
                        var myContent = JsonConvert.SerializeObject(reportType);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var Data = new ByteArrayContent(buffer);
                        Data.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data") { Name = "reportType" };
                        content.Add(Data);
                        myContent = JsonConvert.SerializeObject(report.User.Id);
                        buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        Data = new ByteArrayContent(buffer);
                        Data.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data") { Name = "UserId" };
                        content.Add(Data);

                        myContent = JsonConvert.SerializeObject(report.Id);
                        buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        Data = new ByteArrayContent(buffer);
                        Data.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data") { Name = "reportId" };
                        content.Add(Data);

                        var fileContent = new ByteArrayContent(report.Img);
                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { Name = "image", FileName = report.Image.FileName };
                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                        content.Add(fileContent);

                        var result = httpclient.PostAsync(url, content).Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }

            }
            return View(report);
        }








        public async Task<IActionResult> Profile()
        {
            var user = this.User;
            var User = await userManager.GetUserAsync(user);
            User.Reports = DbContext.Reports.Where(report => report.User.Id == User.Id).Include(e => e.Comments).ToList();
            return View(User);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Profile(string id)
        {
            
            var User = await userManager.FindByIdAsync(id);
            User.Reports = DbContext.Reports.Where(report => report.User.Id == User.Id).Include(e => e.Comments).ToList();
            return View(User);
        }













        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = this.User;
            var User = await userManager.GetUserAsync(user);
            var UserData = new EditProfileData()
            {
                UserName = User.UserName,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Email = User.Email,
                Phone = User.PhoneNumber,
                City = User.City,
                Country = User.Country,
                District = User.District,
                
            };
            
            return View(UserData);
        }


        [HttpGet]
        public async Task<IActionResult> ShowMatchedReport(string Id,int order=1)
        {
           
                var ids = JsonConvert.DeserializeObject<List<RelatedReport>>(Id); ;
                ViewData["Ids"] = Id;
                ViewData["Order"] = order;
                List<List<Report>> reports = new List<List<Report>>();
                foreach (var id in ids)
                {
                    List<Report> report = new List<Report>();
                    if (Convert.ToInt16(id.Order) == order)
                    {
                        report.Add(DbContext.Reports.Where(e => e.Id == Convert.ToInt32(id.ID)).Include(e => e.Comments).Include(e => e.User).FirstOrDefault());
                        report.Add(DbContext.Reports.Where(e => e.Id == Convert.ToInt32(id.relatedId)).Include(e => e.Comments).Include(e => e.User).FirstOrDefault());
                        foreach (var r in report)
                        {
                            r.CommentCount = DbContext.Comments.Where(e => e.ReportId == r.Id).ToList().Count;
                        }
                        if (report == null)
                        {
                            return BadRequest();
                        }
                        reports.Add(report);
                    }

                }

                return View(reports);


        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileData userData)
        {
            if (ModelState.IsValid)
            {
                var user = this.User;
                var User = await userManager.GetUserAsync(user);
                if (userData.NewPassword == null)
                {
                    var result = await signInManager.CheckPasswordSignInAsync(User, userData.Password, false);
                    if (result.Succeeded)
                    {
                        User.FirstName = userData.FirstName;
                        User.LastName = userData.LastName;
                        User.UserName = userData.UserName;
                        User.Email = userData.Email;
                        User.PhoneNumber = userData.Phone;
                        User.District = userData.District;
                        User.Country = userData.Country;
                        User.City = userData.City;
                        if(userData.Image != null)
                        {
                            User.Img = ConvertImage(userData.Image);
                        }
                        await userManager.UpdateAsync(User);
                        _notyfService.Success("Update submited");
                    }
                    else
                    {
                        _notyfService.Error("Error");
                        ModelState.AddModelError("Password", "Password is uncorrect");
                    }
                }
                else
                {
                    var result = await userManager.ChangePasswordAsync(User, userData.Password, userData.NewPassword);
                    if (result.Succeeded)
                    {

                        User.FirstName = userData.FirstName;
                        User.LastName = userData.LastName;
                        User.UserName = userData.UserName;
                        User.Email = userData.Email;
                        User.PhoneNumber = userData.Phone;
                        User.District = userData.District;
                        User.Country = userData.Country;
                        User.City = userData.City;
                        await userManager.UpdateAsync(User);

                        _notyfService.Success("Update submited");
                    }
                    else
                    {
                        _notyfService.Error("Error");

                        ModelState.AddModelError("Password", "Password is uncorrect");
                    }
                    
                }

            }
            return View(userData);
        }



        
        






        [HttpPost]
        public async Task<IActionResult> Search(string search)
        {
            var Reports = SearchForReport(search);
            var Users = SearchUser(search);
            var SearchResult = new SearchVM { Reports = Reports, Users = Users };
            return View(SearchResult);

        }

        [HttpGet]
        public async Task<IActionResult> Search()
        {
            
            return View();

        }










        public List<Report> SearchForReport(string search)
        {
            var Reports = new List<Report>();
            var Report = DbContext.Reports.Where(x=>x.FirstName.Contains(search)).ToList();
            if(Report !=null){
                foreach(var R in Report)
                {
                    Reports.Add(R);
                }
            }
            Report = DbContext.Reports.Where(x => x.LastName.Contains(search)).ToList();
            if (Report != null)
            {
                foreach (var R in Report)
                {
                    Reports.Add(R);
                }
            }
            Report = DbContext.Reports.Where(x => x.City.Contains(search)).ToList();
            if (Report != null)
            {
                foreach (var R in Report)
                {
                    Reports.Add(R);
                }
            }
            Report = DbContext.Reports.Where(x => x.Country.Contains(search)).ToList();
            if (Report != null)
            {
                foreach (var R in Report)
                {
                    Reports.Add(R);
                }
            }
            

            return Reports;
        }
        public List<User> SearchUser(string search)
        {
            var Users = new List<User>();
            var User = userManager.Users.Where(s=>s.FirstName.Contains(search)).Include(x=>x.Reports).ToList();
            if (User != null)
            {
                foreach (var U in User)
                {
                    if(Users.Contains(U))
                    {
                        continue;
                    }
                    Users.Add(U);
                    
                }
            }

            User = userManager.Users.Where(s => s.LastName.Contains(search)).Include(x => x.Reports).ToList();
            if (User != null)
            {
                foreach (var U in User)
                {
                    if (Users.Contains(U))
                    {
                        continue;
                    }
                    Users.Add(U);
                }
            }

             User = userManager.Users.Where(s => s.UserName.Contains(search)).Include(x => x.Reports).ToList();
            if (User != null)
            {
                foreach (var U in User)
                {
                    if (Users.Contains(U))
                    {
                        continue;
                    }
                    Users.Add(U);
                }
            }

            User = userManager.Users.Where(s => s.Email.Contains(search)).Include(x => x.Reports).ToList();
            if (User != null)
            {
                foreach (var U in User)
                {
                    if (Users.Contains(U))
                    {
                        continue;
                    }
                    Users.Add(U);
                }
            }

            User = userManager.Users.Where(s => s.PhoneNumber.Contains(search)).Include(x => x.Reports).ToList();
            if (User != null)
            {
                foreach (var U in User)
                {
                    if (Users.Contains(U))
                    {
                        continue;
                    }
                    Users.Add(U);
                }
            }

             User = userManager.Users.Where(s => s.City.Contains(search)).Include(x => x.Reports).ToList();
            if (User != null)
            {
                foreach (var U in User)
                {
                    if (Users.Contains(U))
                    {
                        continue;
                    }
                    Users.Add(U);
                }
            }

            User = userManager.Users.Where(s => s.Country.Contains(search)).Include(x => x.Reports).ToList();
            if (User != null)
            {
                foreach (var U in User)
                {
                    if (Users.Contains(U))
                    {
                        continue;
                    }
                    Users.Add(U);
                }
            }
            return Users;
        }
        
        public async Task<IActionResult> ReportComment(int Id, bool reply)
        {
            if(reply){
                ViewData["reply"] = reply;
                var Comment = DbContext.Comments.Where(x => x.Id == Id).Include(x => x.Report).FirstOrDefault();
                Comment.Report = DbContext.Reports.Where(x => x.Id == Comment.Report.Id).Include(x => x.User).Include(x => x.Comments).FirstOrDefault();
                Comment.Report.CommentCount = Comment.Report.Comments.Count;
                Comment.CommentReplies = DbContext.commentReplies.Where(x=>x.CommentId==Comment.Id).Include(x=>x.User).ToList();
                return View(Comment);
            }
            else
            {
                ViewData["reply"] = reply;
                var Comment = DbContext.Comments.Where(x => x.Id == Id).Include(x => x.Report).FirstOrDefault();
                Comment.Report = DbContext.Reports.Where(x => x.Id == Comment.Report.Id).Include(x => x.User).Include(x=>x.Comments).FirstOrDefault();
                Comment.Report.CommentCount = Comment.Report.Comments.Count;
                return View(Comment);
            }
            

        }
        public async Task<IActionResult> ContactUS()
        {
            return View();
        }

        public byte[] ConvertImage(IFormFile image)
        {
            byte[] img;
            using (var ms = new MemoryStream())
            {
                image.CopyTo(ms);
                var fileBytes = ms.ToArray();
                img = fileBytes;
            }
            return img;

        }
        


    }
}
