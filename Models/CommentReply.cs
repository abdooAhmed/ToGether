using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpProject.Models
{
    public class CommentReply
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        
        public User User { get; set; }
        public string CommentData { get; set; }

        public int CommentId { get; set; }
        public Comment Comment { get; set; }

        public DateTime date { get; set; } = DateTime.Now;
    }
}
