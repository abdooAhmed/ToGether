using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GpProject.Models.Account
{
    public class ForgetPasswordView
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
