using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using System.Threading.Tasks;
using DotVVM.Framework.Hosting;
using System.Security.Claims;

namespace SimpleForum.ViewModels
{
	public class SiteViewModel : DotvvmViewModelBase
	{

        public async Task Logout()
        {
            await Context.GetAuthentication().SignOutAsync(Startup.AuthenticationScheme);
            Context.RedirectToRoute("Default");
        }


        protected int? GetUserId()
        {
            if (Context.HttpContext.User.Identity.IsAuthenticated)
            {
                return int.Parse(Context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
            else
            {
                return null;
            }
        }
	}
}

