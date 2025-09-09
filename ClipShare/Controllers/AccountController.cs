using ClipShare.Core.Entities;
using ClipShare.DataAccess.Data;
using ClipShare.Utility;
using ClipShare.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClipShare.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly Context _context;

        public AccountController(UserManager<AppUser>userManager,
            SignInManager<AppUser>signInManager,Context context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            var loginvm = new Login_vm()
            {
                ReturnUrl = returnUrl
            };
            return View(loginvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login_vm model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            model.ReturnUrl = model.ReturnUrl ?? Url.Content("~/");

            var user = await _userManager.FindByNameAsync(model.UserName);
            if(user == null)
            {
                user = await _userManager.FindByEmailAsync(model.UserName);
            }
            if(user==null)
            {
                ModelState.AddModelError(string.Empty, "Invalid UserName or Password, Please try again");
                return View(model);

            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if(result.Succeeded)
            {
                await HandleSignInUserAsync(user);
                return LocalRedirect(model.ReturnUrl);
            }
            else
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, $"user has been locked,you should wait til {user.LockoutEnd} to log in again");

                }else
                    

                    ModelState.AddModelError(string.Empty, "Invalid UserName or Password, Please try again");
                return View(model);

            }


        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public  async Task<IActionResult>Register()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Register(Register_vm model)
        {
            if(ModelState.IsValid)
            {
                if(!model.Password.Equals(model.ConfirmPassword))
                {
                    ModelState.AddModelError("Confrim Password","Confirmed Password does not match password");
                    return View(model);

                }
                if(await CheckEmailExistsAsync(model.Email))
                {
                    ModelState.AddModelError("Email", $"Email {model.Email} is taken .please try another Email Adress");
                    return View(model);

                }
                if (await CheckNameEsxistAsync(model.Name))
                {
                    ModelState.AddModelError("Name", $"the name  {model.Name} is taken .please try another Name");
                    return View(model);

                }
                var UserToAdd = new AppUser()
                {
                     Name= model.Name,
                     UserName=model.Name.ToLower(),
                    Email = model.Email.ToLower(),
                };
                var result = await _userManager.CreateAsync(UserToAdd,model.Password);
                await _userManager.AddToRoleAsync(UserToAdd,SD.UserRole);
                if(!result.Succeeded)
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);

                }
                await HandleSignInUserAsync(UserToAdd);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
        #region private methods
        private async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }
        private async Task<bool> CheckNameEsxistAsync(string name)
        {
            return await _userManager.Users.AnyAsync(x => x.Name.ToLower() == name.ToLower());
        }
        private async Task HandleSignInUserAsync(AppUser user)
        {
            var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            
            var userChannelId=await _context.Channel.Where(x=>x.AppUserId== user.Id).Select(x=>x.Id).FirstOrDefaultAsync();
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid, userChannelId.ToString()));
           var roles = await _userManager.GetRolesAsync(user);
            claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var principal = new ClaimsPrincipal(claimsIdentity);
            //using this method in order to assign identity claims in to UserIdentity and sign in using built in dotnet identity
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
        #endregion
    }
}
