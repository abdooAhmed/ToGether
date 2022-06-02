using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GpProject.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ReportId { get; set; }
        public Report Report { get; set; }
        public User User { get; set; }
        public string CommentData { get; set; }

        public ICollection<CommentReply> CommentReplies { get; set; }

        public DateTime date { get; set; } = DateTime.Now;
    }
}
