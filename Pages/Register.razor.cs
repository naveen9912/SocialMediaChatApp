using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using SocialMediaChatApp.Models;

namespace SocialMediaChatApp.Pages
{
    public partial class Register 
    {
        protected RegisterModel RegisterModel = new(); // ✅ ONE INSTANCE ONLY

        [Inject] private UserManager<ApplicationUser> UserManager { get; set; }
        [Inject] private SignInManager<ApplicationUser> SignInManager { get; set; }
        [Inject] private NavigationManager Navigation { get; set; }

        protected async Task HandleRegister()
        {
            var user = new ApplicationUser
            {
                UserName = RegisterModel.Email,
                Email = RegisterModel.Email
            };

            var result = await UserManager.CreateAsync(user, RegisterModel.Password);

            if (result.Succeeded)
            {
                await SignInManager.SignInAsync(user, isPersistent: false);
                Navigation.NavigateTo("/");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Registration Error: {error.Description}");
                }
            }
        }
    }
}
