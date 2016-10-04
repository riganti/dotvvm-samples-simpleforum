using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using SimpleForum.Services;
using DotVVM.Framework.Controls;
using SimpleForum.DTO;
using System.Threading.Tasks;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime.Filters;

namespace SimpleForum.ViewModels
{
	public class DefaultViewModel : SiteViewModel
	{
        private ForumService forumService;

        public DefaultViewModel(ForumService forumService)
        {
            this.forumService = forumService;
        }


        public GridViewDataSet<ForumThreadDTO> ForumThreads { get; set; } = new GridViewDataSet<ForumThreadDTO>()
        {
            PageSize = 20
        };

        public ForumThreadCreateDTO NewThread { get; set; } = new ForumThreadCreateDTO();

        public override Task PreRender()
        {
            forumService.LoadForumThreads(ForumThreads);

            return base.PreRender();
        }

        [Authorize]
        public void CreateThread()
        {
            forumService.CreateThread(NewThread, GetUserId().Value);
            Context.RedirectToRoute(Context.Route.RouteName);
        }
    }
}

