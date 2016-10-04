using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleForum.DTO
{
    public class ForumPostDTO
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        public int AppUserId { get; set; }

        public string AppUserName { get; set; }
        public int AppUserNumberOfPosts { get; internal set; }
    }
}
