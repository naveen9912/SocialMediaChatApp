using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using SocialMediaChatApp.Models;

namespace SocialMediaChatApp.Pages
{
    public partial class Login
    {
        protected LoginModel LoginModel = new();

        [Inject] private SignInManager<ApplicationUser> SignInManager { get; set; }
        [Inject] private NavigationManager Navigation { get; set; }

        protected async Task HandleLogin()
        {
            var result = await SignInManager.PasswordSignInAsync(
                LoginModel.Email,
                LoginModel.Password,
                LoginModel.RememberMe,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                Navigation.NavigateTo("/");
            }
            else
            {
                Console.WriteLine("Login failed: Invalid email or password.");
            }
        }
    }
}
