using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.Model
{
    public class AnswerComment
    {
        public int AnswerCommentId { get; set; }
        public int AnswerId { get; set; }
        public string CommentContent { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Commenter { get; set; }
        public string Avatar { get; set; }
        public string AvatarUrl { get; set; }
    }
}
