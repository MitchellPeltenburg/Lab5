using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Lab5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;

namespace Lab5.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors) //Adds errors if there are any
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(); //Displays the registration page when first loaded
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Email, Email = model.Email }; //Assigns user values to user
                var result = await _userManager.CreateAsync(user, model.Password); //Creates the specified user with given password as async operation
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password."); //Writes message to log

                    await _signInManager.SignInAsync(user, isPersistent: false); //Signs in the specific user
                    _logger.LogInformation("User created a new account with password."); //Writes message to log
                    return RedirectToLocal(returnUrl); //Redirects to home page
                }
                AddErrors(result); //Adds errors if there are any
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme); 

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false); //Contains the sign in result
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in."); //Writes message to log
                    return RedirectToLocal(returnUrl);
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out."); //Writes message to log
                    return RedirectToAction(nameof(Lockout));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt."); //Adds error
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); //Signs current user out of the application
            _logger.LogInformation("User logged out."); //Writes message to log
            return RedirectToAction(nameof(HomeController.Index), "Home"); //Redirects to home page
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost][AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email); //Finds the specified email address
                if (user != null) //If a user is found
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user); //Generate password reset token for user

                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                        new { email = model.Email, token = token }, Request.Scheme); //Build password reset link

                    _logger.Log(LogLevel.Warning, passwordResetLink); //Write password reset token to log (as well as the console window)

                    return View("ForgotPasswordConfirmation"); //Displays confirmation page
                }
                return View("ForgotPasswordConfirmation"); //Displays confirmation page
            }
            return View(model); //Displays ResetPassword page again, ModelState must have been invalid
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null) //If the user tried to get here without a reset token and correct email
            {
                ModelState.AddModelError("", "Invalid password reset token"); //Add error
            }
            return View();
        }

        [HttpPost][AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email); //Gets the user associated with specified email
                if (user != null) //If user exists
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password); //Reset the password
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation"); //Display confirmation page
                    }
                    foreach (var error in result.Errors) //Display any errors if there are any
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model); //Displays ResetPassword page again, user must have been invalid
                }
                return View("ResetPasswordConfirmation"); //Display confirmation page
            }
            return View(model); //Displays ResetPassword page again, ModelState must have been invalid
        }
    }
}
