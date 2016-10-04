using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;

namespace SimpleForum.Model
{
    public class AppUser : IdentityUser<int>
    {

        public virtual ICollection<ForumPost> ForumPosts { get; } = new List<ForumPost>();

    }
}