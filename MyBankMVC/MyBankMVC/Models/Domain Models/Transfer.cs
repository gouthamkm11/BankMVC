using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBankMVC.Models.Domain_Models
{
    public class Transfer
    {
        [Required (ErrorMessage = "Username is required")]
        public decimal amount { get; set; }
    }
}