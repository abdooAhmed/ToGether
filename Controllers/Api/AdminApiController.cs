using AspNetCoreHero.ToastNotification.Abstractions;
using GpProject.Data;
using GpProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly ApplicationDbContext DbContext;
        private UserManager<User> userManager;
        private readonly INotyfService _notyfService;
        private SignInManager<User> signInManager;
        public AdminApiController(ApplicationDbContext applicationDbContext, UserManager<User> userMngr, SignInManager<User> signInMngr, INotyfService notyf)
        {
            DbContext = applicationDbContext;
            userManager = userMngr;
            _notyfService = notyf;
            signInManager = signInMngr;
        }










        public async Task<IActionResult> DeleteUser(string id)
        {
            var user =await userManager.FindByIdAsync(id);
            var result = await userManager.DeleteAsync(user);
            return Ok(true);
        }
    }
}
