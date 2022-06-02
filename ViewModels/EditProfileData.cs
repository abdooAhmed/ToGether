using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GpProject.ViewModels
{
    public class EditProfileData
    {
        
        public String FirstName { get; set; }
        
        public String LastName { get; set; }
        
        public string UserName { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        
        [Display(Name = "Confirm New Password")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
        
        public string Country { get; set; }
        
        public string City { get; set; }
        
        public string District { get; set; }
       
        [Phone]
        public string Phone { get; set; }
        public IFormFile Image { get; set; }
    }
}
