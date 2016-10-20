using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleForum.Model
{
    public class ForumThread
    {

        public int Id { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<ForumPost> ForumPosts { get; } = new List<ForumPost>();

    }
}
