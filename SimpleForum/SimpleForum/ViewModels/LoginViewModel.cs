using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using SimpleForum.Services;
using SimpleForum.DTO;
using System.Threading.Tasks;
using DotVVM.Framework.Hosting;
using System.Security.Claims;

namespace SimpleForum.ViewModels
{
	public class LoginViewModel : SiteViewModel
	{
        private LoginService loginService;

        public LoginViewModel(LoginService loginService)
        {
            this.loginService = loginService;
        }


        public LoginDTO LoginData { get; set; } = new LoginDTO();

        public string ErrorMessage { get; set; }

        public async Task Login()
        {
            ClaimsPrincipal principal = await loginService.TryGetIdentity(LoginData);
            if (principal != null)
            {
                await Context.GetAuthentication().SignInAsync(Startup.AuthenticationScheme, principal);
                Context.RedirectToRoute("Default");
            }
            else
            {
                ErrorMessage = "Invalid credentials.";
            }
        }

    }
}

