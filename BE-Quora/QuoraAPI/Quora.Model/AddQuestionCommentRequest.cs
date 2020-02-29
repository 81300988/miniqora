using System;
using System.Collections.Generic;
using System.Text;

namespace Quora.Model
{
    public class AddQuestionCommentRequest
    {
        public string CommentContent { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
    }
}
