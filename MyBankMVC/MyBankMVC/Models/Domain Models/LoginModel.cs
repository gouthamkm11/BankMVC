using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBankMVC.Models.Domain_Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }

        public string ChkAccNo { get; set; }
    }
}