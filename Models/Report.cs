using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GpProject.Models
{
    public class Report
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        public bool FoundPerson { get; set; }
        [Required]
        public bool LostPerson { get; set; }
        public int Age { get; set; }
        [Required]
        public bool Male { get; set; }
        [Required]
        public bool Famle { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string District { get; set; }
        public byte[] Img { set; get; }
        [Required]
        [NotMapped]
        public IFormFile Image { get; set; }
        [NotMapped]
        public int CommentCount { get; set; }
        public DateTime date { set; get; } = DateTime.Now;
        public int ImageDataId { get; set; }
        
        public ICollection<Comment> Comments { get; set; }
        public User User { get; set; }
    }

}
