using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleForum.DTO
{
    public class ForumPostCreateDTO
    {
        [Required(ErrorMessage = "The Message is required!")]
        public string Message { get; set; }
    }
}
