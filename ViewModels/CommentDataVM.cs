using System;

namespace GpProject.Controllers.Api
{
    internal class CommentDataVM
    {
        public string CommentData { get; set; }
        public DateTime date { get; set; }
        public string UserId { get; set; }
        public int ReportId { get; set; }
        public int Id { get; set; }
        public byte[] Img { get; set; }
        public string UserName { get; set; }
        public bool Full { get; set; }
    }
}