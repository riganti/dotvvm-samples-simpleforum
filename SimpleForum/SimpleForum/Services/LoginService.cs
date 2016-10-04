using Microsoft.AspNetCore.Identity;
using SimpleForum.DTO;
using SimpleForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace SimpleForum.Services
{
    public class LoginService
    {
        private UserManager<AppUser> userManager;

        public LoginService(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        
        public async Task<ClaimsPrincipal> TryGetIdentity(LoginDTO loginData)
        {
            var user = await userManager.FindByNameAsync(loginData.UserName);
            if (user != null)
            {
                if (await userManager.CheckPasswordAsync(user, loginData.Password))
                {
                    return new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    },
                    Startup.AuthenticationScheme));
                }
            }
            return null;
        }
        
    }
}
