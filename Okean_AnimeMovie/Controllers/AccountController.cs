using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Okean_AnimeMovie.Core.DTOs;
using Okean_AnimeMovie.Core.Entities;
using Okean_AnimeMovie.Core.Services;

namespace Okean_AnimeMovie.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IJwtService _jwtService;
    private readonly ILogger<AccountController> _logger;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IJwtService jwtService,
        ILogger<AccountController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Add default role
                await _userManager.AddToRoleAsync(user, "User");

                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: model.RememberMe);

                    user.LastLoginAt = DateTime.UtcNow;
                    await _userManager.UpdateAsync(user);

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetLink = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token }, Request.Scheme);

                // TODO: Send email with reset link
                _logger.LogInformation("Password reset link: {ResetLink}", resetLink);

                return RedirectToAction("ForgotPasswordConfirmation");
            }

            // Don't reveal that the user does not exist
            return RedirectToAction("ForgotPasswordConfirmation");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ResetPassword(string email, string token)
    {
        if (email == null || token == null)
        {
            return RedirectToAction("Index", "Home");
        }

        var model = new ResetPasswordDto
        {
            Email = email,
            Token = token
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction("ResetPasswordConfirmation");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult ResetPasswordConfirmation()
    {
        return View();
    }

    // Google Authentication
    [HttpGet]
    public IActionResult GoogleLogin()
    {
        try
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleCallback", "Account")
            };
            return Challenge(properties, "Google");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error initiating Google authentication");
            return RedirectToAction("Login", new { error = "Failed to initiate Google authentication" });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GoogleCallback()
    {
        try
        {
            _logger.LogInformation("GoogleCallback started");
            
            // Get the external login info
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                _logger.LogError("External login info not found");
                return RedirectToAction("Login", new { error = "External login info not found" });
            }

            _logger.LogInformation($"External login provider: {info.LoginProvider}, Key: {info.ProviderKey}");

            // Sign in the user with this external login provider if the user already has a login
            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (signInResult.Succeeded)
            {
                _logger.LogInformation("External login sign in succeeded");
                
                // Update last login time
                var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                if (user != null)
                {
                    user.LastLoginAt = DateTime.UtcNow;
                    await _userManager.UpdateAsync(user);
                    _logger.LogInformation($"Updated last login time for user: {user.Email}");
                }

                _logger.LogInformation("Redirecting to Home/Index after successful external login");
                return RedirectToAction("Index", "Home");
            }

            // If the user does not have an account, then ask the user to create an account
            if (signInResult.IsLockedOut)
            {
                return RedirectToAction("Login", new { error = "Account is locked out" });
            }

            // Get user info from Google
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var name = info.Principal.FindFirstValue(ClaimTypes.Name);
            var firstName = info.Principal.FindFirstValue(ClaimTypes.GivenName);
            var lastName = info.Principal.FindFirstValue(ClaimTypes.Surname);

            _logger.LogInformation($"Google user info - Email: {email}, Name: {name}, FirstName: {firstName}, LastName: {lastName}");

            // Check if user already exists by email
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                _logger.LogInformation($"User already exists with email: {email}");
                
                // User exists but doesn't have Google login, add it
                var addLoginResult = await _userManager.AddLoginAsync(existingUser, info);
                if (addLoginResult.Succeeded)
                {
                    _logger.LogInformation("Successfully added Google login to existing user");
                    
                    await _signInManager.SignInAsync(existingUser, isPersistent: false);
                    existingUser.LastLoginAt = DateTime.UtcNow;
                    await _userManager.UpdateAsync(existingUser);
                    
                    _logger.LogInformation("Redirecting to Home/Index after adding Google login to existing user");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _logger.LogError($"Failed to add Google login to existing user: {string.Join(", ", addLoginResult.Errors)}");
                }
            }

            // Create new user
            _logger.LogInformation("Creating new user from Google login");
            
            var newUser = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FirstName = firstName ?? name?.Split(' ').FirstOrDefault() ?? "User",
                LastName = lastName ?? name?.Split(' ').Skip(1).FirstOrDefault() ?? "",
                EmailConfirmed = true, // Google emails are pre-verified
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            var createResult = await _userManager.CreateAsync(newUser);
            if (createResult.Succeeded)
            {
                _logger.LogInformation("Successfully created new user");
                
                // Add default role
                await _userManager.AddToRoleAsync(newUser, "User");

                // Add the external login
                var addLoginResult = await _userManager.AddLoginAsync(newUser, info);
                if (addLoginResult.Succeeded)
                {
                    _logger.LogInformation("Successfully added Google login to new user");
                    
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    
                    _logger.LogInformation("Redirecting to Home/Index after creating new user");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _logger.LogError($"Failed to add Google login to new user: {string.Join(", ", addLoginResult.Errors)}");
                }
            }
            else
            {
                _logger.LogError($"Failed to create new user: {string.Join(", ", createResult.Errors)}");
            }

            // If we got this far, something failed
            foreach (var error in createResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return RedirectToAction("Login", new { error = "Failed to create account with Google" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during Google authentication callback");
            return RedirectToAction("Login", new { error = "An error occurred during Google authentication" });
        }
    }
}
