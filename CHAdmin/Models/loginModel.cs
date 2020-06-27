using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CHAdmin.Models
{
    public class loginModel
    {
        [Required(ErrorMessage = "Mời bạn nhập tên đăng nhập")]
        public string userName { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập mật khẩu")]

        public string password { get; set; }

    }
}