using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpProject.Models
{
    public class Notification
    {
        public int Id{ set; get; }
        public string Description { get; set; }
        public int CommentID { get; set; }
        public Comment Comment { get; set; }
        public bool Reply { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public bool IsReaded { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
