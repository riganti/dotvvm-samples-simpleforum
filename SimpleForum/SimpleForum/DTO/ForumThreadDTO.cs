using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleForum.DTO
{
    public class ForumThreadDTO
    {

        public int Id { get; set; }
        public string Title { get; set; }

        public string FirstPostMessage { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PostsCount { get; internal set; }
        public string LastPostUserName { get; internal set; }
        public DateTime LastPostDate { get; internal set; }
    }

}
