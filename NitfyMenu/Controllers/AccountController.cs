using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Models;

namespace Accounts.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IDataProtector _dataProtector;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AccountController(IDataProtectionProvider dataProtectionProvider, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment hostingEnvironment)
        {
            _dataProtector = dataProtectionProvider.CreateProtector("SignIn");
            _userManager = userManager;
            _signInManager = signInManager;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("account/signinactual")]
        public async Task<IActionResult> SignInActual(string t)
        {
            if (!string.IsNullOrEmpty(t))
            {
                var data = _dataProtector.Unprotect(t);

                var parts = data.Split('|');

                var identityUser = await _userManager.FindByIdAsync(parts[0]);

                var isTokenValid = await _userManager.VerifyUserTokenAsync(identityUser, TokenOptions.DefaultProvider, "SignIn", parts[1]);

                if (isTokenValid)
                {
                    await _signInManager.SignInAsync(identityUser, true);
                    if (parts.Length == 3 && Url.IsLocalUrl(parts[2]))
                    {
                        return Redirect(parts[2]);
                    }
                    return Redirect("/");
                }
                else
                {
                    return Unauthorized("STOP!");
                }
            }
            else
            {
                return Unauthorized("STOP!");
            }
        }

        [Authorize]
        [HttpGet("account/signout")]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return Redirect("/Logoff");
        }
    }
}
