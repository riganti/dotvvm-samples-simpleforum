using System;

namespace SimpleForum.Model
{
    public class ForumPost
    {

        public int Id { get; set; }

        public int ForumThreadId { get; set; }

        public virtual ForumThread ForumThread { get; set; }

        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }

    }
}