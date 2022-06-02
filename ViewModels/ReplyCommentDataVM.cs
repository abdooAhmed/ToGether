using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpProject.ViewModels
{
    public class ReplyCommentDataVM
    {
        public string CommentData { get; set; }
        public DateTime date { get; set; }
        public string UserId { get; set; }
        public int CommentId { get; set; }
        public int Id { get; set; }
        public byte[] Img { get; set; }
        public string UserName { get; set; }
    }
}
