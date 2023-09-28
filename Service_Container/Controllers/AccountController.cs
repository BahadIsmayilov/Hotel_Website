using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service_Container.DAL;
using Service_Container.Models.UserResgistration;
using Service_Container.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using static Service_Container.SDUtilities.SD;

namespace Service_Container.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(AppDbContext context,
                                 UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager, 
                                 RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            ViewBag.Countries = _context.Countries;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            ViewBag.Countries = _context.Countries;
            if (!ModelState.IsValid) return View(register);

            ApplicationUser newUser = new ApplicationUser
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                UserName = register.UserName,
                CityId = register.CityId
            };

            IdentityResult identityResult= await  _userManager.CreateAsync(newUser, register.Password);

            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                
                return View(register);
            }
            // add default member role to users
            await _userManager.AddToRoleAsync(newUser,Roles.Member.ToString());

            await _signInManager.SignInAsync(newUser, true);

            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index),"Home");
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LoginViewModel loginView)
        {
            if (!ModelState.IsValid) return View(loginView);

            ApplicationUser user = await _userManager.FindByEmailAsync(loginView.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Email or Password is invalid");
                return View(loginView);
            }

            Microsoft.AspNetCore.Identity.SignInResult signInResult= 
                await _signInManager.PasswordSignInAsync(user,loginView.Password,loginView.RememberMe,true);

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Email or Password is invalid");
                return View(loginView);
            }

            return RedirectToAction(nameof(HomeController.Index),"Home");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordVM)
        {
            if (forgotPasswordVM == null) return NotFound();

            if (!ModelState.IsValid) return View(forgotPasswordVM);

            ApplicationUser user = await _userManager.FindByEmailAsync(forgotPasswordVM.Email);

            SmtpClient client = new SmtpClient("smtp.office365.com", 587)
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential("ismayilovbahad@gmail.com", "B6845841bbb")
            };

            // Passing values to smtp object
            MailMessage message = new MailMessage("ismayilovbahad@gmail.com", forgotPasswordVM.Email)
            {
                IsBodyHtml = true,
                Subject = "Change password",
                Body = $"<a href='https://localhost:44330/Account/ChangePassword/{user.Id}'>Dear {user.FirstName} {user.LastName},click for change password</a>" +
                            "<div style='width:100%'>" +
                                "<div style='width:23%;display:inline-block;background-color:#1876D2;height:3px'></div>" +
                                "<h3 style='font-size: 1.80203rem;line-height: 36px;margin:0;margin-bottom:20px'>Bahad Ismayilov</h3>" +
                                "<p style='color:#00D231; display:inline;font-size: 30px;font-style: italic;font-weight: bold;'>Rezerv</p><p style='color:black; display:inline;font-size: 30px;font-style: italic;font-weight: bold;'>NOW</p>" +
                                "<a href='tel:+994556845841' style='color: rgb(9,117,122);display:block'> +994556845841</a>" +
                                "<a href='www.therentnow.com' target='_blank' style='color: rgb(9,117,122);display:block'>therezervnow</a>" +
                            "</div>"
        };

            await client.SendMailAsync(message);

            return PartialView("_CheckEmailForgotPasswordPartial");
        }
        public async Task<IActionResult> ChangePassword(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            ChangePasswordVM model = new ChangePasswordVM { User = user };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string id, ChangePasswordVM model)
        {
            if (id == null || model == null || !ModelState.IsValid)
            {
                return NotFound();
            }

            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _userManager.ResetPasswordAsync(user, token, model.ConfirmPassword);
            await _context.SaveChangesAsync();
            return PartialView("_ChangePasswordPartial");
        }

        public async Task SeedRoles()
        {
            if (!await _roleManager.RoleExistsAsync(Roles.Admin.ToString()))
            {
              await _roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            }
            if (!await _roleManager.RoleExistsAsync(Roles.Moderator.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
            }
            if (!await _roleManager.RoleExistsAsync(Roles.Member.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole(Roles.Member.ToString()));
            }
        }
        public IActionResult LoadCitiesByCountryId(int countryId)
        {
            return PartialView("_SelectCitiesPartial", _context.Cities.Where(x=>x.CountryId==countryId));
        }
    }
}
