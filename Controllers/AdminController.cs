using AspNetCoreHero.ToastNotification.Abstractions;
using GpProject.Data;
using GpProject.Models;
using GpProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext DbContext;
        private UserManager<User> userManager;
        private readonly INotyfService _notyfService;
        private SignInManager<User> signInManager;
        public AdminController(ApplicationDbContext applicationDbContext, UserManager<User> userMngr, SignInManager<User> signInMngr, INotyfService notyf)
        {
            DbContext = applicationDbContext;
            userManager = userMngr;
            _notyfService = notyf;
            signInManager = signInMngr;
        }




        public async Task<IActionResult> ManageUsers()
        {
            var UsersManger = new List<UserMangerViewModel>();
            var Users = userManager.GetUsersInRoleAsync("Person").Result;
            foreach(var user in Users)
            {
                var UserManger = new UserMangerViewModel() { User=user,Role="Person"};
                UsersManger.Add(UserManger);
            }
            Users = userManager.GetUsersInRoleAsync("PoliceStation").Result;
            foreach (var user in Users)
            {
                var UserManger = new UserMangerViewModel() { User = user, Role = "PoliceStation" };
                UsersManger.Add(UserManger);
            }
            return View(UsersManger);
        }








        public async Task<IActionResult> Dashboard()
        {
            DasboardVM dasboardVM = new DasboardVM();
            
            dasboardVM.Comments = DbContext.Comments.ToList().Count;
            dasboardVM.users = DbContext.Users.ToList().Count;
            dasboardVM.Reports = DbContext.Reports.ToList().Count;
            return View(dasboardVM);
        }
    }
}
