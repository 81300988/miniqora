using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.Model
{
    public class AddAnswerCommentRequest
    {
        public string CommentContent { get; set; }
        public int UserId { get; set; }
        public int AnswerId { get; set; }
    }
}
