using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.Model
{
    public class QuestionComment
    {
        public int QuestionCommentId { get; set; }
        public string CommentContent { get; set; }
        public int QuestionId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Commenter { get; set; }
        public string Avatar { get; set; }
        public string AvatarUrl { get; set; }
    }
}
