using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using RFID.SimpleTask.Application.Common.Interfaces;
using static Duende.IdentityServer.Models.IdentityResources;

namespace WebMVC.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IIdentityService _identityService;

        public LoginModel(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }


        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // Here you should check the user credentials against your database
                // This is a simplified example
                //if (Input.Email == "user@example.com" && Input.Password == "password")
                //{
                //    var claims = new List<Claim>
                //{
                //    new Claim(ClaimTypes.Name, Input.Email),
                //    new Claim(ClaimTypes.Role, "User"),
                //};

                //    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //    await HttpContext.SignInAsync(
                //        CookieAuthenticationDefaults.AuthenticationScheme,
                //        new ClaimsPrincipal(claimsIdentity));

                //    return LocalRedirect(returnUrl);
                //}
                var result = await _identityService.SignInAsync(Input.Email, Input.Password);
                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }

                ModelState.AddModelError(string.Empty, "Invalid email or password.");
            }

            return Page();
        }
    }
}
