using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleForum.DTO
{
    public class LoginDTO
    {

        [Required(ErrorMessage = "The User Name is required!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The Password is required!")]
        public string Password { get; set; }

    }
}
