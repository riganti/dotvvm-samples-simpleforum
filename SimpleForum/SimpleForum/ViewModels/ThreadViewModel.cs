using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using SimpleForum.DTO;
using DotVVM.Framework.Controls;
using SimpleForum.Services;
using System.Threading.Tasks;
using DotVVM.Framework.Runtime.Filters;

namespace SimpleForum.ViewModels
{
	public class ThreadViewModel : SiteViewModel
	{
        private ForumService forumService;

        public ThreadViewModel(ForumService forumService)
        {
            this.forumService = forumService;
        }


        public GridViewDataSet<ForumPostDTO> ForumPosts { get; set; } = new GridViewDataSet<ForumPostDTO>()
        {
            PageSize = 20
        };

        public ForumPostCreateDTO NewPost { get; set; } = new ForumPostCreateDTO();



        public override Task PreRender()
        {
            var forumThreadId = Convert.ToInt32(Context.Parameters["Id"]);
            forumService.LoadForumPosts(forumThreadId, ForumPosts);

            return base.PreRender();
        }


        [Authorize]
        public void AddPost()
        {
            var forumThreadId = Convert.ToInt32(Context.Parameters["Id"]);
            forumService.CreatePost(NewPost, forumThreadId, GetUserId().Value);
            Context.RedirectToRoute(Context.Route.RouteName);
        }

    }
}

