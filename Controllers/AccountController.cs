using Authuntication.ViewModels;
using GpProject.Models;

using GpProject.ViewModels;

using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GpProject.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<User> userMngr, SignInManager<User> signInMngr, RoleManager<IdentityRole> role)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            roleManager = role;
           
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel registrationViewModel)
        {
            if (ModelState.IsValid)
            { 
                var user = new User
                {
                    UserName = registrationViewModel.UserName,
                    FirstName = registrationViewModel.FirstName,
                    LastName = registrationViewModel.LastName,
                    Email = registrationViewModel.Email,
                    PhoneNumber = registrationViewModel.Phone,
                    Country  = registrationViewModel.Country,
                    City = registrationViewModel.City,
                    District = registrationViewModel.District,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user, registrationViewModel.Password);
                if (result.Succeeded)
                {
                    result = await userManager.AddToRoleAsync(user, "Person");
                    var Code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var Link = Url.Action(nameof(VerifyEmail), "Account", new { UserId = user.Id, Code }, Request.Scheme);

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("ToGether", "abdooooahmed7@gmail.com"));
                    message.To.Add(new MailboxAddress(user.UserName, user.Email));
                    message.Subject = "Email Confirmation";
                    message.Body = new TextPart("html")
                    {
                        Text = $"<a href=\"{Link}\">Verify Email</a>"
                    };
                    using (var client = new SmtpClient())
                    {
                        client.SslProtocols = SslProtocols.Ssl3 | SslProtocols.Ssl2 | SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13;
                        client.CheckCertificateRevocation = false;
                        client.Connect("smtp.gmail.com", 465, true);
                        client.Authenticate("abdooooahmed7@gmail.com", "Samara24121977");
                        client.Send(message);
                        client.Disconnect(true);
                    }
                    return RedirectToAction("EmailVerification");

                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);

                        return RedirectToAction("Index", "User"); 
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }


            }
            
            return View(registrationViewModel);
        }

        public async Task<IActionResult> VerifyEmail(string UserId, string Code)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null) return BadRequest();
            var result = await userManager.ConfirmEmailAsync(user, Code);
            if (result.Succeeded)
            {
                return View();
            }
            return BadRequest();
        }

        public IActionResult EmailVerification() => View();

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync()
        {
            
            var Login =new LoginViewModel{
                ReturnUrl = "",
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(Login);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var User = await userManager.FindByEmailAsync(loginViewModel.Email);
                if (User == null)
                {
                    User = await userManager.FindByNameAsync(loginViewModel.Email);
                }
                    if (User != null)
                {
                    var result = await signInManager.PasswordSignInAsync(User, loginViewModel.Password, isPersistent: loginViewModel.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        User.Status = true;
                        var role = await userManager.GetRolesAsync(User);
                       
                        await userManager.UpdateAsync(User);
                        if(role[0] == "Admin")
                        {
                            return RedirectToAction("Dashboard", "Admin");
                        }
                        if (!string.IsNullOrEmpty(loginViewModel.ReturnUrl) && Url.IsLocalUrl(loginViewModel.ReturnUrl))
                        {
                            return Redirect(loginViewModel.ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "User");
                        }
                    }
                }
            }
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account",new { ReturnUrl = returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }


        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            LoginViewModel loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins =(await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View("Login", loginViewModel);
            }
            // Get the login information about the user from the external login provider
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState
                    .AddModelError(string.Empty, "Error loading external login information.");

                return View("Login", loginViewModel);
            }

            // If the user already has a login (i.e if there is a record in AspNetUserLogins
            // table) then sign-in the user with this external login provider
            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                if (returnUrl == "/")
                {
                    return RedirectToAction("Index", "User");
                }
                return LocalRedirect(returnUrl);
            }
            // If there is no record in AspNetUserLogins table, the user may not have
            // a local account
            else
            {
                // Get the email claim value
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (email != null)
                {
                    // Create a new user without password if we do not have a user already
                    var user = await userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new User
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                        };
                        await userManager.CreateAsync(user);
                        var result = await userManager.AddToRoleAsync(user, "Person");
                    }
                    // Add a login (i.e insert a row for the user in AspNetUserLogins table)
                    await userManager.AddLoginAsync(user, info);
                    await signInManager.SignInAsync(user, isPersistent: false);
                    if (returnUrl == "/")
                    {
                        return RedirectToAction("Index", "User");
                    }
                    return LocalRedirect(returnUrl);
                }

                // If we cannot find the user email we cannot continue
                ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
                ViewBag.ErrorMessage = "Please contact support on Pragim@PragimTech.com";

                return View("Error");
            }
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordView model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await userManager.FindByEmailAsync(model.Email);
                // If the user is found AND Email is confirmed
                if (user != null && await userManager.IsEmailConfirmedAsync(user))
                {
                    // Generate the reset password token
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    // Build the password reset link
                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                            new { email = model.Email, token = token }, Request.Scheme);

                    // Log the password reset link


                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("GP_Project", "abdooooahmed7@gmail.com"));
                    message.To.Add(new MailboxAddress(user.UserName, user.Email));
                    message.Subject = "Email Confirmation";
                    message.Body = new TextPart("html")
                    {
                        Text = $"<a href=\"{passwordResetLink}\">Verify Email</a>"
                    };
                    using (var client = new SmtpClient())
                    {
                        client.SslProtocols = SslProtocols.Ssl3 | SslProtocols.Ssl2 | SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13;
                        client.CheckCertificateRevocation = false;
                        client.Connect("smtp.gmail.com", 465, true);
                        client.Authenticate("abdooooahmed7@gmail.com", "Samara24121977");
                        client.Send(message);
                        client.Disconnect(true);
                    }
                    // Send the user to Forgot Password Confirmation view
                    return View("ForgotPasswordConfirmation");
                }

                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist or is not confirmed
                return View("ForgotPasswordConfirmation");
            }

            return View(model);
        }


        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            // If password reset token or email is null, most likely the
            // user tried to tamper the password reset link
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordView model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    // reset the user password
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                    // Display validation errors. For example, password reset token already
                    // used to change the password or password complexity rules not met
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist
                return View("ResetPasswordConfirmation");
            }
            // Display validation errors if model state is not valid
            return View(model);
        }
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            var User = await userManager.GetUserAsync(this.User);
            User.Status = false;
            await userManager.UpdateAsync(User);
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }




    }
}
